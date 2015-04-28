using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AudioDivider
{
    class Configuration
    {
        string dataFolder;

        Dictionary<string, string> configurationValues;
        public Configuration(string dataFolder)
        {
            this.dataFolder = dataFolder;
            configurationValues = new Dictionary<string, string>();
        }

        public string DataFolder
        {
            get
            {
                return dataFolder;
            }
        }

        public void Save()
        {

        }

        public void Load()
        {

        }

        public string this[string index]
        {
            get
            {
                return null;
            }
            set
            {

            }
        }
    }
}
