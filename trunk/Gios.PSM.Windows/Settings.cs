using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace Gios.PDF.SplitMerge
{
    public class Settings
    {
        private bool _allowMultipleInstances;
        public bool AllowMultipleInstances
        {
            get { return _allowMultipleInstances; }
            set { _allowMultipleInstances = value; }
        }

        private string _defaultOutputFolder;
        public string DefaultOutputFolder
        {
            get { return _defaultOutputFolder; }
            set { _defaultOutputFolder = value; }
        }

        #region SettingsHandling

        protected Settings()
        {
            
        }

        private static Settings _instance;
        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Settings();

                    try
                    {
                        string xml = ""+Application.UserAppDataRegistry.GetValue("Settings");
                        if (!string.IsNullOrEmpty(xml))
                        {
                            XmlSerializer xs = new XmlSerializer(typeof(Settings));
                            StringReader sr=new StringReader(xml);
                            _instance = (Settings)xs.Deserialize(sr);
                        }                       
                    }
                    catch
                    {
                    }                    
                }
                return _instance;
            }
        }

        public void Save()
        {
            XmlSerializer xs = new XmlSerializer(typeof(Settings));
            StringWriter sw = new StringWriter();
            xs.Serialize(sw, this);
            Application.UserAppDataRegistry.SetValue("Settings",sw.ToString());
        }

        public static List<string> GetRecentProjects()
        {
            try
            {
                string xml = "" + Application.UserAppDataRegistry.GetValue("RecentProjects");
                if (!string.IsNullOrEmpty(xml))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<string>));
                    StringReader sr = new StringReader(xml);
                    return (List<string>)xs.Deserialize(sr);
                }
            }
            catch { }
            return new List<string>();
        }

        public static void AddRecentProjecy(string prjPath)
        {
            List<string> ls = GetRecentProjects();
            ls.Remove(prjPath);
            ls.Insert(0,prjPath);           

            XmlSerializer xs = new XmlSerializer(typeof(List<string>));
            StringWriter sw = new StringWriter();
            xs.Serialize(sw, ls);
            Application.UserAppDataRegistry.SetValue("RecentProjects", sw.ToString());
        }

        public static void DeleteRecentProject(string prjPath)
        {
            List<string> ls = GetRecentProjects();
            ls.Remove(prjPath);

            XmlSerializer xs = new XmlSerializer(typeof(List<string>));
            StringWriter sw = new StringWriter();
            xs.Serialize(sw, ls);
            Application.UserAppDataRegistry.SetValue("RecentProjects", sw.ToString());
        }

        #endregion

       
    }


    public class SectionAttribute : Attribute
    {
 
    }
    

}
