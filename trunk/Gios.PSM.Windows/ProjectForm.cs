using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Gios.PDF;
using System.IO;
using System.Diagnostics;
using Gios.PDF.SplitMerge;
using System.Reflection;

namespace Gios.PDF.SplitMerge
{
    public partial class ProjectForm : Form
    {
        private Dictionary<string, Type> dropExtensions;
        Dictionary<Type, DetailPanel> detailPanels = new Dictionary<Type, DetailPanel>();

        #region properties

        private string lastSavedProjectMD5;

        private bool PendingChanges
        {
            get
            {
                return this.Project.ProjectMD5 != this.lastSavedProjectMD5;
            }
        }

        private bool suspendEvents = false;

        private PdfProject _project;
        public PdfProject Project
        {
            get { return _project; }
            set
            {
                this.suspendEvents = true;
                _project = value;
                this.lastSavedProjectMD5 = value.ProjectMD5;
                this.Text ="Gios PDF Splitter and Merger - Version "+
                    Assembly.GetEntryAssembly().GetName().Version+" - " + this.Project.Title;
                this.listView1.VirtualListSize = this.Project.Elements.Count;
                this.listView1.Refresh();
                this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                this.checkBoxAlwaysAskDestination.Checked = this.Project.AlwaysAskDestinationFile;
                this.checkBoxOpenAfterCreation.Checked = this.Project.OpenWithDefaultReaderAfterCreation;
                this.checkBoxOverwriteWithoutPrompt.Checked = this.Project.OverwriteDestinationWithoutPrompt;
                this.saveFileDialogTarget.FileName = this.Project.Target;
                this.textBoxTarget.Text = this.Project.Target;
                this.suspendEvents = false;
                this.SomethingChanged();
            }
        }

        public PdfProjectElement CurrentElement
        {
            get
            {
                if (this.Project == null)
                    return null;
                if (this.listView1.SelectedIndices.Count != 1)
                    return null;
                return this.Project.Elements[this.listView1.SelectedIndices[0]];
            }
        }

        public ProjectForm()
        {
            InitializeComponent();

            this.dropExtensions = new Dictionary<string, Type>();

            foreach(Type t in  this.GetType().Assembly.GetTypes())
            {
                if (t.IsSubclassOf(typeof(DetailPanel)))
                {
                    DetailPanel dp = Activator.CreateInstance(t) as DetailPanel;
                    dp.SomethingHappened += new EventHandler(dp_SomethingHappened);
                    dp.Dock = DockStyle.Fill;
                    this.detailPanels.Add(dp.ElementType,dp);
                    foreach(string extension in dp.Extensions)
                    {
                        this.dropExtensions.Add(extension, dp.ElementType);
                    }
                }
            }
        }

        void dp_SomethingHappened(object sender, EventArgs e)
        {
            this.SomethingChanged();
        }

        #endregion       

        #region visual events

        Graphics g = Graphics.FromImage(new Bitmap(1, 1));

        private void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex < this.Project.Elements.Count)
            {
                PdfProjectElement ppe = this.Project.Elements[e.ItemIndex];
                string text = this.Adapt(ppe.Path,this.listView1.Columns[0].Width-40,"...");

                

                ListViewItem lvi = new ListViewItem(new string[] {text, ppe.Detail });
                lvi.Checked = false;
                lvi.Checked = ppe.Enabled;
                lvi.ImageIndex = ppe.Enabled ? 1 : 0;
                e.Item = lvi;
            }
            else
            {
                ListViewItem lvi = new ListViewItem(new string[] { "", "","", "" });
                lvi.Checked = false;
                e.Item = lvi;
            }
        }

        private int MeasureString(string s)
        {
            return (int)g.MeasureString(s, this.listView1.Font).Width;
        }               

        public string Adapt(string s, int width,string sep)
        {
            int minOccurences = s.StartsWith(@"\\") ? 4 : 2;

            int wsep = MeasureString(sep);

            if (MeasureString(s) < width)
                return s;

            int index = s.LastIndexOf('\\')-1;
            string lasts = s;
            while (index > 1 && MeasureString(s) + wsep > width)
            {
                s = s.Remove(index, 1);
                index = s.LastIndexOf('\\') - 1;

                int occurrencies = s.Split("\\".ToCharArray()).Length;
                if (occurrencies <= minOccurences)
                    return lasts;

                lasts = s.Insert(index+1, sep);

                

            }
            return lasts;
        }

        private void listView1_VirtualItemsSelectionRangeChanged(object sender, ListViewVirtualItemsSelectionRangeChangedEventArgs e)
        {
            bool singleSelection = e.EndIndex == e.StartIndex;
            this.buttonDown.Enabled = singleSelection;
            this.buttonUp.Enabled = singleSelection;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateButtonStates();
            this.UpdateDetailPanel();
            this.SomethingChanged();
        }

        private void listView1_SizeChanged(object sender, EventArgs e)
        {
            this.listView1.Columns[0].Width = this.listView1.Width;
        }

        #endregion

        #region clicks
        
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X > 5 && e.X < 22)
            {
                ListViewItem lvi = this.listView1.GetItemAt(e.X, e.Y);
                this.Project.Elements[lvi.Index].Enabled = !this.Project.Elements[lvi.Index].Enabled;
                this.UpdateButtonStates();
                this.UpdateDetailPanel();
                this.listView1.RedrawItems(lvi.Index, lvi.Index, true);
                this.SomethingChanged();
            }
        }

        #endregion

        #region buttons

        private void UpdateButtonStates()
        {            

            bool up = false;
            bool down = false;

            if (this.listView1.SelectedIndices.Count == 1)
            {
                int index = this.listView1.SelectedIndices[0];
                if (index < this.Project.Elements.Count - 1)
                {
                    down = true;
                }
                if (index > 0)
                {
                    up = true;
                }
            }
            this.buttonDelete.Enabled = this.listView1.SelectedIndices.Count > 0;
            this.buttonUp.Enabled = up;
            this.buttonDown.Enabled = down;

        }

        private void UpdateDetailPanel()
        {
            PdfProjectElement ppe = this.CurrentElement;
            if (ppe == null)
            {
                this.panelCommonDetails.Visible = false;
                this.panelDetails.Visible = false;
            }
            else
            {
                this.panelCommonDetails.Visible = true;
                this.checkBoxDetailInclude.Checked = ppe.Enabled;
                this.labelDetailPath.Text = ppe.Path;

                this.panelDetails.Visible = false;
                // this.CurrentElement.Analyze();
                this.panelDetails.Controls.Clear();
                this.panelDetails.Controls.Add(this.detailPanels[ppe.GetType()]);
                this.detailPanels[ppe.GetType()].SetElement(this.CurrentElement);
                this.panelDetails.Visible = true;
            }
            this.IncrementCounter();
        }

        private void IncrementCounter()
        {
            //int count = int.Parse(this.toolStripLabelCount.Text);
            //count++;
            //this.toolStripLabelCount.Text = "" + count;
        }

        private void buttonUpDown_Click(object sender, EventArgs e)
        {            
            if (this.listView1.SelectedIndices.Count == 1)
            {
                int index = this.listView1.SelectedIndices[0];
                int index2 = sender == this.buttonDown ? index + 1 : index - 1;

                PdfProjectElement ppe1 = this.Project.Elements[index];
                this.Project.Elements[index] = this.Project.Elements[index2];
                this.Project.Elements[index2] = ppe1;
                this.listView1.SelectedIndices.Clear();
                this.listView1.SelectedIndices.Add(index2);
                if (sender == buttonDown)
                {
                    this.listView1.RedrawItems(index, index2, true);
                }
                else
                {
                    this.listView1.RedrawItems(index2, index, true);
                }
                this.listView1.Focus();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int lastIndex = -1;
            for (int index = this.listView1.SelectedIndices.Count - 1; index >= 0; index--)
            {
                this.Project.Elements.RemoveAt(this.listView1.SelectedIndices[index]);
                this.listView1.VirtualListSize = this.Project.Elements.Count;
                lastIndex = index - 1;
            }
            if (lastIndex > -1)
            {
                this.listView1.SelectedIndices.Clear();
                this.listView1.SelectedIndices.Add(lastIndex);
            }

            this.UpdateButtonStates();
            this.SomethingChanged();
            this.listView1.Focus();
        }

        #endregion

        private void checkBoxDetailInclude_CheckedChanged(object sender, EventArgs e)
        {
            this.CurrentElement.Enabled = this.checkBoxDetailInclude.Checked;
            this.listView1.RedrawItems(this.listView1.SelectedIndices[0], this.listView1.SelectedIndices[0], true);
            this.SomethingChanged();
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            this.SaveProject(sender == this.saveProjectAsToolStripMenuItem);
        }

        private void SaveProject(bool forceAskTarget)
        {
            if (string.IsNullOrEmpty(this.Project.Path) || forceAskTarget)
            {
                this.saveProjectDialog1.FileName = this.Project.Path;
                if (this.saveProjectDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.Project.Path = this.saveProjectDialog1.FileName;
                }
            }
            if (!string.IsNullOrEmpty(this.Project.Path))
            {
                this.Project.Save(this.Project.Path);
                Settings.AddRecentProjecy(this.Project.Path);
                this.lastSavedProjectMD5 = this.Project.ProjectMD5;
                this.SomethingChanged();
            }

            this.PrepareRecentProjectItems();
        }


        private void SomethingChanged()
        {
            this.toolStripButtonSaveProject.Enabled = this.PendingChanges;
            this.saveProjectToolStripMenuItem.Enabled = this.toolStripButtonSaveProject.Enabled;
            this.toolStripButtonMergePDF.Enabled = this.Project.HasPages;
            this.toolStripButtonSplitPDF.Enabled = this.Project.HasPages;
            
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.suspendEvents)
                return;
            this.Project.OpenWithDefaultReaderAfterCreation = this.checkBoxOpenAfterCreation.Checked;
            this.Project.OverwriteDestinationWithoutPrompt = this.checkBoxOverwriteWithoutPrompt.Checked;
            this.Project.AlwaysAskDestinationFile = this.checkBoxAlwaysAskDestination.Checked;
            this.SomethingChanged();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.suspendEvents)
                return;
            this.Project.Target = this.textBoxTarget.Text;
            this.SomethingChanged();
        }

        private void toolStripButtonNewProject_Click(object sender, EventArgs e)
        {
            if (this.PendingChanges)
            {
                switch (MessageBox.Show("Save Pending Changes?", "Closing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
                {
                    case DialogResult.Yes:
                        this.SaveProject(false);
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        return;

                }
            }

            this.Project = PdfProject.BlankProject;

        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            if (this.openProjectDialog1.ShowDialog() == DialogResult.OK)
            {
                this.LoadProject(this.openProjectDialog1.FileName);
            }
        }
       

        private void ProjectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.PendingChanges)
                return;

            switch (MessageBox.Show("Save Pending Changes?", "Closing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
            {
                case DialogResult.Yes:
                    this.SaveProject(false);
                    break;
                case DialogResult.No:
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;

            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void LoadProject(string path)
        {
            if (this.PendingChanges)
            {
                switch (MessageBox.Show("Save Pending Changes?", "Load Project", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
                {
                    case DialogResult.Yes:
                        this.SaveProject(false);
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }

            try
            {
                this.Project = PdfProject.Load(path);
                Settings.AddRecentProjecy(path);
                this.PrepareRecentProjectItems();
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Add(string file)
        {            
            try
            {
                string ext = Path.GetExtension(file).ToLower().TrimStart(".".ToCharArray());
                Type ppType=this.dropExtensions[ext];

                if (ppType==null)
                    return;

                PdfProjectElement ppe = (PdfProjectElement)Activator.CreateInstance(ppType, new object[] { file });
                ppe.Analyze();
                this.Project.Elements.Add(ppe);
                this.listView1.VirtualListSize = this.Project.Elements.Count;
                this.SomethingChanged();
            }           
            catch (Exception e)
            {
                if (MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    == DialogResult.Retry)
                {
                    this.Add(file);
                }
            }
        }


        private void buttonPickTarget_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialogTarget.ShowDialog() == DialogResult.OK)
            {
                this.textBoxTarget.Text = this.saveFileDialogTarget.FileName;
            }
        }                

        private void PrepareRecentProjectItems()
        {
            this.recentProjectsToolStripMenuItem.DropDownItems.Clear();
            foreach (string path in Settings.GetRecentProjects())
            {
                ToolStripMenuItem ddm = new ToolStripMenuItem();
                ddm.Text = path;
                ddm.Tag = path;
                ddm.Click += new EventHandler(ddm_Click);
                this.recentProjectsToolStripMenuItem.DropDownItems.Add(ddm);
            }

            this.recentProjectsToolStripMenuItem.Enabled = this.recentProjectsToolStripMenuItem.DropDownItems.Count > 0;
        }

        void ddm_Click(object sender, EventArgs e)
        {
            string path = ""+(sender as ToolStripMenuItem).Tag;
            if (!File.Exists(path))
            {
                if (MessageBox.Show(Path.GetFileName(path) + " no longer exists. Do you want to remove it from the list?"
                    , "Project File no longer exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    , MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    Settings.DeleteRecentProject(path);
                    this.PrepareRecentProjectItems();
                }
            }
            else
            {
                this.LoadProject(path);
            }
        }

        private void ProjectForm_Load(object sender, EventArgs e)
        {
            this.PrepareRecentProjectItems();
        }

        private void imageFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialogAddImage.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in this.openFileDialogAddImage.FileNames)
                {
                    this.Add(file);
                }
            }
        }
        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialogAddPDF.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in this.openFileDialogAddPDF.FileNames)
                {
                    this.Add(file);
                }
            }
        }

        //Merge PDFs
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FormCreationProgress fcp = new FormCreationProgress();
            fcp.Command = FormCreationProgress.Commands.Split;
            if (sender == this.toolStripButtonMergePDF)
            {
                fcp.Command = FormCreationProgress.Commands.Merge;
            }

            if (fcp.Command == FormCreationProgress.Commands.Merge)
            {
                if (this.Project.AlwaysAskDestinationFile || string.IsNullOrEmpty(this.Project.Target))
                {
                    if (this.saveFileDialogTarget.ShowDialog() == DialogResult.OK)
                    {
                        this.Project.Target = this.saveFileDialogTarget.FileName;
                        this.textBoxTarget.Text = this.Project.Target;
                        this.SomethingChanged();
                    }
                    else
                    {
                        return;
                    }
                }
                if (!this.Project.OverwriteDestinationWithoutPrompt && File.Exists(this.Project.Target))
                {
                    if (MessageBox.Show(string.Format("Overwrite {0}?", Path.GetFileName(this.Project.Target))
                        , "Target file already exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                        , MessageBoxDefaultButton.Button2) == DialogResult.No)
                        return;
                }
            }

            if (fcp.Command == FormCreationProgress.Commands.Split)
            {
                if (MessageBox.Show("Are you sure?", "Split PDF", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
            }                      

            try
            {                
                fcp.Project = this.Project;
                DialogResult result= fcp.ShowDialog();
                switch (result)
                {
                    case DialogResult.Cancel:
                        try
                        {
                            File.Delete(this.Project.TempTarget);
                        }
                        catch
                        { }
                        break;
                    case DialogResult.OK:
                        if (fcp.Command == FormCreationProgress.Commands.Merge)
                        {
                            if (this.Project.OpenWithDefaultReaderAfterCreation)
                            {
                                Process.Start(this.Project.Target);
                            }
                        }
                        if (fcp.Command == FormCreationProgress.Commands.Split)
                        {
                            MessageBox.Show("PDF split","Split Successfull.");
                        }
                        break;
                }               

            }
            catch (Exception e2)
            {
                fcp.Close();
                MessageBox.Show(e2.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string temp = Path.Combine(Path.GetTempPath(), "preview.pdf");
                using (Stream s = new FileStream(temp, FileMode.Create, FileAccess.Write))
                {
                    PdfMerger pm = new PdfMerger(s);
                    this.CurrentElement.AddToMerger(pm);
                    pm.Finish();
                }
                Process.Start(temp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProjectForm_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                string[] files = (string[])e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop);
                bool ok = true;
                foreach (string file in files)
                {
                    string ext = Path.GetExtension(file).ToLower().TrimStart(".".ToCharArray());
                    if (!this.dropExtensions.ContainsKey(ext))
                    {
                        ok = false;
                    }
                }

                if (ok)
                {
                    e.Effect = DragDropEffects.Copy;
                    return;
                }
            }
            catch
            {
            }
            e.Effect = DragDropEffects.None;
        }

        private void ProjectForm_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string[] files = (string[])e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop);                
                foreach (string file in files)
                {
                    this.Add(file);
                }

            }
            catch
            {
            }
        }

        private void fileAssociationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormFileAssociation().ShowDialog();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.paologios.com/?help=gios.psm");
        }

        private void toolStripSettings_Click(object sender, EventArgs e)
        {
            
        }

  
    }
}