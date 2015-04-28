using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioDivider
{
    class Security
    {
        Logger logger;
        public Security()
        {
            logger = Logger.getLogger();
        }

        // Enable the privilege to change security parameters
        public void EnableSeSecurityPrivilege()
        {
            int error;

            Native.LUID luid = new Native.LUID();
            error = Native.LookupPrivilegeValue(null, "SeSecurityPrivilege", ref luid);
            if (error == 0)
                logger.Error("'LookupPrivilegeValue' failed.");

            IntPtr hToken;
            error = Native.OpenProcessToken(Native.GetCurrentProcess(), Native.TOKEN_QUERY | Native.TOKEN_ADJUST_PRIVILEGES, out hToken);
            if (error == 0)
                logger.Error("'OpenProcessToken' failed.");

            Native.TOKEN_PRIVILEGES tokenPrivileges;
            tokenPrivileges.PrivilegeCount = 1;
            tokenPrivileges.Privileges = new Native.LUID_AND_ATTRIBUTES[1];
            tokenPrivileges.Privileges[0].Luid = luid;
            tokenPrivileges.Privileges[0].Attributes = (int)Native.SE_PRIVILEGE_ENABLED;
            error = Native.AdjustTokenPrivileges(hToken, 0, ref tokenPrivileges, 0, IntPtr.Zero, IntPtr.Zero);
            if (error == 0)
                logger.Error("'AdjustTokenPrivileges' failed.");
        }

        // Sets the rights for a handle (e.g pipe) to lowest, so even low-integrity programs like browsers are allowed to access it
        public void SetLowIntegrity(IntPtr handle)
        {
            int error;

            string descriptor = "S:(ML;;NW;;;LW)";
            IntPtr securityDescriptor = new IntPtr();
            uint securityDescriptorSize;
            error = Native.ConvertStringSecurityDescriptorToSecurityDescriptor(descriptor, Native.SDDL_REVISION_1, ref securityDescriptor, out securityDescriptorSize);
            if (error == 0)
                logger.Error("'ConvertStringSecurityDescriptorToSecurityDescriptor' failed.");

            int saclPresent;
            IntPtr sacl = new IntPtr();
            int lpbSaclDefaulted;
            error = Native.GetSecurityDescriptorSacl(securityDescriptor, out saclPresent, ref sacl, out lpbSaclDefaulted);
            if (error == 0)
                logger.Error("'GetSecurityDescriptorSacl' failed.");

            error = Native.SetSecurityInfo(handle, Native.SE_OBJECT_TYPE.SE_KERNEL_OBJECT, Native.LABEL_SECURITY_INFORMATION, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, sacl);
            if (error != 0)
                logger.Error("'SetSecurityInfo' failed.");
        }
    }
}
