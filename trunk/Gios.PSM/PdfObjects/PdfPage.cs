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
using System.Globalization;
using System.Text;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace Gios.PDF.SplitMerge
{
    internal class PdfPage : PdfObject
	{

        private PdfDocumentFormat PdfDocumentFormat;
       internal  PdfStream PdfStream;
        ArrayList Images;

		internal double Height
		{
			get
			{
				return this.PdfDocumentFormat.height;
			}			
		}
		internal double Width
		{
			get
			{
				return this.PdfDocumentFormat.width;
			}			
		}		

		internal PdfPage(PdfDocument pdfDocument, PdfDocumentFormat PdfDocumentFormat)
            :base(pdfDocument)
		{
            this.PdfDocument.pageCount++;
            this.Images = new ArrayList();
			this.PdfDocumentFormat=PdfDocumentFormat;
			this.PdfStream=new PdfStream(this);
		}	
	

		private void AddInternal(PdfStreamElement PdfStreamElement)
		{          
			this.PdfStream.StreamObjectElements.Add(PdfStreamElement);
		}


        internal void Add(byte[] jpegImage, double posx, double posy, double DPI)
        {
            if (DPI <= 0) throw new Exception("DPI must be greater than zero.");
            
            string hash = BitConverter.ToInt32(MD5.Create().ComputeHash(jpegImage), 12).ToString("X");            
            PdfImage pi = (PdfImage)this.PdfDocument.images[hash];
            if (pi == null)
            {
               pi= this.PdfDocument.NewImage(jpegImage,hash);
               this.PdfDocument.images.Add(hash, pi);
            }

            if (!this.Images.Contains(pi))
                this.Images.Add(pi);

            PdfImageContent pic = new PdfImageContent(pi, posx, this.Height - posy, DPI);
            this.PdfStream.StreamObjectElements.Add(pic);
        }



        internal override int Write(Stream s)
        {
            int p = 0;
            p += Utility.Write(s, this.HeadObj, true);
            p += Utility.Write(s, "<< /Type /Page ", true);
            p += Utility.Write(s, "/Parent 3 0 R /MediaBox [0 0 ", false);

            p += Utility.Write(s, this.Width.ToString("0.##", CultureInfo.InvariantCulture) + " "
            + this.Height.ToString("0.##", CultureInfo.InvariantCulture) + "]", true);
            
            p += Utility.Write(s, "/Resources ", true);
            p += Utility.Write(s, "<< ", true);
            p += Utility.Write(s, "/Font << ", true);
            p += Utility.Write(s, ">>/ProcSet [/PDF /ImageC /ImageI /ImageB /Text]", true);

            p+=Utility.Write(s,"/XObject <<",true);

            foreach (PdfImage pi in this.Images)
            {
                p+=Utility.Write(s,"/" + pi.ImageName + " " + pi.HeadR,false);
            }

            p+=Utility.Write(s,">>",true);
            p+=Utility.Write(s,">>",true);

            p+=Utility.Write(s,"/Contents ["+this.PdfStream.HeadR+"]",true);

             p+=Utility.Write(s," >>",true);
            p+=Utility.Write(s,"endobj",true);	
	        return p;
        }		
		
	}
}

