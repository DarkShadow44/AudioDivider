using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AudioDivider
{
    public class Configuration
    {
        string dataFolder;
        bool showOnlyActivePrograms;
        IniConfiguration iniConfiguration;

        public Configuration(string dataFolder)
        {
            this.dataFolder = dataFolder;
            iniConfiguration = new IniConfiguration();
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
            iniConfiguration.SetValueBool("AudioDivider", "showOnlyActivePrograms", showOnlyActivePrograms);

            iniConfiguration.SetValueInt("AudioDivider", "autoControl", autoControls.Count);

            for (int i = 0; i < autoControls.Count; i++)
            {
                iniConfiguration.SetValueInt("AutoControl" + i, "selectBy", (int)autoControls[i].selectBy);
                iniConfiguration.SetValueString("AutoControl" + i, "windowName", autoControls[i].windowName);
                iniConfiguration.SetValueString("AutoControl" + i, "programPath", autoControls[i].programPath);
                iniConfiguration.SetValueString("AutoControl" + i, "deviceId", autoControls[i].deviceId);
                iniConfiguration.SetValueBool("AutoControl" + i, "instantHook", autoControls[i].instantHook);
            }


            iniConfiguration.Save(dataFolder + "\\config.ini");
        }

        public void Load()
        {
            if (!System.IO.File.Exists(dataFolder + "\\config.ini"))
                return;

            iniConfiguration.Load(dataFolder + "\\config.ini");
            showOnlyActivePrograms = iniConfiguration.GetValueBool("AudioDivider", "showOnlyActivePrograms", false);

            autoControls.Clear();

            int num = iniConfiguration.GetValueInt("AudioDivider", "autoControl", 0);

            for (int i = 0; i < num; i++)
            {
                int selectBy = iniConfiguration.GetValueInt("AutoControl" + i, "selectBy", 0);
                string windowName = iniConfiguration.GetValueString("AutoControl" + i, "windowName");
                string programPath = iniConfiguration.GetValueString("AutoControl" + i, "programPath");
                string deviceId = iniConfiguration.GetValueString("AutoControl" + i, "deviceId");
                bool instantHook = iniConfiguration.GetValueBool("AutoControl" + i, "instantHook", false);
                autoControls.Add(new ProgramAutoInfo((ProgramAutoInfo.SelectBy)selectBy, programPath, windowName, deviceId, instantHook));
            }
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

        public bool ShowOnlyActivePrograms
        {
            get
            {
                return showOnlyActivePrograms;
            }
            set
            {
                showOnlyActivePrograms = value;
                Save();
            }
        }

        List<ProgramAutoInfo> autoControls = new List<ProgramAutoInfo>();

        public bool AutoControlExists(string programPath, string windowName)
        {
            return autoControls.Exists(autoControl => autoControl.programPath == programPath && autoControl.windowName == windowName);
        }

        public void AutoControlAdd(ProgramAutoInfo autoControl)
        {
            autoControls.Add(autoControl);
            Save();
        }

        public void AutoControlRemove(string programPath, string windowName)
        {
            autoControls.RemoveAll(autoControl => autoControl.programPath == programPath && autoControl.windowName == windowName);
            Save();
        }

        public List<ProgramAutoInfo> GetAutoControls
        {
            get
            {
                return autoControls;
            }
        }
    }
}
