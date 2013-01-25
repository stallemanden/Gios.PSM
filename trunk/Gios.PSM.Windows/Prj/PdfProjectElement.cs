using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using Gios.PDF.SplitMerge;

namespace Gios.PDF.SplitMerge
{
    public abstract class PdfProjectElement
    {
        protected string _error;
        public string Error
        {
            get
            {
                return _error;
            }
        }

        private string _path;
        [XmlAttribute]
        public string Path
        {
            get { return _path; }
            set
            { 
                _path = value;
                if (!string.IsNullOrEmpty(_path))
                {
                    _filename = System.IO.Path.GetFileName(_path);
                }
            }
        }

        private string _filename;
        public string Filename
        {
            get
            {
                return _filename;
            }
        }

        private bool _enabled;
        [XmlAttribute]
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public PdfProjectElement()
        {
            this.Enabled = true;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Path))
            {
                return "";
            }
            return System.IO.Path.GetFileName(this.Path) + (Enabled ? " (enabled)" : " (disabled)");
        }
        public abstract string Detail
        {
            get;
        }

        public abstract bool HasPages
        {
            get;
        }

        public abstract void AddToMerger(PdfMerger pdfMerger);

        public abstract void Analyze();

    }
}
