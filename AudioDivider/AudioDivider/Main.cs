using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Principal;
using Microsoft.Win32;
using System.IO;

namespace SoundTest_C
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Logging.workingDirectory = Directory.GetCurrentDirectory() + "\\";
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\AudioDivider", "Path", Logging.workingDirectory); 
            bool admin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
            if (!admin)
            {
                MessageBox.Show("This program needs admin rights.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AudioDivider());
        }
    }
}
