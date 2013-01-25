using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Reflection;

namespace Gios.PDF.SplitMerge
{
    internal class PdfStream : PdfObject
	{
        internal ArrayList StreamObjectElements;
		private double pageHeight;
        internal PdfStream(PdfPage PdfPage)
            :base(PdfPage.PdfDocument)
		{
            //this.deflateCompression = PdfPage.PdfDocument.DeflateCompression;
			this.pageHeight=PdfPage.Height;
			this.StreamObjectElements=new ArrayList();
		}

        internal override int Write(Stream s)
        {
            int pos = 0;

            using (MemoryStream ms = new MemoryStream())
            {
                Stream content = ms;

                //if (deflateCompression && compressionType != null)
                //{
                //    content = (Stream)Activator.CreateInstance(compressionType, new object[] { ms });
                //}

                using (content)
                {
                    PdfStreamWriter lw = new PdfStreamWriter(content, this.pageHeight);
                    PdfStreamElement lastElement = null;
                  
                    foreach (PdfStreamElement plo in this.StreamObjectElements)
                    {
                        lastElement = lw.Write(plo, lastElement);
                    }
                    lw.Finish(lastElement);
                    content.Flush();
                    //if (deflateCompression && compressionType != null)
                    //{
                    //    compressionType.InvokeMember("Finish", BindingFlags.InvokeMethod
                    //    , Type.DefaultBinder, content, new object[0]);
                    //}

                    pos += Utility.Write(s, this.HeadObj, true);
                    pos += Utility.Write(s, "<< "+ 
                        //(deflateCompression ? "/Filter /FlateDecode " : 
                        " "
                        //)
                        + " /Length " + ms.Length + " >>", true);
                    pos += Utility.Write(s, "stream", true);

                    ms.WriteTo(s);
                    pos += (int)ms.Length;
                }

            }

            pos += Utility.Write(s, "", true);
            pos += Utility.Write(s, "endstream", true);
            pos += Utility.Write(s, "endobj", true);


            return pos;
        }

	}
}
