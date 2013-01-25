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
	internal class PdfHeader : PdfObject
	{
		private string subject,title,author,creationdate;

		internal PdfHeader(PdfDocument PdfDocument,string subject,string title,string author)
            :base(PdfDocument)
		{
			this.subject=subject;
			this.title=title;
			this.author=author;
			this.creationdate=DateTime.Today.ToShortDateString();
		}

        internal override int Write(Stream s)
        {
            int pos = 0;
            pos += Utility.Write(s, this.HeadObj, true);
            pos += Utility.Write(s, "<<", true);
            pos += Utility.Write(s, "/Subject (" + subject + ")", true);
            pos += Utility.Write(s, "/Creator (Gios Pdf.NET Library)", true);
            pos += Utility.Write(s, "/Producer(Paolo Gios - http://www.paologios.com)", true);
            pos += Utility.Write(s, "/Title (" + title + ")", true);
            pos += Utility.Write(s, "/Author (" + author + ")", true);
            pos += Utility.Write(s, "/CreationDate (" + creationdate + ")", true);
            pos += Utility.Write(s, ">>", true);
            pos += Utility.Write(s, "endobj", true);
            return pos;           
        }

	}
}