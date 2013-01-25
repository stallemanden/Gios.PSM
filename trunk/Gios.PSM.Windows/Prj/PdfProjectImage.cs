using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Drawing.Printing;
using System.Drawing;
using Gios.PDF.SplitMerge;

namespace Gios.PDF.SplitMerge
{
    [XmlType("Image")]
    public class PdfProjectImage : PdfProjectElement
    {     

        public PdfProjectImage()
        { }

        public PdfProjectImage(string path)
            : this()
        {
            this.Path = path;
        }

        public override string Detail
        {
            get
            {
                return "Full Screen";
            }
        }

        public override void AddToMerger(PdfMerger pdfMerger)
        {
            pdfMerger.Add(PdfParser.Parse(ImageToPDFConverter.ConvertImageToPDF(this.Path)));          
        }

        public override void Analyze()
        {
            
        }

        public override bool HasPages
        {
            get { return true; }
        }
    }
}
