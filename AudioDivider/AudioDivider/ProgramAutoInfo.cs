using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace AudioDivider
{
    public class ProgramAutoInfo
    {
        public enum SelectBy
        {
            ProgramPath = 0,
            Windowname
        }

        public SelectBy selectBy;
        public string programPath;
        public string windowName;
        public string deviceId;
        public bool instantHook;

        public ProgramAutoInfo(SelectBy selectBy, string programPath, string windowName, string deviceId, bool instantHook)
        {
            this.selectBy = selectBy;
            this.programPath = programPath;
            this.windowName = windowName;
            this.deviceId = deviceId;
            this.instantHook = instantHook;
        }
    }
}
