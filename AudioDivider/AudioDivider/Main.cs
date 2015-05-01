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
        static Configuration configuration;
        [STAThread]
        static void Main()
        {
            Logger.setLogger(new Logger(configuration));

            Communication communication = new Communication();

            string workingDirectory = Directory.GetCurrentDirectory() + "\\";

            configuration = new Configuration(workingDirectory);
            configuration.Load();
           
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\AudioDivider", "Path", configuration.DataFolder);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormAudioDivider(configuration, communication));
        }
    }
}
