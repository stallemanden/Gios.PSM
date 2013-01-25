using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Gios.PDF.SplitMerge;


namespace Gios.PDF.SplitMerge
{
    static class Program
    {
       

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ProjectForm pf = new ProjectForm();
            pf.Project = PdfProject.BlankProject;
            if (args.Length > 0)
            {
                try
                {
                    pf.Project = PdfProject.Load(args[0]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            Application.Run(pf);        


        }

    }

}