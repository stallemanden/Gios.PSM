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
using System.Drawing;
using System.Globalization;
using System.IO;
using System.ComponentModel;
using System.Collections;

namespace Gios.PDF.SplitMerge
{
    internal abstract class PdfObject
	{
        internal abstract int Write(Stream s);

        internal PdfObject(PdfDocument pdfDocument)
        {
            this.PdfDocument = pdfDocument;
            this._id = this.PdfDocument.GetNextId();
            this.PdfDocument.PdfObjects.Add(this);
        }

       

		internal PdfDocument PdfDocument;        
		private int _id;
		internal int ID
		{
			get
			{                
				return this._id;
			}
		}
		internal string HeadR
		{
			get
			{
				return this.ID+" 0 R ";
			}
		}
		internal string HeadObj
		{
			get
			{
				return this.ID+" 0 obj";
			}
		}

        public override string ToString()
        {
            return this.GetType().Name + " " + this.HeadObj;
        }
	}

    internal class PdfObjectComparer : IComparer
    {
        #region IComparer Members

        public int Compare(object x, object y)
        {
            PdfObject a = x as PdfObject;
            PdfObject b = y as PdfObject;
            return a.ID.CompareTo(b.ID);
        }

        #endregion
    }
}
