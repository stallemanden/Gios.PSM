using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gios.PDF.SplitMerge
{
    public class DetailPanel : UserControl
    {
        public event EventHandler SomethingHappened;

        public DetailPanel()
        {
            this.SomethingHappened = new EventHandler(this.OnSomethingHappened);
        }

        protected void OnSomethingHappened(object sender, EventArgs e)
        {

        }

        protected void SomethingHappens()
        {
            this.SomethingHappened(this, EventArgs.Empty);
        }

        public virtual Type ElementType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual string[] Extensions
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual void SetElement(PdfProjectElement ppe)
        {
            throw new NotImplementedException();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DetailPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Name = "DetailPanel";
            this.ResumeLayout(false);

        }
    }
}
