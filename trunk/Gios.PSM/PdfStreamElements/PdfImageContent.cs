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
using System.Globalization;
using System.IO;

namespace Gios.PDF.SplitMerge
{
	internal class PdfImageContent : PdfStreamElement
    {
        #region properties

        PdfImage image;
        private double dpi;
        double posx, posy;
		private double TargetWidth
		{
			get
			{
				return this.image.width*72/this.dpi;
			}
		}
		private double TargetHeight
		{
			get
			{
				return this.image.height*72/this.dpi;
			}
        }

        #endregion

        #region constructors

        internal PdfImageContent(PdfImage image, double posx, double posy, double DPI)
        {
            this.image = image;
            this.posx = posx;
            this.dpi = DPI;
            this.posy = posy - this.TargetHeight;
        }
        #endregion

        #region PdfStreamElement overrides

        internal override void StreamElementWrite(System.IO.Stream Stream, double pageHeight)
        {
            StreamWriter sw = new StreamWriter(Stream);

            sw.WriteLine("q");
            sw.Write(this.TargetWidth.ToString("0.##", CultureInfo.InvariantCulture) + " 0 0 ");
            sw.WriteLine(this.TargetHeight.ToString("0.##", CultureInfo.InvariantCulture)
                + " " + posx.ToString("0.##", CultureInfo.InvariantCulture)
                + " " + posy.ToString("0.##", CultureInfo.InvariantCulture) + " cm");
            sw.WriteLine("/" + this.image.ImageName + " Do");
            sw.WriteLine("Q");
            sw.Flush();
        }

        #endregion

    }
	
}