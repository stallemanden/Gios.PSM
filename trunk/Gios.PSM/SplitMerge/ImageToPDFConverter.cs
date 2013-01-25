using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Gios.PDF.SplitMerge
{
    public class ImageToPDFConverter
    {
        public static MemoryStream ConvertImageToPDF(string jpegFile)
        {
            return ConvertImageToPDF(File.ReadAllBytes(jpegFile));
        }
        public static MemoryStream ConvertImageToPDF(byte[] jpegStream)
        {
            System.Drawing.Image img = null;
            using (MemoryStream ms2 = new MemoryStream())
            {
                ms2.Write(jpegStream, 0, jpegStream.Length);
                img = System.Drawing.Image.FromStream(ms2);
            }

            float dpi = img.HorizontalResolution;

            float realw = (((float)(img.Width)) / img.HorizontalResolution);
            float realy = (((float)(img.Height)) / img.HorizontalResolution);

            PdfDocumentFormat format = PdfDocumentFormat.InInches(realw, realy);

            MemoryStream ms = new MemoryStream();
            PdfDocument pd = new PdfDocument(format, "", "", "");
            PdfPage pp = pd.NewPage();
            pp.Add(jpegStream, 0, 0, dpi);
            pd.Save(ms);
            return ms;
        }

    }
}
