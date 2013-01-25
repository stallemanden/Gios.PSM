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
using System.Drawing;
using System.Drawing.Imaging;

namespace Gios.PDF.SplitMerge
{	
    internal class PdfImage : PdfObject
    {
        #region properties

        internal int height,width;
		byte[] imageStream;

        private string hash;
        internal string Hash
        {
            get
            {
                return hash;
            }
        }

        internal string ImageName
        {
            get
            {
                return  "I" + this.Hash;
            }
        }

        #endregion

        #region constructors

        internal PdfImage(PdfDocument pdfDocument, byte[] imageStream, int width, int height,string hash)
            :base(pdfDocument)
        {
            this.imageStream = imageStream;
            this.width = width;
            this.height = height;
            this.hash = hash;
        }

        #endregion

        #region PdfObject overrides

        internal override int Write(System.IO.Stream s)
        {
            int pos = 0;
            pos += Utility.Write(s, this.HeadObj, true);
            pos += Utility.Write(s, "<< /Type /XObject", true);
            pos += Utility.Write(s, "/Subtype /Image", true);
            pos += Utility.Write(s, "/Width " + this.width, true);
            pos += Utility.Write(s, "/Height " + this.height, true);
            pos += Utility.Write(s, "/BitsPerComponent 8", true);
            //pos += Utility.Write(s, "/Name/" + this.ImageName, true);
            pos += Utility.Write(s, "/ColorSpace /DeviceRGB", true);
            pos += Utility.Write(s, "/Length " + this.imageStream.Length, true);
            pos += Utility.Write(s, "/Filter /DCTDecode ", true);
            pos += Utility.Write(s, ">>", true);
            pos += Utility.Write(s, "stream", true);
            pos += this.imageStream.Length;
            s.Write(this.imageStream, 0, this.imageStream.Length);
            pos += Utility.Write(s, "", true);
            pos += Utility.Write(s, "endstream", true);
            pos += Utility.Write(s, "endobj", true);

            return pos;
        }

        #endregion
    }
}
