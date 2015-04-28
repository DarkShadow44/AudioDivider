using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Principal;
using Microsoft.Win32;
using System.IO;

namespace AudioDivider
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Logging.workingDirectory = Directory.GetCurrentDirectory() + "\\";
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\AudioDivider", "Path", Logging.workingDirectory);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormAudioDivider());
        }
    }
}
