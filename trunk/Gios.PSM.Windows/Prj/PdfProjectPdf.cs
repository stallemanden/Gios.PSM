using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using Gios.PDF.SplitMerge;

namespace Gios.PDF.SplitMerge
{
    [XmlType("PDF")]
    public class PdfProjectPdf : PdfProjectElement
    {     
        private bool _includeAllPages;
        [XmlAttribute]
        public bool IncludeAllPages
        {
            get { return _includeAllPages; }
            set { _includeAllPages = value; }
        }

        private string _pages;
        [XmlAttribute]
        public string Pages
        {
            get { return _pages; }
            set
            {
                if (value == null)
                    value = "";
                _pages = value;
               
            }
        }


        public int[] CalculatePageSelection(bool checkIncludeAllPages)
        {

            if (checkIncludeAllPages && this.IncludeAllPages)
            {
                List<int> ps = new List<int>();
                for (int n = 0; n < this.PageCount; n++)
                {
                    ps.Add(n);
                }
                return ps.ToArray();
            }

            if (this.Pages == null)
                return null;

            try
            {
                List<int> sel = new List<int>();
                foreach (string val in this.Pages.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                {
                    string[] val2 = val.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (val2.Length > 2 || val2.Length == 0)
                    {
                        break;
                    }
                    int startPage = int.Parse(val2[0].Trim());
                    int endPage = startPage;
                    if (val2.Length == 2)
                    {
                        endPage = int.Parse(val2[1].Trim());
                        if (endPage < startPage)
                        {
                            break;
                        }
                    }
                    for (int x = startPage; x <= endPage; x++)
                    {
                        sel.Add(x-1);
                    }
                }
                return sel.ToArray();
            }
            catch
            {

            }
            return null;

        }

        private int _pagecount=-1;
        public int PageCount
        {
            get
            {
                if (_pagecount < 0)
                {
                    this.Analyze();
                }
                return _pagecount; 
            }
        }

       
        public PdfProjectPdf()
        {
            this.IncludeAllPages = true;
        }

        public PdfProjectPdf(string path)
            : this()
        {
            this.Path = path;
        }
        
                
        public override string Detail
        {
            get
            {
                return "All pages";
            }
        }

        public override void AddToMerger(PdfMerger pdfMerger)
        {
            using (Stream s = new FileStream(this.Path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                PdfParser pa = PdfParser.Parse(s);
                pdfMerger.Add(pa, this.CalculatePageSelection(true));
            }
        }

        public override void Analyze()
        {
            using (Stream s = new FileStream(this.Path, FileMode.Open, FileAccess.Read))
            {
                PdfParser pfa = PdfParser.Parse(s);

                if (pfa.IsEncrypted)
                {
                    throw new PdfEncryptedException(string.Format("{0} is an Encrypted PDF."
                        , System.IO.Path.GetFileName(this.Path)));
                }
                //if (pfa.Version > 1.4)
                //{
                //    throw new PdfDamagedException("Not supported v1.5");
                //}
                //if (pfa.MissingRoot)
                //{
                //    throw new PdfDamagedException("Unable to find root node");
                //}
                this._pagecount = pfa.PageCount;
            }
        }

        public override bool HasPages
        {
            get
            {
                if (this.PageCount == 0)
                    return false;
                //return this.IncludeAllPages || this.Pages.Length > 0;
                return true;
            }
        }
    }
}
