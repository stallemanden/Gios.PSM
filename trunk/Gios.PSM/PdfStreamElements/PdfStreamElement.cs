using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

namespace Gios.PDF.SplitMerge
{
	internal abstract class PdfStreamElement
	{
        internal abstract void StreamElementWrite(Stream Stream,double pageHeight);

		internal virtual Color Colorrg
		{
			get
			{
				return Color.Empty;
			}
		}

		internal virtual Color ColorRG
		{
			get
			{
				return Color.Empty;
			}
		}

		internal virtual Font Font
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		internal virtual bool IsText
		{
			get
			{
				return false;
			}
		}

		internal virtual double StrokeWidth
		{
			get
			{
				return -1;
			}
			set
			{}
		}

       

        internal virtual List<PdfStreamElement> SubElements
        {
            get
            {
                return null;
            }
        }

        private List<Font> requiredFonts;
        internal List<Font> RequiredFonts
        {
            get
            {
                if (this.SubElements == null)
                    return null;
                this.requiredFonts = new List<Font>();
                foreach (PdfStreamElement element in this.SubElements)
                {
                    if (element.Font != null)
                    {
                        this.requiredFonts.Add(element.Font);
                    }
                }
                return requiredFonts;
            }
        }
	}
}
