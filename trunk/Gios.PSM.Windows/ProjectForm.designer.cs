namespace Gios.PDF.SplitMerge
{
    partial class ProjectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonPickTarget = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxOpenAfterCreation = new System.Windows.Forms.CheckBox();
            this.checkBoxOverwriteWithoutPrompt = new System.Windows.Forms.CheckBox();
            this.checkBoxAlwaysAskDestination = new System.Windows.Forms.CheckBox();
            this.textBoxTarget = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.panelCommonDetails = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.checkBoxDetailInclude = new System.Windows.Forms.CheckBox();
            this.labelDetailPath = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonNewProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonMergePDF = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSplitPDF = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.recentProjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileAssociationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveProjectDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialogAddPDF = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogAddImage = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogTarget = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSettings = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelCommonDetails.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 51);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(779, 302);
            this.splitContainer1.SplitterDistance = 363;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.SizeChanged += new System.EventHandler(this.listView1_SizeChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonDelete);
            this.groupBox1.Controls.Add(this.buttonDown);
            this.groupBox1.Controls.Add(this.buttonPickTarget);
            this.groupBox1.Controls.Add(this.buttonUp);
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBoxOpenAfterCreation);
            this.groupBox1.Controls.Add(this.checkBoxOverwriteWithoutPrompt);
            this.groupBox1.Controls.Add(this.checkBoxAlwaysAskDestination);
            this.groupBox1.Controls.Add(this.textBoxTarget);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 296);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Project Settings";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDelete.Location = new System.Drawing.Point(304, 234);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(38, 34);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "X";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDown.Enabled = false;
            this.buttonDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDown.Location = new System.Drawing.Point(304, 173);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(38, 34);
            this.buttonDown.TabIndex = 4;
            this.buttonDown.Text = "DN";
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonUpDown_Click);
            // 
            // buttonPickTarget
            // 
            this.buttonPickTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPickTarget.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPickTarget.Location = new System.Drawing.Point(304, 38);
            this.buttonPickTarget.Name = "buttonPickTarget";
            this.buttonPickTarget.Size = new System.Drawing.Size(34, 20);
            this.buttonPickTarget.TabIndex = 4;
            this.buttonPickTarget.Text = "...";
            this.buttonPickTarget.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonPickTarget.UseVisualStyleBackColor = true;
            this.buttonPickTarget.Click += new System.EventHandler(this.buttonPickTarget_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUp.Enabled = false;
            this.buttonUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUp.Location = new System.Drawing.Point(304, 133);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(38, 34);
            this.buttonUp.TabIndex = 4;
            this.buttonUp.Text = "UP";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUpDown_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BackColor = System.Drawing.Color.White;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(6, 136);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(292, 154);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.VirtualMode = true;
            this.listView1.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listView1_RetrieveVirtualItem);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.VirtualItemsSelectionRangeChanged += new System.Windows.Forms.ListViewVirtualItemsSelectionRangeChangedEventHandler(this.listView1_VirtualItemsSelectionRangeChanged);
            this.listView1.SizeChanged += new System.EventHandler(this.listView1_SizeChanged);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 119;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "notchecked.gif");
            this.imageList1.Images.SetKeyName(1, "checked.gif");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Target Location:";
            // 
            // checkBoxOpenAfterCreation
            // 
            this.checkBoxOpenAfterCreation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxOpenAfterCreation.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.checkBoxOpenAfterCreation.Location = new System.Drawing.Point(6, 113);
            this.checkBoxOpenAfterCreation.Name = "checkBoxOpenAfterCreation";
            this.checkBoxOpenAfterCreation.Size = new System.Drawing.Size(336, 17);
            this.checkBoxOpenAfterCreation.TabIndex = 1;
            this.checkBoxOpenAfterCreation.Text = "Open with default reader after creation";
            this.checkBoxOpenAfterCreation.UseVisualStyleBackColor = true;
            this.checkBoxOpenAfterCreation.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBoxOverwriteWithoutPrompt
            // 
            this.checkBoxOverwriteWithoutPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxOverwriteWithoutPrompt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.checkBoxOverwriteWithoutPrompt.Location = new System.Drawing.Point(6, 90);
            this.checkBoxOverwriteWithoutPrompt.Name = "checkBoxOverwriteWithoutPrompt";
            this.checkBoxOverwriteWithoutPrompt.Size = new System.Drawing.Size(336, 17);
            this.checkBoxOverwriteWithoutPrompt.TabIndex = 1;
            this.checkBoxOverwriteWithoutPrompt.Text = "Overwrite destination file without prompt";
            this.checkBoxOverwriteWithoutPrompt.UseVisualStyleBackColor = true;
            this.checkBoxOverwriteWithoutPrompt.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBoxAlwaysAskDestination
            // 
            this.checkBoxAlwaysAskDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxAlwaysAskDestination.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.checkBoxAlwaysAskDestination.Location = new System.Drawing.Point(6, 67);
            this.checkBoxAlwaysAskDestination.Name = "checkBoxAlwaysAskDestination";
            this.checkBoxAlwaysAskDestination.Size = new System.Drawing.Size(336, 17);
            this.checkBoxAlwaysAskDestination.TabIndex = 1;
            this.checkBoxAlwaysAskDestination.Text = "Always ask destination file";
            this.checkBoxAlwaysAskDestination.UseVisualStyleBackColor = true;
            this.checkBoxAlwaysAskDestination.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // textBoxTarget
            // 
            this.textBoxTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTarget.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTarget.Location = new System.Drawing.Point(6, 41);
            this.textBoxTarget.Name = "textBoxTarget";
            this.textBoxTarget.Size = new System.Drawing.Size(292, 20);
            this.textBoxTarget.TabIndex = 0;
            this.textBoxTarget.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.panelDetails);
            this.groupBox2.Controls.Add(this.panelCommonDetails);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(398, 296);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Element Settings";
            // 
            // panelDetails
            // 
            this.panelDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelDetails.Location = new System.Drawing.Point(6, 113);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Size = new System.Drawing.Size(385, 177);
            this.panelDetails.TabIndex = 2;
            // 
            // panelCommonDetails
            // 
            this.panelCommonDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCommonDetails.Controls.Add(this.linkLabel1);
            this.panelCommonDetails.Controls.Add(this.checkBoxDetailInclude);
            this.panelCommonDetails.Controls.Add(this.labelDetailPath);
            this.panelCommonDetails.Location = new System.Drawing.Point(6, 19);
            this.panelCommonDetails.Name = "panelCommonDetails";
            this.panelCommonDetails.Size = new System.Drawing.Size(385, 88);
            this.panelCommonDetails.TabIndex = 1;
            this.panelCommonDetails.Visible = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.Location = new System.Drawing.Point(3, 22);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(379, 30);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Preview this section with default reader";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // checkBoxDetailInclude
            // 
            this.checkBoxDetailInclude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxDetailInclude.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.checkBoxDetailInclude.Location = new System.Drawing.Point(6, 59);
            this.checkBoxDetailInclude.Name = "checkBoxDetailInclude";
            this.checkBoxDetailInclude.Size = new System.Drawing.Size(376, 17);
            this.checkBoxDetailInclude.TabIndex = 1;
            this.checkBoxDetailInclude.Text = "Include this element in the merge";
            this.checkBoxDetailInclude.UseVisualStyleBackColor = true;
            this.checkBoxDetailInclude.CheckedChanged += new System.EventHandler(this.checkBoxDetailInclude_CheckedChanged);
            // 
            // labelDetailPath
            // 
            this.labelDetailPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDetailPath.Location = new System.Drawing.Point(3, 6);
            this.labelDetailPath.Name = "labelDetailPath";
            this.labelDetailPath.Size = new System.Drawing.Size(379, 22);
            this.labelDetailPath.TabIndex = 0;
            this.labelDetailPath.Text = "labelDetailPath";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNewProject,
            this.toolStripButtonOpen,
            this.toolStripButtonSaveProject,
            this.toolStripSeparator5,
            this.toolStripButtonMergePDF,
            this.toolStripButtonSplitPDF});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(780, 23);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonNewProject
            // 
            this.toolStripButtonNewProject.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNewProject.Image")));
            this.toolStripButtonNewProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNewProject.Name = "toolStripButtonNewProject";
            this.toolStripButtonNewProject.Size = new System.Drawing.Size(91, 20);
            this.toolStripButtonNewProject.Text = "New Project";
            this.toolStripButtonNewProject.Click += new System.EventHandler(this.toolStripButtonNewProject_Click);
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpen.Image")));
            this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(96, 20);
            this.toolStripButtonOpen.Text = "Open Project";
            this.toolStripButtonOpen.Click += new System.EventHandler(this.toolStripButtonOpen_Click);
            // 
            // toolStripButtonSaveProject
            // 
            this.toolStripButtonSaveProject.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSaveProject.Image")));
            this.toolStripButtonSaveProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveProject.Name = "toolStripButtonSaveProject";
            this.toolStripButtonSaveProject.Size = new System.Drawing.Size(91, 20);
            this.toolStripButtonSaveProject.Text = "Save Project";
            this.toolStripButtonSaveProject.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 23);
            // 
            // toolStripButtonMergePDF
            // 
            this.toolStripButtonMergePDF.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMergePDF.Image")));
            this.toolStripButtonMergePDF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMergePDF.Name = "toolStripButtonMergePDF";
            this.toolStripButtonMergePDF.Size = new System.Drawing.Size(88, 20);
            this.toolStripButtonMergePDF.Text = "Merge PDF!";
            this.toolStripButtonMergePDF.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButtonSplitPDF
            // 
            this.toolStripButtonSplitPDF.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSplitPDF.Image")));
            this.toolStripButtonSplitPDF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSplitPDF.Name = "toolStripButtonSplitPDF";
            this.toolStripButtonSplitPDF.Size = new System.Drawing.Size(164, 20);
            this.toolStripButtonSplitPDF.Text = "Split into single page files!";
            this.toolStripButtonSplitPDF.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(780, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.toolStripSeparator1,
            this.addToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveProjectToolStripMenuItem,
            this.saveProjectAsToolStripMenuItem,
            this.toolStripSeparator3,
            this.recentProjectsToolStripMenuItem,
            this.toolStripSeparator4,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.newProjectToolStripMenuItem.Text = "New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonNewProject_Click);
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.openProjectToolStripMenuItem.Text = "Open Project";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonOpen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pDFToolStripMenuItem,
            this.imageFileToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // pDFToolStripMenuItem
            // 
            this.pDFToolStripMenuItem.Name = "pDFToolStripMenuItem";
            this.pDFToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.pDFToolStripMenuItem.Text = "PDF File";
            this.pDFToolStripMenuItem.Click += new System.EventHandler(this.pDFToolStripMenuItem_Click);
            // 
            // imageFileToolStripMenuItem
            // 
            this.imageFileToolStripMenuItem.Name = "imageFileToolStripMenuItem";
            this.imageFileToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.imageFileToolStripMenuItem.Text = "Image File";
            this.imageFileToolStripMenuItem.Click += new System.EventHandler(this.imageFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(152, 6);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveProjectToolStripMenuItem.Text = "Save Project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // saveProjectAsToolStripMenuItem
            // 
            this.saveProjectAsToolStripMenuItem.Name = "saveProjectAsToolStripMenuItem";
            this.saveProjectAsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveProjectAsToolStripMenuItem.Text = "Save Project As";
            this.saveProjectAsToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(152, 6);
            // 
            // recentProjectsToolStripMenuItem
            // 
            this.recentProjectsToolStripMenuItem.Name = "recentProjectsToolStripMenuItem";
            this.recentProjectsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.recentProjectsToolStripMenuItem.Text = "Recent Projects";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(152, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileAssociationToolStripMenuItem,
            this.toolStripSettings});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // fileAssociationToolStripMenuItem
            // 
            this.fileAssociationToolStripMenuItem.Name = "fileAssociationToolStripMenuItem";
            this.fileAssociationToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.fileAssociationToolStripMenuItem.Text = "File Association";
            this.fileAssociationToolStripMenuItem.Click += new System.EventHandler(this.fileAssociationToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // openProjectDialog1
            // 
            this.openProjectDialog1.DefaultExt = "giospsm";
            this.openProjectDialog1.Filter = "PDF Splitter Merger Project Files (*.giospsm)|*.giospsm";
            this.openProjectDialog1.Title = "Open Project";
            // 
            // saveProjectDialog1
            // 
            this.saveProjectDialog1.DefaultExt = "giospsm";
            this.saveProjectDialog1.Filter = "PDF Splitter Merger Project Files|*.giospsm";
            this.saveProjectDialog1.Title = "Save Project";
            // 
            // openFileDialogAddPDF
            // 
            this.openFileDialogAddPDF.DefaultExt = "pdf";
            this.openFileDialogAddPDF.Filter = "PDF Documents (*.pdf)|*.pdf";
            this.openFileDialogAddPDF.Multiselect = true;
            // 
            // openFileDialogAddImage
            // 
            this.openFileDialogAddImage.DefaultExt = "pdf";
            this.openFileDialogAddImage.Filter = "Image Files (*.jpg)|*.jpg";
            this.openFileDialogAddImage.Multiselect = true;
            // 
            // saveFileDialogTarget
            // 
            this.saveFileDialogTarget.DefaultExt = "pdf";
            this.saveFileDialogTarget.Filter = "PDF Files|*.pdf";
            this.saveFileDialogTarget.OverwritePrompt = false;
            this.saveFileDialogTarget.Title = "Choose Target Location ...";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 360);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(780, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripSettings
            // 
            this.toolStripSettings.Name = "toolStripSettings";
            this.toolStripSettings.Size = new System.Drawing.Size(156, 22);
            this.toolStripSettings.Text = "Settings...";
            this.toolStripSettings.Click += new System.EventHandler(this.toolStripSettings_Click);
            // 
            // ProjectForm
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(780, 382);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ProjectForm";
            this.Text = "ProjectForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProjectForm_FormClosing);
            this.Load += new System.EventHandler(this.ProjectForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ProjectForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ProjectForm_DragEnter);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panelCommonDetails.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxOpenAfterCreation;
        private System.Windows.Forms.CheckBox checkBoxOverwriteWithoutPrompt;
        private System.Windows.Forms.CheckBox checkBoxAlwaysAskDestination;
        private System.Windows.Forms.TextBox textBoxTarget;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveProject;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panelCommonDetails;
        private System.Windows.Forms.Label labelDetailPath;
        private System.Windows.Forms.CheckBox checkBoxDetailInclude;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pDFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonNewProject;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
        private System.Windows.Forms.OpenFileDialog openProjectDialog1;
        private System.Windows.Forms.SaveFileDialog saveProjectDialog1;
        private System.Windows.Forms.ToolStripMenuItem saveProjectAsToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogAddPDF;
        private System.Windows.Forms.OpenFileDialog openFileDialogAddImage;
        private System.Windows.Forms.Button buttonPickTarget;
        private System.Windows.Forms.SaveFileDialog saveFileDialogTarget;
        private System.Windows.Forms.ToolStripMenuItem recentProjectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButtonMergePDF;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem fileAssociationToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonSplitPDF;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem toolStripSettings;
    }
}