using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;

namespace Gios.PDF.SplitMerge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("GIOS PDF Splitter and Merger - Version "
                + Assembly.GetEntryAssembly().GetName().Version);
            Console.WriteLine("Copyright © 2009 Paolo Gios - www.paologios.com");
            Console.WriteLine();
            Console.WriteLine("Merges PDF and JPG files into a single PDF file.");
            Console.WriteLine();
            try 
            {
                if (args.Length == 0)
                {
                    Help();
#if DEBUG
                    Console.ReadLine();
#endif
                    return;
                }

                string output = "";

                for (int index = 0; index < args.Length - 1; index++)
                {
                    string f = args[index].ToLower();
                    if (f == "output")
                    {
                        output = args[index + 1];
                        break;
                    }
                }

                if (output == null)
                    return;

                using (FileStream outputStream = new FileStream(output, FileMode.Create, FileAccess.Write))
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    Console.WriteLine("Creating " + output + " ...");
                    PdfMerger merger = new PdfMerger(outputStream);
                    merger.PdfMergerProgress += new EventHandler<PdfMergerProgressEventArgs>(merger_PdfMergerProgress);
                    for (int index = 0; index < args.Length; index++)
                    {
                        string f = args[index].ToLower();
                        if (f == "output")
                        {
                            index++;
                            continue;
                        }

                        string directory = Path.GetDirectoryName(f);

                        if (string.IsNullOrEmpty(directory))
                        {
                            directory = Environment.CurrentDirectory;
                        }

                        string[] files = Directory.GetFiles(directory, Path.GetFileName(f), SearchOption.TopDirectoryOnly);

                        Array.Sort(files);

                        foreach (string file in files)
                        {
                            if (string.Compare(file, outputStream.Name) == 0)
                                continue;

                            string extension = Path.GetExtension(file).ToLower();
                            Stream fs = null;
                            switch (extension)
                            {
                                case ".pdf":
                                    fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);      
                                    break;
                                case ".jpg":
                                    fs = ImageToPDFConverter.ConvertImageToPDF(file);
                                    break;
                                default :
                                    continue;
                            }

                            using (fs)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Adding " + file + "... ");
                                PdfParser pa = PdfParser.Parse(fs);
                                merger.Add(pa, null);
                                Console.SetCursorPosition(0, Console.CursorTop);
                                Console.WriteLine("Done.                                                    ");
                            }
                        }
                    }
                    Console.WriteLine();
                    Console.Write("Finishing ... ");
                    merger.Finish();
                    sw.Stop();
                    Console.WriteLine("Finished in " + sw.ElapsedMilliseconds + " ms.");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

#if DEBUG
            Console.ReadLine();
#endif
        }

        static void Help()
        {
            string fname = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
            
            Console.WriteLine(fname + " names output destination");
            Console.WriteLine();
            Console.WriteLine("source\t\tSpecifies a list of one or more files or directories.");
            Console.WriteLine("\t\tWildcards may be used to include multiple files.");
            Console.WriteLine("destination\tSpecifies destination PDF file.");
            Console.WriteLine();
            Console.WriteLine("Examples: " );
            Console.WriteLine(fname + @" c:\tmp\pdf1.pdf c:\tmp\pdf2.pdf output c:\tmp\out.pdf");
            Console.WriteLine(fname + @" c:\tmp\img1.jpg c:\tmp\pag*.pdf output c:\tmp\out.pdf");
        }

        static void merger_PdfMergerProgress(object sender, PdfMergerProgressEventArgs e)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(string.Format(e.Operation,e.Completed,e.Total)+"                        ");
        }

       
    }
}
