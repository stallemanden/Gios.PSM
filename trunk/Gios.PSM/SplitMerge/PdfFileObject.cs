using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Security.Cryptography;
using System.IO;

namespace Gios.PDF.SplitMerge
{
    internal class PdfFileObject
    {
        private PdfParser pdfFileParser;

        #region Text Object properties
        
        internal int ObjID;
        internal int OriginalObjectLength;
        internal long OriginalObjectAbsolutePosition;

        private string originalText;
        internal string OriginalText
        {
            get
            {
                if (this.originalText != null)
                {
                    return this.originalText;
                }
                return originalText = System.Text.ASCIIEncoding.ASCII.GetString(this.Buffer);
            }
            set
            {
                this.originalText = value;
            }
        }
        internal int StreamLength = -1;
        internal long StreamPosition = -2;  

        internal string TransformatedTextPart;
        internal int TransformatedObjID;
        internal bool excludedByHashComparison = false;

        internal byte[] Buffer
        {
            get
            {
                byte[] buffer = new byte[this.OriginalObjectLength];
                this.pdfFileParser.inputStream.Seek(this.OriginalObjectAbsolutePosition, SeekOrigin.Begin);
                this.pdfFileParser.inputStream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        internal string StreamContent2Inflated
        {
            get
            {
                byte[] buf = this.GetStreamContent2();
                try
                {         
                    Stream iis = new Ionic.Zlib.ZlibStream(new MemoryStream(buf), Ionic.Zlib.CompressionMode.Decompress);
                    
                    byte[] output = ReadFully(iis);
                    return System.Text.ASCIIEncoding.ASCII.GetString(output);
                   
                }
                catch
                {
                    return null;
                }
            }
        }

       
        
        private byte[] GetStreamContent2()
        {            
            byte[] sc2 = new byte[this.StreamLength];
            this.pdfFileParser.inputStream.Seek(this.StreamPosition, SeekOrigin.Begin);
            this.pdfFileParser.inputStream.Read(sc2, 0, sc2.Length);
            return sc2;
        }
        

        internal string OriginalTextPart
        {
            get
            {
                if (this.Type == PdfObjectTypes.Stream || this.Type==PdfObjectTypes.XRef)
                {
                    return this.OriginalText.Substring(0, (int)(this.StreamPosition - this.OriginalObjectAbsolutePosition));
                }
                return this.OriginalText;
            }
        }
        internal string OriginalTextPartForHash
        {
            get
            {
                return this.OriginalTextPart.TrimStart("1234567890 ".ToCharArray());                
            }
        }

        internal string Hash
        {
            get
            {
                if (this.Type == PdfObjectTypes.Stream)
                {
                    string tph = this.OriginalTextPartForHash;
                    Match m;
                    do
                    {
                        m=Regex.Match(tph, @"(?'id'\d+)\s{1,2}0\s{1,2}R");
                        if (m.Success)
                        {
                            int id = int.Parse(m.Groups["id"].Value);
                            PdfFileObject pfo= this.pdfFileParser.ReadObject(id);
                            if (pfo.Type==PdfObjectTypes.Stream)
                                return null;
                            tph= Regex.Replace(tph, id+@"\s{1,2}0\s{1,2}R", pfo.OriginalTextPartForHash);
                        }
                    }                        
                    while (m.Success);
                    tph = Regex.Replace(tph, @"/Length\s+\d+\s{1,2}0\s{1,2}R", "");
                    if (Regex.IsMatch(tph, @"\d+\s{1,2}0\s{1,2}R"))
                        return null;
                    MD5 md5 = MD5.Create();
                    string hash = Convert.ToBase64String(
                        md5.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(tph)));

                    md5 = MD5.Create();
                    hash += Convert.ToBase64String(md5.ComputeHash(this.StreamContent));
                    return hash;
                }
                return null;
            }
        }


        internal byte[] StreamContent
        {
            get
            {
                byte[] streamContent = new byte[this.StreamLength];
                this.pdfFileParser.inputStream.Seek(this.StreamPosition, SeekOrigin.Begin);
                this.pdfFileParser.inputStream.Read(streamContent, 0, this.StreamLength);
                return streamContent;
            }
        }
     
        private PdfObjectTypes type;
        internal PdfObjectTypes Type
        {
            get
            {
                if (this.type == PdfObjectTypes.UnKnown)
                {
                    
                    if (Regex.IsMatch(this.OriginalText, @"/Pages"))
                    {
                        this.type = PdfObjectTypes.Pages;
                        return this.type;
                    }
                    if (Regex.IsMatch(this.OriginalText, @"/Page"))
                    {
                        this.type = PdfObjectTypes.Page;
                        return this.type;
                    }
                    if (Regex.IsMatch(this.OriginalText, @"/XRef"))
                    {
                        this.type = PdfObjectTypes.XRef;
                        return this.type;
                    }
                    if (Regex.IsMatch(this.OriginalText, @"/Type\s*/ObjStm"))
                    {
                        this.type = PdfObjectTypes.ObjStm;
                        return this.type;
                    }
                    if (Regex.IsMatch(this.OriginalText, @"endstream"))
                    {
                        this.type = PdfObjectTypes.Stream;
                        return this.type;
                    }
                   
                    this.type = PdfObjectTypes.Other;
                }
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }

        internal ArrayList Kids
        {
            get
            {
                if (this.Type != PdfObjectTypes.Pages)
                {
                    return new ArrayList();
                }
                return this.GetChildObjects("kids");
            }
        }

        internal ArrayList GetAllSubPages()
        {
            ArrayList al = new ArrayList();
            foreach (PdfFileObject child in this.Kids)
            {
                switch (child.Type)
                {
                    case PdfObjectTypes.Page:
                        al.Add(child);
                        break;
                    case PdfObjectTypes.Pages:
                        al.AddRange(child.GetAllSubPages());
                        break;
                }
            }
            return al;
        }

        #endregion

        #region constructor

        internal PdfFileObject(PdfParser pdfFileParser)
        {
            this.pdfFileParser = pdfFileParser;
        }

        #endregion
       
        #region ToString

        public override string ToString()
        {
            switch (this.Type)
            {                
                default:
                    return this.Type + " of "+OriginalObjectLength+" bytes: " + this.OriginalText;
            }
        }

        #endregion

        #region analysis

        internal PdfFileObject GetChildObject(string key)
        {
            Match m = Regex.Match(this.OriginalText, @"/" + key + @" (?'id'\d+)", RegexOptions.ExplicitCapture);
            if (m.Success)
            {
                return this.pdfFileParser.ReadObject(int.Parse(m.Groups["id"].Value));
            }
            return null;
        }

        internal int? GetInt32Value(string keyName)
        {
            string pattern = @"/" + keyName + @"\s+(?'id'\d+)";
            Match m = Regex.Match(this.OriginalText, pattern, RegexOptions.ExplicitCapture);
            if (m.Success)
            {
                return int.Parse(m.Groups[1].Value);
            }
            return null;
        }



        internal ArrayList GetChildObjects(string key)
        {
            ArrayList children = new ArrayList();
            Match m = Regex.Match(this.OriginalText, @"/" + key + @"\s*\[(\s*(?'id'\d+) 0 R\s*)*"
                , RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
            foreach (Capture cap in m.Groups["id"].Captures)
            {
                children.Add(this.pdfFileParser.ReadObject(int.Parse(cap.Value)));
            }
            return children;
        }


        static Regex regex1 = new Regex(@"(?'parent'(/Parent)*)\s*(?'id'\d+) 0 R[^G]", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        internal void PopulateRelatedObjects(Hashtable container)
        {
            if (!container.ContainsKey(this.ObjID))
            {
                container.Add(this.ObjID, this);
                Match m = regex1.Match(this.OriginalText);
                while (m.Success)
                {
                    int num = int.Parse(m.Groups["id"].Value);
                    bool notparent = m.Groups["parent"].Length == 0;
                    if (notparent)
                    {
                        if (!container.Contains(num))
                        {
                            PdfFileObject pfo = this.pdfFileParser.ReadObject(num);
                            if (pfo != null)
                            {
                                pfo.PopulateRelatedObjects(container);
                            }
                        }
                    }
                    m = m.NextMatch();
                }
            }
        }
        
        #endregion

        #region Transformation Process

        private Hashtable TransformationTable;
        private string FilterEval(Match m)
        {
            int id = int.Parse(m.Groups["id"].Value);
            string end = m.Groups["end"].Value;
            if (this.TransformationTable.ContainsKey(id))
            {
                string rest = m.Groups["rest"].Value;
                return (int)TransformationTable[id] + rest + end;
            }            
            return end;
        }
        internal PdfFileObject GetParent()
        {
            return this.GetChildObject("Parent");           
        }
        internal string MediaBoxText
        {
            get
            {
                string pattern = @"/MediaBox\s*\[\s*(\+|-)?\d+(.\d+)?\s+(\+|-)?\d+(.\d+)?\s+(\+|-)?\d+(.\d+)?\s+(\+|-)?\d+(.\d+)?\s*]";
                return Regex.Match(this.OriginalTextPart, pattern).Value;
            }
        }

        MatchEvaluator filterEval;
        bool transformed = false;
        internal virtual void Transform(Hashtable TransformationTable)
        {
            if (transformed)
            {
                throw new Exception("Already transformed!");
            }
            transformed = true;
            if (this.Type == PdfObjectTypes.Page)
            {

            }
            this.TransformationTable = TransformationTable;
            this.TransformatedObjID = (int)this.TransformationTable[this.ObjID];
           
            this.filterEval = new MatchEvaluator(this.FilterEval);

            this.TransformatedTextPart = this.OriginalTextPart;

            if (this.Type == PdfObjectTypes.Page && this.MediaBoxText == "")
            {
                PdfFileObject parent = this.GetParent();
                while (parent != null)
                {
                    string mb = parent.MediaBoxText;
                    if (mb == "")
                    {
                        parent = parent.GetParent();
                    }
                    else
                    {
                        this.TransformatedTextPart = Regex.Replace(this.TransformatedTextPart, @"/Type\s*/Page", "/Type /Page\r" + mb);
                        parent = null;
                    }
                }
            }
            this.TransformatedTextPart = Regex.Replace(this.TransformatedTextPart
            , @"(?'id'\d+)(?'rest' 0 (obj|R))(?'end'[^G])", this.filterEval, RegexOptions.ExplicitCapture);
            this.TransformatedTextPart = Regex.Replace(this.TransformatedTextPart
                , @"/Parent\s+(\d+ 0 R)*", "/Parent 2 0 R \r");
        }

        #endregion

        internal List<PdfFileObject> GetStreamObjects()
        {
            List<PdfFileObject> objects = new List<PdfFileObject>();
            if (this.Type != PdfObjectTypes.ObjStm)
                return objects;

            string txt = this.StreamContent2Inflated;
            if (txt == null)
                return new List<PdfFileObject>();

            int n = this.GetInt32Value("N").Value;

            string regex = "";
            for (int x = 0; x < n; x++)
            {
                regex += @"(\d+)\s+(\d+)\s+";
            }

            Match m = Regex.Match(txt, regex);

            for (int x = 0; x < n; x++)
            {
                PdfFileObject pfo = new PdfFileObject(this.pdfFileParser);
                pfo.ObjID = int.Parse(m.Groups[x * 2 + 1].Value);
                int position = int.Parse(m.Groups[x * 2 + 2].Value);
                if (x == n - 1)
                {
                    pfo.OriginalText = pfo.ObjID + " 0 obj " + txt.Substring(m.Length + position) + "\nendobj\n";
                }
                else
                {
                    int length = int.Parse(m.Groups[x * 2 + 4].Value) - position;
                    pfo.OriginalText = pfo.ObjID + " 0 obj " + txt.Substring(m.Length + position, length) + "\nendobj\n";
                }
                objects.Add(pfo);
            }


            return objects;
        }

        internal long WriteToStream(Stream Stream)
        {           

            StreamWriter sw = new StreamWriter(Stream, Encoding.ASCII);

            if (this.Type == PdfObjectTypes.Stream)
            {
                sw.Write(this.TransformatedTextPart);
                sw.Flush();
                Stream.Write(this.StreamContent, 0, this.StreamContent.Length);
                string sf="endstream\rendobj\n";
                sw.Write(sf);
                sw.Flush();
                return this.TransformatedTextPart.Length + this.StreamContent.Length + sf.Length;
            }

            sw.Write(this.TransformatedTextPart);
            sw.Flush();
            return this.TransformatedTextPart.Length;
        }

        internal static byte[] ReadFully(Stream stream)
        {
            byte[] buffer = new byte[32768];
            using (MemoryStream ms = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                        return ms.ToArray();
                    ms.Write(buffer, 0, read);
                }
            }
        }


    }

    internal class PdfFileObjectNumberComparer : IComparer
    {
        #region IComparer Members

        public int Compare(object x, object y)
        {
            PdfFileObject a = x as PdfFileObject;
            PdfFileObject b = y as PdfFileObject;
            return a.ObjID.CompareTo(b.ObjID);
        }

        #endregion
    }
}
