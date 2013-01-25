using System;
using System.Collections.Generic;
using System.Text;

namespace Gios.PDF.SplitMerge
{
    public class PdfEncryptedException : PdfException
    {
        public PdfEncryptedException(string message)
            : base(message)
        {
        }
        
    }
}
