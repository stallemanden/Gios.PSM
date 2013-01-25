using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gios.PDF.SplitMerge
{
    public class PdfProjectElementListViewItem : ListViewItem
    {
        public PdfProjectElementListViewItem(PdfProjectElement ppe)
        {
            //this.SubItems = new ListViewSubItemCollection(this);            
            this.Checked = ppe.Enabled;
        }

        
    }
}
