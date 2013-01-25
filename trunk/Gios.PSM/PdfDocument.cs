//============================================================================
//Gios Pdf.NET - A library for exporting Pdf Documents in C#
//Copyright (C) 2005  Paolo Gios - www.paologios.com
//
//This library is free software; you can redistribute it and/or
//modify it under the terms of the GNU Lesser General Public
//License as published by the Free Software Foundation; either
//version 2.1 of the License, or (at your option) any later version.
//
//This library is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//Lesser General internal License for more details.
//
//You should have received a copy of the GNU Lesser General Public
//License along with this library; if not, write to the Free Software
//Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//=============================================================================

using System; 
using System.Collections;
using System.IO;
using System.Text;
using System.Drawing;
using System.Globalization;
using System.Drawing.Imaging;

namespace Gios.PDF.SplitMerge
{
	/// <summary>
	/// The base Pdf Document.
	/// </summary>
	internal class PdfDocument
	{
		#region properties
        		
		private int _nextid=0;
        internal int GetNextId()
        {
            return ++_nextid;
        }       

		internal Hashtable images;
        internal ArrayList PdfObjects;

        internal int pageCount=0;

        PdfDocumentFormat defaultPageFormat;
        internal PdfDocumentFormat DefaultPageFormat
        {
            get
            {
                return this.defaultPageFormat;
            }
        }
				
		      
		#endregion        

        #region constructors

        internal PdfDocument(PdfDocumentFormat DefaultPageFormat, string subject, string title, string author)
		{
			this.defaultPageFormat=DefaultPageFormat;
            this.PdfObjects = new ArrayList();
            this.images = new Hashtable();
            new PdfHeader(this, subject, title, author);
            new PdfCatalog(this);
            new PdfRoot(this);
        }

        #endregion

        #region Save Methods

        internal void Save(Stream stream)
		{
            int index = 0;
            while(index<this.PdfObjects.Count)
            {
                PdfPage element = this.PdfObjects[index] as PdfPage;
                if (element != null)
                {
                    foreach (PdfStreamElement element2 in ((PdfPage)element).PdfStream.StreamObjectElements)
                    {
                        if (element2.RequiredFonts == null)
                            continue;
                    }
                }
                index++;
            }

			StreamWriter streamWriter=new StreamWriter(stream);		
            streamWriter.WriteLine("%PDF-1.4");
            streamWriter.WriteLine("%âãÏÓ");
            streamWriter.Flush();

			ArrayList xrefs=new ArrayList();
            long pos = 10;

            this.PdfObjects.Sort(new PdfObjectComparer());
            foreach (PdfObject po in this.PdfObjects)
            {
                xrefs.Add(((double)pos).ToString("0000000000", CultureInfo.InvariantCulture) + " 00000 n");
                int size = po.Write(stream);
                pos += size;
            }		
			streamWriter.WriteLine("xref");
			streamWriter.Write("0 ");
			streamWriter.WriteLine(this.PdfObjects.Count);
			streamWriter.WriteLine("0000000000 65535 f");			
			foreach (string xr in xrefs)
                streamWriter.WriteLine(xr);
			streamWriter.WriteLine("trailer");
			streamWriter.WriteLine("<<");
			streamWriter.Write("/Size ");
			streamWriter.WriteLine(PdfObjects.Count);
			streamWriter.WriteLine("/Root 2 0 R");
			streamWriter.WriteLine("/Info 1 0 R");
			streamWriter.WriteLine(">>");
			streamWriter.WriteLine("startxref");
			streamWriter.WriteLine(pos);
			streamWriter.WriteLine("%%EOF");
			streamWriter.Flush();
		}
              
		/// <summary>
		/// Outputs the complete PDF Document to a file
		/// </summary>
		/// <param name="file"></param>
		internal void Save(string path)
		{
            using (Stream filsStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                this.Save(filsStream);
            }
        }

        #endregion
       
        #region internal adds

        internal PdfImage NewImage(byte[] imageData,string hash)
        {
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                using (Image i = Image.FromStream(ms))
                {
                    using (MemoryStream ms2 = new MemoryStream())
                    {
                        i.Save(ms2, ImageFormat.Jpeg);
                        PdfImage pi = new PdfImage(this, ms2.ToArray(), i.Width, i.Height,hash);
                        return pi;
                    }
                }
            }
        }
        #if LIB
        internal void AddFont(Font f)
        {            
            int hashCode = Utility.GetFontHashCode(f);
            if (this.fonts.ContainsKey(hashCode))
                return;

            PdfFont pf = new PdfFont(this,f);

            this.fonts.Add(hashCode, pf);
        }
#endif
        #endregion

        #region internal methods

        internal PdfPage NewPage()
		{
            return this.NewPage(this.DefaultPageFormat);
		}

        internal PdfPage NewPage(PdfDocumentFormat PdfDocumentFormat)
		{
			return new PdfPage(this,PdfDocumentFormat);
        }

        #endregion

        #region table stuff
        /// <summary>
		/// Instantiates a new PdfTable setting the default specs.
		/// </summary>
		/// <param name="DefaultContentAlignment"></param>
		/// <param name="DefaultFont"></param>
		/// <param name="DefaultForegroundColor"></param>
		/// <param name="Rows"></param>
		/// <param name="Columns"></param>
		/// <param name="CellPadding"></param>
		/// <returns></returns>
        //internal PdfTable NewTable(ContentAlignment DefaultContentAlignment,Font DefaultFont,Color DefaultForegroundColor,int Rows
        //    ,int Columns,double CellPadding)
        //{
        //    if (Rows<=0) throw new Exception("Rows must be grater than zero.");
        //    if (Columns<=0) throw new Exception("Columns must be grater than zero.");
        //    if (CellPadding<0) throw new Exception("CellPadding must be non-negative.");
        //    PdfTable pt=new PdfTable(this,DefaultContentAlignment,DefaultFont,DefaultForegroundColor,Rows
        //        ,Columns,CellPadding);
        //    pt.header=new PdfTable(this,ContentAlignment.MiddleCenter,DefaultFont,Color.Black,1
        //        ,Columns,CellPadding);
        //    this.AddFont(DefaultFont);
        //    return pt;
        //}
		/// <summary>
		/// Instantiates a new PdfTable setting the default specs.
		/// </summary>
		/// <param name="DefaultFont"></param>
		/// <param name="Rows"></param>
		/// <param name="Columns"></param>
		/// <param name="CellPadding"></param>
		/// <returns></returns>
        //internal PdfTable NewTable(Font DefaultFont,int Rows,int Columns,double CellPadding)
        //{
        //    if (Rows<=0) throw new Exception("Rows must be grater than zero.");
        //    if (Columns<=0) throw new Exception("Columns must be grater than zero.");
        //    if (CellPadding<0) throw new Exception("CellPadding must be non-negative.");
        //    PdfTable pt=new PdfTable(this,ContentAlignment.TopCenter,DefaultFont,Color.Black,Rows
        //        ,Columns,CellPadding);
        //    pt.header=new PdfTable(this,ContentAlignment.MiddleCenter,DefaultFont,Color.Black,1
        //        ,Columns,CellPadding);
        //    this.AddFont(DefaultFont);
			
        //    return pt;
        //}


        #endregion
    }
}
