using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Collections;
using System.Text;
using System.Collections.Generic;

namespace Gios.PDF.SplitMerge
{
	internal class PdfStreamWriter
	{
		double ph;
        bool finished = false;
        System.IO.StreamWriter sw;

		internal PdfStreamWriter(Stream Stream,double PageHeight)
		{
			this.ph=PageHeight;
            //PdfLine pl = new PdfLine(new System.Drawing.Point(0, 0), new Point(1, 1), Color.Empty, 1);
            //pl.strokeWidth = -1;
            //this.LastElement = pl;
			sw=new System.IO.StreamWriter(Stream);
		}

        internal PdfStreamElement Write(PdfStreamElement StreamElement, PdfStreamElement LastElement)
		{
            List<PdfStreamElement> subElements = StreamElement.SubElements;
            if (subElements != null)
            {
                foreach (PdfStreamElement element in subElements)
                {
                    LastElement = Write(element, LastElement);
                }
                return LastElement;
            }

            //if (LastElement==null || StreamElement.IsText != LastElement.IsText)
            //{
            //    sw.WriteLine(StreamElement.IsText ? "BT" : "ET");
            //}

            //if (LastElement == null || StreamElement.Colorrg != Color.Empty && LastElement.Colorrg != StreamElement.Colorrg)
            //{
            //    sw.WriteLine(ColorrgLine(StreamElement.Colorrg));
            //}
            //if (LastElement == null || StreamElement.ColorRG != Color.Empty && LastElement.ColorRG != StreamElement.ColorRG)
            //{
            //    sw.WriteLine(ColorRGLine(StreamElement.ColorRG));
            //}
            //if (LastElement == null || StreamElement.StrokeWidth > -1 && LastElement.StrokeWidth != StreamElement.StrokeWidth)
            //{
            //    sw.WriteLine(StreamElement.StrokeWidth.ToString("0.##", CultureInfo.InvariantCulture) + " w");
            //}
			sw.Flush();

			StreamElement.StreamElementWrite(this.sw.BaseStream,this.ph);

            return LastElement;

		}
        internal void Finish(PdfStreamElement LastElement)
		{
			if (!this.finished)
			{
				this.finished=true;
                if (LastElement != null && LastElement.IsText)
				{
					sw.WriteLine("ET");
					
				}
			}
			sw.Flush();
		}

        private static string BCR(Color Color)
        {
            return (((double)Color.R) / 255).ToString("0.##",CultureInfo.InvariantCulture);
        }
        private static string BCG(Color Color)
        {
            return (((double)Color.G) / 255).ToString("0.##", CultureInfo.InvariantCulture);

        }
        private static string BCB(Color Color)
        {
            return (((double)Color.B) / 255).ToString("0.##", CultureInfo.InvariantCulture);
        }
        private static Hashtable ColorRGLineCache = new Hashtable();
        private static Hashtable ColorrgLineCache = new Hashtable();

        private static string ColorRGLine(Color Color)
        {
            if (!ColorRGLineCache.ContainsKey(Color))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(BCR(Color));
                sb.Append(' ');
                sb.Append(BCG(Color));
                sb.Append(' ');
                sb.Append(BCB(Color));
                sb.Append(" RG ");
                ColorRGLineCache.Add(Color, sb.ToString());
            }
            return (string)ColorRGLineCache[Color];
        }
        private static string ColorrgLine(Color Color)
        {
            if (!ColorrgLineCache.ContainsKey(Color))
            {
                ColorrgLineCache.Add(Color, ColorRGLine(Color).Replace("RG", "rg"));
            }
            return (string)ColorrgLineCache[Color];
        }
		
	}
}
