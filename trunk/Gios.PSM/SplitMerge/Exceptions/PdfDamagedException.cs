using System;
using System.Collections.Generic;
using System.Text;

namespace Gios.PDF.SplitMerge
{
    internal class PdfDamagedException : PdfException
    {
        internal PdfDamagedException(string message)
            : base(message)
        {
        }
    }
}
