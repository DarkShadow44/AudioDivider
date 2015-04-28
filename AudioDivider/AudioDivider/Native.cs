using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AudioDivider
{
    class Native
    {

        // #################### values for pipes ####################
        public static IntPtr INVALID_HANDLE_VALUE = (IntPtr)(-1);
        public const int PIPE_ACCESS_DUPLEX = 0x00000003;
        public const int PIPE_WAIT = 0x00000000;
        public const int PIPE_TYPE_MESSAGE = 0x00000004;
        public const int PIPE_READMODE_MESSAGE = 0x00000002;
        public const int PIPE_REJECT_REMOTE_CLIENTS = 0x00000008;
        public const int PIPE_UNLIMITED_INSTANCES = 255;
        public const int ERROR_NO_DATA = 232;

        public const int WRITE_OWNER = 0x00080000;

        // #################### Values for memory ####################
        public const int PROCESS_VM_WRITE = 0x0020;
        public const int PROCESS_VM_READ = 0x0010;
        public const int PROCESS_VM_OPERATION = 0x0008;
        public const int PROCESS_CREATE_THREAD = 0x0002;
        public const int PROCESS_QUERY_INFORMATION = 0x0400;

        public const int MEM_COMMIT = 0x00001000;
        public const int MEM_RESERVE = 0x00002000;

        public const int PAGE_READWRITE = 0x04;

        // #################### Values for security ####################
        public const uint OWNER_SECURITY_INFORMATION = 0x00000001;
        public const uint GROUP_SECURITY_INFORMATION = 0x00000002;
        public const uint DACL_SECURITY_INFORMATION = 0x00000004;
        public const uint SACL_SECURITY_INFORMATION = 0x00000008;
        public const uint LABEL_SECURITY_INFORMATION = 0x00000010;

        public const uint PROTECTED_DACL_SECURITY_INFORMATION = 0x80000000;
        public const uint PROTECTED_SACL_SECURITY_INFORMATION = 0x40000000;
        public const uint UNPROTECTED_DACL_SECURITY_INFORMATION = 0x20000000;
        public const uint UNPROTECTED_SACL_SECURITY_INFORMATION = 0x10000000;

        public const int ACCESS_SYSTEM_SECURITY = 0x01000000;
        public const int WRITE_DAC = 0x00040000;

        public const int SDDL_REVISION_1 = 1;

        public const int TOKEN_QUERY = 0x0008;
        public const int TOKEN_ADJUST_PRIVILEGES = 0x0020;

        public const int SE_PRIVILEGE_ENABLED_BY_DEFAULT = 0x00000001;
        public const uint SE_PRIVILEGE_ENABLED = 0x00000002;
        public const uint SE_PRIVILEGE_REMOVED = 0X00000004;
        public const uint SE_PRIVILEGE_USED_FOR_ACCESS = 0x80000000;

        public const int SECURITY_DESCRIPTOR_REVISION = 1;

        // #################### Structures and enums ####################
        public enum SE_OBJECT_TYPE
        {
            SE_UNKNOWN_OBJECT_TYPE = 0,
            SE_FILE_OBJECT,
            SE_SERVICE,
            SE_PRINTER,
            SE_REGISTRY_KEY,
            SE_LMSHARE,
            SE_KERNEL_OBJECT,
            SE_WINDOW_OBJECT,
            SE_DS_OBJECT,
            SE_DS_OBJECT_ALL,
            SE_PROVIDER_DEFINED_OBJECT,
            SE_WMIGUID_OBJECT,
            SE_REGISTRY_WOW64_32KEY,
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct LUID
        {
            public int LowPart;
            public int HighPart;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct LUID_AND_ATTRIBUTES
        {
            public LUID Luid;
            public int Attributes;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TOKEN_PRIVILEGES
        {
            public int PrivilegeCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public LUID_AND_ATTRIBUTES[] Privileges;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_DESCRIPTOR
        {
            public byte Revision;
            public byte Sbz1;
            public short Control;
            public IntPtr Owner;
            public IntPtr Group;
            public IntPtr Sacl;
            public IntPtr Dacl;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public int bInheritHandle;
        }

        // #################### Methods for pipe communication ####################
        [DllImport("kernel32", SetLastError = true)]
        public extern static IntPtr CreateNamedPipe(string lpName, int dwOpenMode, int dwPipeMode, int nMaxInstances, int nOutBufferSize, int nInBufferSize, int nDefaultTimeOut, ref  SECURITY_ATTRIBUTES lpSecurityAttributes);

        [DllImport("kernel32", SetLastError = true)]
        public extern static int ConnectNamedPipe(IntPtr hNamedPipe, IntPtr lpOverlapped);

        [DllImport("kernel32")]
        public extern static int WriteFile(IntPtr hFile, IntPtr lpBuffer, int nNumberOfBytesToWrite, out int lpNumberOfBytesWritten, IntPtr lpOverlapped);

        // #################### Methods for injection ####################
        [DllImport("kernel32")]
        public extern static IntPtr GetCurrentProcess();

        [DllImport("kernel32")]
        public extern static IntPtr OpenProcess(int dwDesiredAccess, int bInheritHandle, int dwProcessId);

        [DllImport("kernel32")]
        public extern static IntPtr LoadLibraryA(string lpFileName);

        [DllImport("kernel32")]
        public extern static IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32")]
        public extern static IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, int flAllocationType, int flProtect);

        [DllImport("kernel32")]
        public extern static int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, IntPtr nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32", SetLastError = true)]
        public extern static IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, IntPtr dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, int dwCreationFlags, out int lpThreadId);

        // #################### Methods regarding security ####################
        [DllImport("advapi32")]
        public extern static int OpenProcessToken(IntPtr ProcessHandle, int DesiredAccess, out IntPtr TokenHandle);

        [DllImport("Advapi32.dll")]
        public extern static int InitializeSecurityDescriptor(out SECURITY_DESCRIPTOR refpSecurityDescriptor, int dwRevision);

        [DllImport("Advapi32.dll")]
        public extern static int SetSecurityDescriptorDacl(ref SECURITY_DESCRIPTOR pSecurityDescriptor, int bDaclPresent, IntPtr pDacl, int bDaclDefaulted);

        [DllImport("Advapi32")]
        public extern static int SetSecurityInfo(IntPtr handle, SE_OBJECT_TYPE ObjectType, uint SecurityInfo, IntPtr psidOwner, IntPtr psidGroup, IntPtr pDacl, IntPtr pSacl);

        [DllImport("advapi32")]
        public extern static int LookupPrivilegeValue(string lpSystemName, string lpName, ref     LUID lpLuid);

        [DllImport("advapi32")]
        public extern static int AdjustTokenPrivileges(IntPtr TokenHandle, int DisableAllPrivileges, ref TOKEN_PRIVILEGES NewState, int BufferLength, IntPtr PreviousState, IntPtr ReturnLength);

        [DllImport("Advapi32")]
        public extern static int ConvertStringSecurityDescriptorToSecurityDescriptor(string StringSecurityDescriptor, int StringSDRevision, ref  IntPtr /*Pointer to pointer*/ SecurityDescriptor, out  uint SecurityDescriptorSize);
        [DllImport("Advapi32")]
        public extern static int GetSecurityDescriptorSacl(IntPtr pSecurityDescriptor, out int lpbSaclPresent, ref  IntPtr pSacl, out int lpbSaclDefaulted);

        // #################### Misc methods ####################
        [DllImport("kernel32.dll")]
        public extern static int IsWow64Process(IntPtr hProcess, out int Wow64Process);

        [DllImport("kernel32")]
        public extern static int TerminateThread(IntPtr hThread, int dwExitCode);

        [DllImport("kernel32")]
        public extern static int GetLastError();

    }
}
