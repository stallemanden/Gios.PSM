using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;

namespace Gios.PDF.SplitMerge
{
    public partial class FormFileAssociation : Form
    {
        public FormFileAssociation()
        {
            InitializeComponent();
        }

        private void buttonAssociate_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = Process.GetCurrentProcess().MainModule.FileName;
                Registry.SetValue(@"HKEY_CURRENT_USER\Software\Classes\.giospsm", "", "GiosPSM");
                Registry.SetValue(@"HKEY_CURRENT_USER\Software\Classes\GiosPSM", "", "GiosPSM");
                Registry.SetValue(@"HKEY_CURRENT_USER\Software\Classes\GiosPSM\DefaultIcon","", "" + filename + ",1");
                Registry.SetValue(@"HKEY_CURRENT_USER\Software\Classes\GiosPSM\shell\open\command",
                    "", "\"" + filename + "\" \"%1\"");

                MessageBox.Show("Association Successfully Done.", "File Type Association", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                RegistryKey reg = Registry.CurrentUser;
                reg.DeleteSubKeyTree(@"Software\Classes\.giospsm");
                reg.DeleteSubKeyTree(@"Software\Classes\GiosPSM");
                reg.Close();

                MessageBox.Show("Association Successfully Removed.", "File Type Association", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormFileAssociation_Load(object sender, EventArgs e)
        {

        }
    }
}
