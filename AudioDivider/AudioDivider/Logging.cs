using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace AudioDivider
{
    class Logger
    {
        static Logger logger;

        public static Logger getLogger()
        {
            return logger;
        }
        public static void setLogger(Logger newLogger)
        {
            logger = newLogger;
        }


        Configuration configuration;

        public Logger(Configuration configuration)
        {
            this.configuration = configuration;
        }

        public void Error(string text)
        {
            Log("Error: " + text);
        }

        public void Log(string text)
        {
            try
            {
                File.AppendAllText(configuration.DataFolder + "AudioDivider.log", DateTime.Now.ToString("'['hh':'mm':'ss'] '") + "(Server): " + text + "\n");
            }
            catch (Exception)
            {
                try
                {
                    Thread.Sleep(100); // To work against race conditions when injecting the Dll
                    File.AppendAllText(configuration.DataFolder + "AudioDivider.log", DateTime.Now.ToString("'['hh':'mm':'ss'] '") + "(Server): " + text + "\n");
                }
                catch (Exception)
                {

                }
            }
        }
        public void Log(string text, int data)
        {
            try
            {
                File.AppendAllText(configuration.DataFolder + "AudioDivider.log", DateTime.Now.ToString("'['hh':'mm':'ss'] '") + "(Server): " + text + data + "\n");
            }
            catch (Exception)
            {
                try
                {
                    Thread.Sleep(100); // To work against race conditions when injecting the Dll
                    File.AppendAllText(configuration.DataFolder + "AudioDivider.log", DateTime.Now.ToString("'['hh':'mm':'ss'] '") + "(Server): " + text + data + "\n");
                }
                catch (Exception)
                {

                }
            }

        }
    }
}
