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
using System.Text;
using System.IO;

namespace Gios.PDF.SplitMerge
{
	internal class PdfCatalog : PdfObject
	{
		internal PdfCatalog(PdfDocument PdfDocument)
            :base(PdfDocument)
		{

		}
        internal override int Write(Stream s)
        {
            int pos = 0;
            pos+=Utility.Write(s, this.HeadObj, true);
            pos += Utility.Write(s, "<<", true);
            pos += Utility.Write(s, "/Type /Catalog /Pages 3 0 R ", true);
            pos += Utility.Write(s, "/OpenAction /Fit", true);
            pos += Utility.Write(s, ">>", true);
            pos += Utility.Write(s, "endobj", true);
            return pos;
        }

	}
}
