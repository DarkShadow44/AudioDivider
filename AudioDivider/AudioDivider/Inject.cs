using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AudioDivider
{
    class Injector
    {
        Logger logger;
        Security security;
        public Injector()
        {
            logger = Logger.getLogger();
            security = new Security();
            Initialize();
        }

        int LoadLibrary32Address; // Address of LoadLibrary in 32Bit modules, we get this using a 32Bit-helper program. We need this because the address of LoadLibrary is different in 64Bit and 32Bit processes.
        string WorkingDirectory;

        void Initialize()
        {
            WorkingDirectory = System.IO.Directory.GetCurrentDirectory();

            if (!System.IO.File.Exists(WorkingDirectory + @"\Helper.exe"))
            {
                System.Windows.Forms.MessageBox.Show("Helper.exe missing", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            if (!System.IO.File.Exists(WorkingDirectory + @"\SoundHook32.dll"))
            {
                System.Windows.Forms.MessageBox.Show("SoundHook32.dll missing", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

#if !RELEASE32
            if (!System.IO.File.Exists(WorkingDirectory + @"\SoundHook64.dll"))
            {
                System.Windows.Forms.MessageBox.Show("SoundHook64.dll missing", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
#endif

            Process.EnterDebugMode();
            security.EnableSeSecurityPrivilege();

            Process process = new Process();
            process.StartInfo.FileName = WorkingDirectory + @"\Helper.exe";
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();

            LoadLibrary32Address = process.ExitCode;

            
        }

        // Inject the DLL setting the hook into a process
        public void Inject(int pid)
        {
            int error;

            int isWow;
            IntPtr hProcess = Native.OpenProcess(Native.PROCESS_VM_WRITE | Native.PROCESS_VM_READ | Native.PROCESS_VM_OPERATION | Native.PROCESS_CREATE_THREAD | Native.PROCESS_QUERY_INFORMATION, 0, pid);
            Native.IsWow64Process(hProcess, out isWow);

            string dllPath32 = WorkingDirectory + @"\SoundHook32.dll";
            string dllPath64 = WorkingDirectory + @"\SoundHook64.dll";

            string dllPath;
            IntPtr addressLoadLibrary;

#if RELEASE32
            dllPath = dllPath32;
            addressLoadLibrary = new IntPtr(LoadLibrary32Address);
#else
            if (isWow == 1)
            {
                dllPath = dllPath32;
                addressLoadLibrary = new IntPtr(LoadLibrary32Address);
            }
            else
            {
                dllPath = dllPath64;
                IntPtr kernel32 = Native.LoadLibraryA("kernel32.dll");
                addressLoadLibrary = Native.GetProcAddress(kernel32, "LoadLibraryA");
            }
#endif



            // Write path to DLL into memory
            IntPtr memPath = Native.VirtualAllocEx(hProcess, IntPtr.Zero, new IntPtr(200), Native.MEM_COMMIT | Native.MEM_RESERVE, Native.PAGE_READWRITE);
            if (memPath == IntPtr.Zero)
            {
                logger.Error("'VirtualAllocEx' failed: " + Native.GetLastError());
            }
            IntPtr bytesWritten;
            IntPtr strPtr = Marshal.StringToHGlobalAnsi(dllPath);
            error = Native.WriteProcessMemory(hProcess, memPath, strPtr, new IntPtr(dllPath.Length + 1), out bytesWritten);
            if (error != 1)
                logger.Error("'WriteProcessMemory' failed.");

            int threadId;
            IntPtr handleThread = Native.CreateRemoteThread(hProcess, IntPtr.Zero, IntPtr.Zero, addressLoadLibrary, memPath, 0, out threadId);
            if (handleThread == IntPtr.Zero)
            {
                logger.Log("inject failed : ", Marshal.GetLastWin32Error());
                System.Windows.Forms.MessageBox.Show("Failed to control the program.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                logger.Log("Inject successful");
            }
        }

    }
}
