using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace AudioDivider
{
   public class Communication
    {

        Logger logger;
        Security security;
        public Communication()
        {
            logger = Logger.getLogger();
            security = new Security();
        }

        // Message format, as used in the DLLs
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        struct ClientMessage
        {
            public int pid;
            public int action;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
            public byte[] data;
            public int dataLen;
            public ClientMessage(int pid, int action, byte[] mesData)
            {
                this.pid = pid;
                this.action = action;
                this.data = new byte[200];
                for (int i = 0; i < mesData.Length; i++)
                {
                    this.data[i] = mesData[i];
                }
                this.dataLen = mesData.Length;
            }

            public ClientMessage(int pid, int action, string message)
                : this(pid, action, Encoding.Unicode.GetBytes(message))
            {
            }
        };

        List<IntPtr> Clients = new List<IntPtr>();

        int PipeBufferSize = 1000;
        string pipeName = "\\\\.\\pipe\\SoundInjectController";

        Thread serverThread;
        volatile bool serverRunning;

        void PipeServerThread()
        {
            try
            {
                logger.Log("Waiting for clients.");
                serverRunning = true;
                bool first = true;
                while (serverRunning)
                {
                    Native.SECURITY_DESCRIPTOR SD = new Native.SECURITY_DESCRIPTOR();
                    SD.Revision = Native.SECURITY_DESCRIPTOR_REVISION;
                    Native.InitializeSecurityDescriptor(out SD, Native.SECURITY_DESCRIPTOR_REVISION);
                    Native.SetSecurityDescriptorDacl(ref SD, 1, IntPtr.Zero, 0);
                    Native.SECURITY_ATTRIBUTES SA = new Native.SECURITY_ATTRIBUTES();
                    SA.nLength = Marshal.SizeOf(SA);
                    IntPtr SDPtr = Marshal.AllocHGlobal(Marshal.SizeOf(SD));
                    Marshal.StructureToPtr(SD, SDPtr, false);
                    SA.lpSecurityDescriptor = SDPtr;
                    SA.bInheritHandle = 1;
                    
                    IntPtr hPipe;
                    if (first)
                    {
                        first = false;
                        hPipe = Native.CreateNamedPipe(pipeName, Native.PIPE_ACCESS_DUPLEX | Native.WRITE_DAC | Native.ACCESS_SYSTEM_SECURITY | Native.WRITE_OWNER, Native.PIPE_TYPE_MESSAGE | Native.PIPE_READMODE_MESSAGE | Native.PIPE_WAIT | Native.PIPE_REJECT_REMOTE_CLIENTS, Native.PIPE_UNLIMITED_INSTANCES, PipeBufferSize, PipeBufferSize, 0, ref SA);
                        security.SetLowIntegrity(hPipe);
                    }
                    else // only first pipe can change dacl/sacl
                    {
                        hPipe = Native.CreateNamedPipe(pipeName, Native.PIPE_ACCESS_DUPLEX, Native.PIPE_TYPE_MESSAGE | Native.PIPE_READMODE_MESSAGE | Native.PIPE_WAIT | Native.PIPE_REJECT_REMOTE_CLIENTS, Native.PIPE_UNLIMITED_INSTANCES, PipeBufferSize, PipeBufferSize, 0, ref SA);
                    }
                    if (hPipe == Native.INVALID_HANDLE_VALUE)
                    {
                        logger.Log("CreateNamedPipe failed: ", Marshal.GetLastWin32Error());
                        Thread.Sleep(1000);
                    }
                   


                    if (0 == Native.ConnectNamedPipe(hPipe, IntPtr.Zero))
                    {
                        if (Marshal.GetLastWin32Error() == 535) // ERROR_PIPE_CONNECTED
                        {
                            Clients.Add(hPipe);
                            logger.Log("AcceptedClient.");
                        }
                        else
                        {
                            logger.Log("ConnectNamedPipe failed: ", Marshal.GetLastWin32Error());
                        }
                    }
                    else
                    {
                        Clients.Add(hPipe);
                        logger.Log("AcceptedClient.");
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

        }

        public void ServerStart()
        {
            serverThread = new Thread(PipeServerThread);
            serverThread.IsBackground = true;
            serverThread.Start();
        }

        public void ServerStop()
        {
            serverRunning = false;
            serverThread.Abort();

        }

        // Sends a message to all hooked processes. The DLLs decide whether the message should be processed using the process ID
        public void ServerSend(int pid, int action, string messageStr)
        {
            string mes = Console.ReadLine();
            for (int i = 0; i < Clients.Count; i++)
            {
                ClientMessage message = new ClientMessage(pid, action, messageStr);
                IntPtr nativeMessage = Marshal.AllocHGlobal(Marshal.SizeOf(message));
                Marshal.StructureToPtr(message, nativeMessage, false);

                int numBytesWritten;
                if (0 == Native.WriteFile(Clients[i], nativeMessage, Marshal.SizeOf(message), out numBytesWritten, IntPtr.Zero))
                {
                    if (Native.GetLastError() == Native.ERROR_NO_DATA)
                    {
                        logger.Log("Client disconnected.");
                        Clients.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        logger.Log("WriteFile failed: ", Native.GetLastError());
                    }
                }
                Marshal.FreeHGlobal(nativeMessage);
            }
        }

    }
}
