using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Gios.PDF.SplitMerge.Controls
{
    public partial class DetailPanelPdf : DetailPanel
    {
        PdfProjectPdf ppp;
        public DetailPanelPdf()
        {
            InitializeComponent();
        }

        public override Type ElementType
        {
            get
            {
                return typeof(PdfProjectPdf);
            }
        }

       

        public override string[] Extensions
        {
            get
            {
                return new string[] { "pdf" };
            }
        }

        public override void SetElement(PdfProjectElement ppe)
        {            
            this.ppp=(PdfProjectPdf)ppe;
            this.textBox1.Text = this.ppp.Pages;
            this.radioButtonIncludeAllPages.Checked = ppp.IncludeAllPages;
            this.radioButtonIncludePageRange.Checked = !ppp.IncludeAllPages;

            this.timer1_Tick(null, null);

            if (ppe.Error == null)
            {
                this.label1.Text = "PDF of " + ppp.PageCount + " pages.";
            }
            else
            {
                this.label1.Text = ppe.Error;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.timer1.Stop();
            this.timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Stop();

            string newSelectionText = this.textBox1.Text;
            PdfProjectPdf ppf=new PdfProjectPdf();
            ppf.Pages=newSelectionText;
            int[] newSelection = ppf.CalculatePageSelection(false);

            if (newSelection == null)
            {
                this.labelStatusPageSelection.Text = "Invalid selection!";
            }
            else
            {                
                this.labelStatusPageSelection.Text = newSelection.Length+" pages selected.";
                ppp.Pages = ppf.Pages;
                this.SomethingHappens();
            }

        }

        private void radioButtonIncludeAllPages_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonIncludeAllPages.Checked)
            {
                this.radioButtonIncludePageRange.Checked = false;                
                this.textBox1.Enabled = false;
                this.labelStatusPageSelection.Enabled = false;
                this.ppp.IncludeAllPages = true;
                this.SomethingHappens();
            }
        }

        private void radioButtonIncludePageRange_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonIncludePageRange.Checked)
            {
                this.radioButtonIncludeAllPages.Checked = false;
                this.textBox1.Enabled = true;
                this.labelStatusPageSelection.Enabled = true;
                this.ppp.IncludeAllPages = false;
                this.SomethingHappens();
            }
        }

        
    }
}
