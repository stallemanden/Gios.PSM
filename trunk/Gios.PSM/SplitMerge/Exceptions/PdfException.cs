using System;
using System.Collections.Generic;
using System.Text;

namespace Gios.PDF.SplitMerge
{
    public abstract class PdfException : Exception
    {
        public PdfException(string message)
            : base(message)
        {
        }
        
    }
}
