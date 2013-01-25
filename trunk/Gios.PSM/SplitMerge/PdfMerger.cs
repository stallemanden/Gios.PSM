using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace Gios.PDF.SplitMerge
{
    public class PdfMerger
    {
        int elementCount = 0;
        Stream target;
        long pos = 15;

        private int number = 2;
        private List<long> xrefs;
        private List<int> pageNumbers;       

        private bool _optimizeStreams=true;
        public bool OptimizeStreams
        {
            get { return _optimizeStreams; }
            set { _optimizeStreams = value; }
        }

        private bool _cancelPending;
        public bool CancelPending
        {
            get { return _cancelPending; }
            set { _cancelPending = value; }
        }

        public event EventHandler<PdfMergerProgressEventArgs> PdfMergerProgress;
        protected void OnPdfMergerProgress(object sender, PdfMergerProgressEventArgs e)
        {

        }

        Dictionary<string,int> alreadyUsedObjects;

        public PdfMerger(Stream OutputStream)
        {
            this.OptimizeStreams = false;

            this.PdfMergerProgress = new EventHandler<PdfMergerProgressEventArgs>(this.OnPdfMergerProgress);

            this.xrefs = new List<long>(); ;
            this.pageNumbers = new List<int>();
            this.alreadyUsedObjects = new Dictionary<string, int>();
            this.target = OutputStream;

            StreamWriter sw = new StreamWriter(this.target);
            sw.Write("%PDF-1.4\r");
            sw.Flush();
            byte[] buffer = new byte[] { 0x25, 0xE2, 0xE3, 0xCF, 0xD3, 0x0D, 0x0A };
            this.target.Write(buffer, 0, buffer.Length);
            this.target.Flush();
        }

        public void Add(PdfParser pdfParser)
        {
            this.Add(pdfParser, null);
        }

        public void Add(PdfParser pdfParser, int[] PageNumbers)
        {            
            if (this.CancelPending)
                return;

            this.PdfMergerProgress(this, new PdfMergerProgressEventArgs(this.elementCount,
                        0, 0, "Analyzing PDF Structure"));


            PdfFileObject[] pages = pdfParser.GetAllPages();

            ArrayList selectedPages = new ArrayList();

            #region gets needed objects
                        
            Hashtable relatedObjects = new Hashtable();

            if (PageNumbers == null)
            {
                List<int> ps = new List<int>();
                for (int p = 0; p < pages.Length; p++)
                {
                    ps.Add(p);
                }
                PageNumbers = ps.ToArray();
            }


            int currentPageIndex = 1, pageCount = PageNumbers.Length;

            int step = Math.Max(pageCount / 20, 10);

            foreach (int pageNumber in PageNumbers)
            {
                if (this.CancelPending)
                    return;

                if (currentPageIndex % step == 0 || currentPageIndex == pageCount)
                {
                    this.PdfMergerProgress(this, new PdfMergerProgressEventArgs(this.elementCount,
                        currentPageIndex, pageCount, "Analyzing Page {0} of {1}"));
                }
                PdfFileObject selectedPage = pages[pageNumber];
                selectedPages.Add(selectedPage);
                selectedPage.PopulateRelatedObjects(relatedObjects);
                currentPageIndex++;
            }

            ArrayList neededObjects = new ArrayList();
            neededObjects.AddRange(relatedObjects.Values);
            neededObjects.Sort(new PdfFileObjectNumberComparer());

            #endregion

            #region creates IDs transformation table

            int objectIndex = 1;
            int objectCount = neededObjects.Count;

            step = Math.Max(objectCount / 20, 50);

            Hashtable transformationTable = new Hashtable();
            foreach (PdfFileObject pfo in neededObjects)
            {
                if (this.CancelPending)
                    return;

                if (objectIndex % step == 0 || objectIndex == objectCount)
                {
                    this.PdfMergerProgress(this, new PdfMergerProgressEventArgs(this.elementCount,
                     objectIndex, objectCount, "Rebuilding Indexes for object {0} of {1}"));
                }

                string hash = OptimizeStreams?pfo.Hash:null;
                if (hash != null && this.alreadyUsedObjects.ContainsKey(hash))
                {
                    pfo.excludedByHashComparison = true;
                    transformationTable.Add(pfo.ObjID, (int)this.alreadyUsedObjects[hash]);
                }
                else
                {
                    number++;
                    transformationTable.Add(pfo.ObjID, number);
                    if (hash != null)
                    {
                        this.alreadyUsedObjects.Add(hash, number);
                    }
                }
                objectIndex++;
            }

            #endregion

            objectIndex = 1;
           
            foreach (PdfFileObject pfo in neededObjects)
            {
                if (this.CancelPending)
                    return;

                if (objectIndex % step == 0 || objectIndex==objectCount)
                {
                    this.PdfMergerProgress(this, new PdfMergerProgressEventArgs(this.elementCount,
                    objectIndex, objectCount, "Writing object {0} of {1}"));
                }

                pfo.Transform(transformationTable);
                if (!pfo.excludedByHashComparison)
                {
                    this.xrefs.Add(pos);
                    this.pos += pfo.WriteToStream(this.target);
                }
                pfo.TransformatedTextPart = null;
                objectIndex++;
            }

            foreach (PdfFileObject selectedPage in selectedPages)
            {                
                this.pageNumbers.Add(selectedPage.TransformatedObjID);
            }


            this.elementCount++;
        }

        public void Finish()
        {

            StreamWriter sw = new StreamWriter(this.target);

            string root = "";
            root = "1 0 obj\r";
            root += "<< \r/Type /Catalog \r";
            root += "/Pages 2 0 R \r";
            root += ">> \r";
            root += "endobj\r";

            xrefs.Insert(0, pos);
            pos += root.Length;
            sw.Write(root);

            StringBuilder pages = new StringBuilder();
            pages.Append("2 0 obj \r");
            pages.Append("<< \r");
            pages.Append("/Type /Pages \r");
            pages.Append("/Count " + pageNumbers.Count + " \r");
            pages.Append("/Kids [ ");
            foreach (int pageIndex in pageNumbers)
            {
                pages.Append(pageIndex + " 0 R ");
            }
            pages.Append("] \r");
            pages.Append(">> \r");
            pages.Append("endobj\r");

            xrefs.Insert(1, pos);
            pos += pages.Length;
            sw.Write(pages.ToString());


            sw.Write("xref\r");
            sw.Write("0 " + (this.xrefs.Count + 1) + " \r");
            sw.Write("0000000000 65535 f \r");

            foreach (long xref in this.xrefs)
                sw.Write((xref + 1).ToString("0000000000") + " 00000 n \r");
            sw.Write("trailer\r");
            sw.Write("<<\r");
            sw.Write("/Size " + (this.xrefs.Count + 1) + "\r");
            sw.Write("/Root 1 0 R \r");
            sw.Write(">>\r");
            sw.Write("startxref\r");
            sw.Write((pos + 1) + "\r");
            sw.Write("%%EOF\r");
            sw.Flush();
            sw.Close();


        }

       
    }
}
