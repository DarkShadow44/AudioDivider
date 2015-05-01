using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AudioDivider
{
    class IniConfiguration
    {
        class Property
        {
            public string name;
            public string value;
            public Property(string name, string value)
            {
                this.name = name;
                this.value = value;
            }
        }

        class Section
        {
            public string name;
            public List<Property> properties = new List<Property>();
            public Section(string name)
            {
                this.name = name;
            }

            public Property TryGetPropery(string name)
            {
                foreach (Property property in properties)
                {
                    if (property.name == name)
                        return property;
                }
                return null;
            }

        }

        List<Section> sections = new List<Section>();

        public IniConfiguration()
        {

        }

        Section TryGetSection(string name)
        {
            foreach (Section section in sections)
            {
                if (section.name == name)
                    return section;
            }
            return null;
        }

        public void SetValueString(string nameSection, string nameProperty, string value)
        {
            Section section = TryGetSection(nameSection);
            if (section == null)
            {
                section = new Section(nameSection);
                sections.Add(section);
            }

            Property property = section.TryGetPropery(nameProperty);
            if (property == null)
            {
                property = new Property(nameProperty, value);
                section.properties.Add(property);
            }
            else
            {
                property.value = value;
            }
        }

        public string GetValueString(string nameSection, string nameProperty)
        {
            Section section = TryGetSection(nameSection);
            if (section == null)
                return null;

            Property property = section.TryGetPropery(nameProperty);
            if (property == null)
                return null;

            return property.value;
        }

        public void SetValueInt(string nameSection, string nameProperty, int value)
        {
            SetValueString(nameSection, nameProperty, value.ToString());
        }

        public int GetValueInt(string nameSection, string nameProperty, int defaultValue)
        {
            string value = GetValueString(nameSection, nameProperty);

            if (value == null)
                return defaultValue;

            int valueInt;
            if (int.TryParse(value, out valueInt))
                return valueInt;
            else
                return defaultValue;
        }

        public void SetValueBool(string nameSection, string nameProperty, bool value)
        {
            SetValueString(nameSection, nameProperty, value ? "true" : "false");
        }

        public bool GetValueBool(string nameSection, string nameProperty, bool defaultValue)
        {
             string value = GetValueString(nameSection, nameProperty);
             if (value == null)
                 return defaultValue;

             bool valueBool;
             if (bool.TryParse(value, out valueBool))
                 return valueBool;
             else
                 return defaultValue;
        }

        public void Save(Stream stream)
        {
            StreamWriter sw = new StreamWriter(stream, Encoding.UTF8);

            foreach (Section section in sections)
            {
                sw.WriteLine("[" + section.name + "]");
                foreach (Property property in section.properties)
                {
                    sw.Write(StringExtensions.Escape(property.name));
                    sw.Write("=");
                    sw.Write(StringExtensions.Escape(property.value));
                    sw.WriteLine();
                }
            }
            sw.Flush();
        }

        public void Load(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                Load(fs);
            }
        }

        public void Save(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                Save(fs);
            }
        }

        public void Load(Stream stream)
        {
            sections.Clear();
            StreamReader sr = new StreamReader(stream, Encoding.UTF8);

            Section currentSection = null;
            while (sr.Peek() >= 0)
            {
                string line = sr.ReadLine();
                if (line.StartsWith("["))
                {
                    line = line.Substring(1, line.Length - 2);
                    currentSection = new Section(line);
                    sections.Add(currentSection);
                }
                else
                {
                    bool found = false;
                    int middle = 0;
                    while (!found)
                    {
                        middle = line.IndexOf('=', middle);
                        if (line[middle - 1] != '\\')
                            found = true;
                    }
                    string propertyName = line.Substring(0, middle);
                    string propertyValue = line.Substring(middle + 1, line.Length - (middle + 1));
                    Property property = new Property(StringExtensions.Unescape(propertyName), StringExtensions.Unescape(propertyValue));
                    currentSection.properties.Add(property);
                }
            }
        }
    }
}
