using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Gios.PDF.SplitMerge
{
    public class PdfParser
    {
        #region fields and properties

        internal Stream inputStream;
        long startxref = -1;

        private double _version = -1;
        public double Version
        {
            get
            {

                return this._version;
            }
        }

        internal Dictionary<int, PdfFileObject> objectMemory;

        private PdfFileObject trailer, rootPages, root;

        private bool isEncrypted;
        public bool IsEncrypted
        {
            get
            {
                return isEncrypted;
            }
        }


        internal PdfFileObject[] GetAllPages()
        {
            return (PdfFileObject[])this.rootPages.GetAllSubPages().ToArray(typeof(PdfFileObject));
        }

        private int pageCount;
        public int PageCount
        {
            get
            {
                return pageCount;
            }
        }


        #endregion

        internal PdfFileObject ReadObject(int objID)
        {
            return (PdfFileObject)this.objectMemory[objID];
        }

        public PdfParser()
        {
        }

        public static PdfParser Parse(Stream InputStream)
        {
            PdfParser analyzer = new PdfParser();
            analyzer.inputStream = InputStream;
            analyzer.objectMemory = new Dictionary<int, PdfFileObject>();

            analyzer.inputStream.Seek(0, SeekOrigin.Begin);
            string ver = new StreamReader(analyzer.inputStream).ReadLine().Substring(5);
            analyzer._version = double.Parse(ver, System.Globalization.CultureInfo.InvariantCulture);

            analyzer.startxref = GetStartxref(analyzer.inputStream);
            analyzer.objectMemory = analyzer.LoadObjects(analyzer.inputStream);           

            if (analyzer.trailer == null)
            {
                analyzer.trailer = analyzer.ParseTrailer(analyzer.startxref);
            }

            analyzer.root = analyzer.trailer.GetChildObject("Root");
            analyzer.root.Type = PdfObjectTypes.Root;
            analyzer.rootPages = analyzer.root.GetChildObject("Pages");
            analyzer.isEncrypted = analyzer.trailer.GetChildObject("Encrypt") != null;
            analyzer.pageCount = analyzer.rootPages.GetInt32Value("Count").Value;

            return analyzer;
        }

        private static long GetStartxref(Stream inputStream)
        {
            StreamReader sr = new StreamReader(inputStream);
            inputStream.Seek(inputStream.Length - 40, SeekOrigin.Begin);
            string line = "";
            while (!line.StartsWith("startxref"))
            {
                line = sr.ReadLine();
            }
            long startxref = long.Parse(sr.ReadLine());
            if (startxref == -1)
                throw new Exception("Cannot find the startxref");
            return startxref;
        }

        private PdfFileObject ParseTrailer(long xref)
        {
            PdfFileObject pfo = new PdfFileObject(this);
            pfo.OriginalText = "";
            pfo.ObjID = 0;
            pfo.Type = PdfObjectTypes.Trailer;

            this.inputStream.Seek(xref, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(this.inputStream);
            string line;
            bool istrailer = false;
            while ((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                if (line == "startxref")
                {
                    return pfo;
                }
                if (line.StartsWith("trailer"))
                {
                    pfo.OriginalText = "";
                    istrailer = true;
                }
                if (istrailer)
                {
                    pfo.OriginalText += line + "\r";
                }
            }

            throw new Exception("Cannot find trailer");
        }

        int lastObjID = 0;
        internal Dictionary<int, PdfFileObject> LoadObjects(Stream s)
        {
            s.Seek(0, SeekOrigin.Begin);
            Dictionary<int, PdfFileObject> objects = new Dictionary<int, PdfFileObject>();
            int c = 0, objIDCandidate = 0, streamLengthCandidate = -1;
            long streamPositionCandidate = -2;
            long addressCandidate = -1, step = 0, position = 0;
            ObjectAddress oa = null;
            while ((c = s.ReadByte()) != -1)
            {               
                switch (step)
                {
                    case 0:
                        if (oa != null)
                        {
                            if (c == ' ' || c == '\r' || c == '\n')
                            {
                                oa.Length++;
                            }
                            else
                            {
                                PdfFileObject pfo = oa.CreateFileObject(this);
                                if (pfo.OriginalObjectAbsolutePosition == this.startxref)
                                {
                                    this.trailer = pfo;
                                }

                                objects[oa.ObjID] = pfo;
                                this.inputStream.Seek(position+1, SeekOrigin.Begin);
                                oa = null;
                                streamPositionCandidate = -2;
                                streamLengthCandidate = -1;
                            }
                        }
                        if (c > 48 && c < 58)
                        {
                            addressCandidate = position;
                            objIDCandidate = c - 48;
                            step = 1;
                        }
                        break;
                    case 1:
                        if (c > 47 && c < 58)
                        {
                            objIDCandidate = objIDCandidate * 10 + c - 48;                           
                        }
                        else
                        {
                            step = (c == ' ') ? 2 : 0;
                        }
                        break;
                    case 2:
                        step = (c == '0') ? 3 : 0;
                        break;
                    case 3:
                        step = (c == ' ') ? 4 : 0;
                        break;
                    case 4:
                        step = (c == 'o') ? 5 : 0;
                        break;
                    case 5:
                        step = (c == 'b') ? 6 : 0;
                        break;
                    case 6:
                        if (c == 'j')
                        {
                            step = 7;
                           
                        }
                        else
                        {
                            step = 0;
                        }
                        break;
                    case 7:
                        
                        if (objIDCandidate !=lastObjID)
                        {
                            lastObjID = objIDCandidate;
                            
                        }

                        if (objIDCandidate == 8)
                        {
                            if (c == '>')
                            {

                            }
                        }
                       
                        if (c == 'e')
                        {
                            // possible beginning of stream
                            step = 29;
                        }
                        if (c == 's')
                        {
                            step = 9;
                            break;
                        }
                        break;
                    case 8:
                        if (c == 's')
                        {
                            step++;
                            break;
                        }
                        if (c == 'e')
                        {
                            step = 29;
                            break;
                        }
                        if (c == '\r' || c == '\n' || c == ' ')
                        {
                            break;
                        }
                        step = 7;
                        break;
                    case 9:
                        step = (c == 't') ? 10 : 7;
                        break;
                    case 10:
                        step = (c == 'r') ? 11 : 7;
                        break;
                    case 11:
                        step = (c == 'e') ? 12 : 7;
                        break;
                    case 12:
                        step = (c == 'a') ? 13 : 7;
                        break;
                    case 13:
                        step = (c == 'm') ? 14 : 7;
                        break;
                    case 14:
                        step = (c == '\r' || c == '\n' || c == ' ') ? 15 : 7;
                        break;
                    case 15:
                        if (c == '\r' ||c  == '\n' || c == ' ')
                        {

                        }
                        else
                        {
                            step++;
                            streamPositionCandidate = position;
                        }
                        break;
                    case 16:
                        if (c == 'e')
                        {
                            step++;
                            break;
                        }
                       
                        streamLengthCandidate = (int)(position - streamPositionCandidate + 1);
                        break;
                    case 17:
                        step = (c == 'n') ? 18 : 16;
                        break;
                    case 18:
                        step = (c == 'd') ? 19 : 16;
                        break;
                    case 19:
                        if (c == 'o')
                        {
                            step = 32;
                            break;
                        }
                        if (c == 's')
                        {
                            step = 20;
                            break;
                        }
                        step = 16;
                        break;
                    case 20:
                        step = (c == 't') ? 21 : 16;
                        break;
                    case 21:
                        step = (c == 'r') ? 22 : 16;
                        break;
                    case 22:
                        step = (c == 'e') ? 23 : 16;
                        break;
                    case 23:
                        step = (c == 'a') ? 24 : 16;
                        break;
                    case 24:
                        step = (c == 'm') ? 25 : 16;
                        break;
                    case 25:
                        step = (c == '\r' || c == '\n' || c == ' ') ? 28 : 16;
                        break;
                        

                    case 28:
                        step = (c == 'e') ? 29 : 7;
                        break;
                    case 29:
                        step = (c == 'n') ? 30 : 7;
                        break;
                    case 30:
                        step = (c == 'd') ? 31 : 7;
                        break;
                    case 31:
                        step = (c == 'o') ? 32 : 7;
                        break;
                    case 32:
                        step = (c == 'b') ? 33 : 7;
                        break;
                    case 33:
                        if (c == 'j')
                        {
                            step = 0;
                            if (objIDCandidate == 140)
                            {

                            }
                            oa = new ObjectAddress(objIDCandidate, addressCandidate
                                ,(int)( position - addressCandidate+1),streamPositionCandidate,streamLengthCandidate);
                           
                        }
                        else
                        {
                            step = 7;
                        }
                        break;



                }
                position++;
            }

            List<PdfFileObject> ObjStms = new List<PdfFileObject>();
            foreach (PdfFileObject pfo in objects.Values)
            {
                if (pfo.Type == PdfObjectTypes.ObjStm)
                {
                    ObjStms.Add(pfo);
                }
            }
            foreach (PdfFileObject ObjStm in ObjStms)
            {
                foreach (PdfFileObject pfo2 in ObjStm.GetStreamObjects())
                {
                    objects[pfo2.ObjID] = pfo2;
                }
            }
            return objects;
        }

    }

    class ObjectAddress
    {
        internal ObjectAddress(int objID, long address, int length,long streamPosition,int streamLength)
        {
            this.Address = address;
            this.Length = length;
            this.ObjID = objID;
            this.StreamPosition = streamPosition;            
            this.StreamLength = streamLength;
        }
        internal int ObjID;
        internal long Address;
        internal int Length;
        internal int StreamLength;
        internal long StreamPosition;

        internal PdfFileObject CreateFileObject(PdfParser analyzer)
        {
            PdfFileObject pfo = new PdfFileObject(analyzer);
            pfo.ObjID = this.ObjID;
            pfo.OriginalObjectAbsolutePosition = this.Address;
            pfo.OriginalObjectLength = this.Length;
            pfo.StreamPosition = this.StreamPosition;
            pfo.StreamLength = this.StreamLength;           
            return pfo;
        }

        public override string ToString()
        {
            return this.Address + " (" + this.Length + " bytes)";
        }
    }


}



