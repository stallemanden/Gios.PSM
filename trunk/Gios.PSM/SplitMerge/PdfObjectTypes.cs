using System;
using System.Collections.Generic;
using System.Text;

namespace Gios.PDF.SplitMerge
{
    internal enum PdfObjectTypes
    {
        UnKnown = 0,
        Stream,
        Page,
        Pages,
        Root,
        Trailer,
        XRef,
        ObjStm,
        Other
    }
}
