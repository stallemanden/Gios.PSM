using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Gios.PDF.SplitMerge;

namespace Gios.PDF.SplitMerge
{
    public partial class FormCreationProgress : Form
    {
        public enum Commands
        {
            NA,
            Split,
            Merge
        }

        public Commands Command { get; set; }

        PdfMerger merger = null;

        private PdfProject _project;
        public PdfProject Project
        {
            get { return _project; }
            set
            { 
                _project = value;
            }
        }

        public void StartMerger()
        {
            this.DialogResult = DialogResult.OK;
            using (Stream s = new FileStream(this.Project.TempTarget, FileMode.Create, FileAccess.Write))
            {
                this.merger = new PdfMerger(s);

                this.merger.PdfMergerProgress += new EventHandler<PdfMergerProgressEventArgs>(merger_PdfMergerProgress);
               
                foreach (PdfProjectElement ppe in this.Project.Elements)
                {
                    if (this.merger.CancelPending)
                    {
                        this.DialogResult = DialogResult.Cancel;
                        return;
                    }
                    if (ppe.Enabled)
                    {
                        ppe.AddToMerger(this.merger);
                    }
                }

                this.merger.Finish();
            }
            File.Copy(this.Project.TempTarget, this.Project.Target,true);
            File.Delete(this.Project.TempTarget);

        }

        public void StartSplitter()
        {
            this.DialogResult = DialogResult.OK;

            int n = 1;
            foreach (PdfProjectElement ppe in this.Project.Elements)
            {
                if (!ppe.Enabled)
                    continue;
                if (ppe is PdfProjectPdf)
                {
                    foreach (int page in ((PdfProjectPdf)ppe).CalculatePageSelection(true))
                    {
                        using (Stream s = File.OpenRead(ppe.Path))
                        {
                            PdfParser parsed = PdfParser.Parse(s);
                            this.GenerateSplit(n, this.Project.Target, parsed, page);
                            n++;
                        }
                    }
                }
                if (ppe is PdfProjectImage)
                {
                    this.GenerateSplit(n, this.Project.Target, PdfParser.Parse(ImageToPDFConverter.ConvertImageToPDF(ppe.Path)), 0);
                    n++;  
                }
            }

        }

        public void GenerateSplit(int n, string target, PdfParser parsedPdf, int page)
        {
            target = Path.Combine(Path.GetDirectoryName(target),
                Path.GetFileNameWithoutExtension(target) + " - " + n.ToString("00000") + ".pdf");

            using (Stream s = new FileStream(target, FileMode.Create, FileAccess.Write))
            {
                this.merger = new PdfMerger(s);
                this.merger.PdfMergerProgress += new EventHandler<PdfMergerProgressEventArgs>(merger_PdfMergerProgress);
                this.merger.Add(parsedPdf, new int[] { page });                
                this.merger.Finish();
            }
            
        }


        void merger_PdfMergerProgress(object sender, PdfMergerProgressEventArgs e)
        {
            this.label1.Text = this.Project.Elements[e.Element].Filename + ": " + e.ToString()+" ...";
            Application.DoEvents();
        }

        public FormCreationProgress()
        {
            InitializeComponent();

            this.label1.Text = "";
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (this.merger!=null)
            {
                this.buttonCancel.Enabled=false;
                merger.CancelPending=true;
            }
            
        }

        private void FormCreationProgress_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (this.merger == null)
            {
                switch (this.Command)
                {
                    case Commands.Merge:
                        this.StartMerger();
                        break;
                    case Commands.Split:
                        this.StartSplitter();
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            this.Close();
        }
    }
}