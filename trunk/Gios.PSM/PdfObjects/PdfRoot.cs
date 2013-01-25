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
	internal class PdfRoot : PdfObject
	{
        internal PdfRoot(PdfDocument PdfDocument)
            :base(PdfDocument)
		{

		}

        internal override int Write(Stream s)
        {
            int pos = 0;
            pos+=Utility.Write(s,this.HeadObj,true);
            pos += Utility.Write(s, "<<\n/Type /Pages\n/Count " + this.PdfDocument.pageCount,true);
            pos += Utility.Write(s, "/Kids [",false);
            foreach (PdfObject po in this.PdfDocument.PdfObjects)
            {
                if (po is PdfPage)
                {
                    pos += Utility.Write(s, po.HeadR, false);
                }
            }
            pos += Utility.Write(s, "]",true);
            pos += Utility.Write(s, ">>",true);
            pos += Utility.Write(s, "endobj",true);
            return pos;
        }
	}
}
