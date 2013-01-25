using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Gios.PSM
{
    public partial class FormAppSettings : Form
    {
        public FormAppSettings()
        {
            InitializeComponent();
        }

        private void FormAppSettings_Load(object sender, EventArgs e)
        {
            RegistryKey regCurrentUser = Registry.CurrentUser;

            bool skipMessages = (bool)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Classes\GiosPSM\messages", "", false);
            bool defaultTarget = (bool)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Classes\GiosPSM\usedefaultTarget", "", false);
            bool defaultName = (bool)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Classes\GiosPSM\usedefaultName", "", false);

            if (defaultTarget)
            {
                textBox1.Text = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Classes\GiosPSM\defaultTarget", "", "");
            }
            if (defaultName)
            {
                textBox1.Text = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Classes\GiosPSM\defaultName", "", "");
            }

        }
    }
}
