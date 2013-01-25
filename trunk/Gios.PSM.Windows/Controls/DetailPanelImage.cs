using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Gios.PDF.SplitMerge.Controls
{
    public partial class DetailPanelImage : DetailPanel
    {
        public DetailPanelImage()
        {
            InitializeComponent();
        }

        public override Type ElementType
        {
            get
            {
                return typeof(PdfProjectImage);
            }
        }

        public override string[] Extensions
        {
            get
            {
                return new string[] { "jpg" };
            }
        }

        public override void SetElement(PdfProjectElement ppe)
        {
            this.pictureBox1.Image = Image.FromFile(ppe.Path);
        }
    }
}
