using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Gios.PDF.SplitMerge
{
    [XmlType("Project")]
    public class PdfProject
    {
        #region properties

        private string _path="";
        [XmlIgnore]
        public string Path
        {
            get { return _path; }
            set
            {                
                _path = value;                
            }
        }

        private string _target="" ;
        [XmlAttribute]
        public string Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public string TempTarget
        {
            get
            {
                if (string.IsNullOrEmpty(this.Target))
                    return "";
                return this.Target + ".tmp";
            }
        }

        private bool _alwaysAskDestinationFile;
        [XmlAttribute("AskDest")]
        public bool AlwaysAskDestinationFile
        {
            get { return _alwaysAskDestinationFile; }
            set { _alwaysAskDestinationFile = value; }
        }

        private bool _overwriteDestinationWithoutPrompt;
        [XmlAttribute("Overwrite")]
        public bool OverwriteDestinationWithoutPrompt
        {
            get { return _overwriteDestinationWithoutPrompt; }
            set { _overwriteDestinationWithoutPrompt = value; }
        }

        private bool _openWithDefaultReaderAfterCreation;
        [XmlAttribute("OpenAfterCreate")]
        public bool OpenWithDefaultReaderAfterCreation
        {
            get { return _openWithDefaultReaderAfterCreation; }
            set { _openWithDefaultReaderAfterCreation = value; }
        }

        private List<PdfProjectElement> _elements;
        [XmlArrayItem(typeof(PdfProjectPdf))]
        [XmlArrayItem(typeof(PdfProjectImage))]
        public List<PdfProjectElement> Elements
        {
            get
            {
                return this._elements;
            }
            set
            {
                this._elements = value;
            }
        }

        #endregion

        [XmlIgnore]
        public string ProjectXML
        {
            get
            {
                XmlSerializer xs = new XmlSerializer(typeof(PdfProject));
                XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
                xsn.Add("", "");
                StringWriter sw1 = new StringWriter();
                xs.Serialize(sw1, this,xsn);
                return sw1.ToString();
            }            
        }

        [XmlIgnore]
        public string ProjectMD5
        {
            get
            {
                return Convert.ToBase64String(MD5.Create().ComputeHash(ASCIIEncoding.ASCII.GetBytes(this.ProjectXML)));
            }
        }

        [XmlIgnore]
        public bool HasPages
        {
            get
            {
                foreach (PdfProjectElement ppe in this.Elements)
                {
                    if (ppe.Enabled && ppe.HasPages)
                        return true;
                }
                return false;
            }
        }


        public static PdfProject BlankProject
        {
            get            
            {
                PdfProject prj = new PdfProject();
                prj.OpenWithDefaultReaderAfterCreation = true;
                prj.OverwriteDestinationWithoutPrompt = false;
                prj.AlwaysAskDestinationFile = false;

                return prj;
            }
        }


        

        public PdfProject()
        {
            this._elements = new List<PdfProjectElement>();
            //this._listElements = new List<PdfProjectElementListViewItem>();
        }

        public void Save(string path)
        {
            File.WriteAllText(path, this.ProjectXML,Encoding.Unicode);
        }

        [XmlIgnore]
        public string Title
        {
            get
            {
                if (string.IsNullOrEmpty(this.Path))
                {
                    return " New Project ";
                }
                return System.IO.Path.GetFileName(this.Path);
            }
        }

        public static PdfProject Load(string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(PdfProject));

            using (Stream s = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                PdfProject pp = (PdfProject)xs.Deserialize(s);
                pp.Path = path;
                return pp;
            }

        }

        internal void Save()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
