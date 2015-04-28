using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace AudioControl
{
    class Logging
    {
        public static string workingDirectory;

        public static void Error(string text)
        {
            Log("Error: " + text);
        }

        public static void Log(string text)
        {
            try
            {
                File.AppendAllText(Logging.workingDirectory + "AudioDivider.log", DateTime.Now.ToString("'['hh':'mm':'ss'] '") + "(Server): " + text + "\n");
            }
            catch (Exception)
            {
                try
                {
                    Thread.Sleep(100); // To work against race conditions when injecting the Dll
                    File.AppendAllText(Logging.workingDirectory + "AudioDivider.log", DateTime.Now.ToString("'['hh':'mm':'ss'] '") + "(Server): " + text + "\n");
                }
                catch (Exception)
                {

                }
            }
        }
        public static void Log(string text, int data)
        {
            try
            {
                File.AppendAllText(Logging.workingDirectory + "AudioDivider.log", DateTime.Now.ToString("'['hh':'mm':'ss'] '") + "(Server): " + text + data + "\n");
            }
            catch (Exception)
            {
                try
                {
                    Thread.Sleep(100); // To work against race conditions when injecting the Dll
                    File.AppendAllText(Logging.workingDirectory + "AudioDivider.log", DateTime.Now.ToString("'['hh':'mm':'ss'] '") + "(Server): " + text + data + "\n");
                }
                catch (Exception)
                {

                }
            }

        }
    }

}
