using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AudioDivider
{
    public class ProgramInfo
    {
        Process process;
        public string deviceID;
        public string name;
        public int pid;
        public ProgramInfo(string name, int pid, string deviceID)
        {
            this.name = name;
            this.pid = pid;
            this.deviceID = deviceID;
            process = Process.GetProcessById(pid);
        }

        public bool IsAlive()
        {
            try
            {
                return !process.HasExited;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public string Path
        {
            get
            {
                return process.Modules[0].FileName;
            }
        }
    }
}
