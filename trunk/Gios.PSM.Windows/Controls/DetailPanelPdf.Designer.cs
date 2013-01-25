namespace Gios.PDF.SplitMerge.Controls
{
    partial class DetailPanelPdf
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelStatusPageSelection = new System.Windows.Forms.Label();
            this.radioButtonIncludeAllPages = new System.Windows.Forms.RadioButton();
            this.radioButtonIncludePageRange = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.471698F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(20, 103);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(276, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelStatusPageSelection
            // 
            this.labelStatusPageSelection.AutoSize = true;
            this.labelStatusPageSelection.Location = new System.Drawing.Point(17, 126);
            this.labelStatusPageSelection.Name = "labelStatusPageSelection";
            this.labelStatusPageSelection.Size = new System.Drawing.Size(35, 13);
            this.labelStatusPageSelection.TabIndex = 2;
            this.labelStatusPageSelection.Text = "label2";
            // 
            // radioButtonIncludeAllPages
            // 
            this.radioButtonIncludeAllPages.AutoSize = true;
            this.radioButtonIncludeAllPages.Location = new System.Drawing.Point(3, 49);
            this.radioButtonIncludeAllPages.Name = "radioButtonIncludeAllPages";
            this.radioButtonIncludeAllPages.Size = new System.Drawing.Size(107, 17);
            this.radioButtonIncludeAllPages.TabIndex = 3;
            this.radioButtonIncludeAllPages.TabStop = true;
            this.radioButtonIncludeAllPages.Text = "Include All Pages";
            this.radioButtonIncludeAllPages.UseVisualStyleBackColor = true;
            this.radioButtonIncludeAllPages.CheckedChanged += new System.EventHandler(this.radioButtonIncludeAllPages_CheckedChanged);
            // 
            // radioButtonIncludePageRange
            // 
            this.radioButtonIncludePageRange.AutoSize = true;
            this.radioButtonIncludePageRange.Location = new System.Drawing.Point(3, 78);
            this.radioButtonIncludePageRange.Name = "radioButtonIncludePageRange";
            this.radioButtonIncludePageRange.Size = new System.Drawing.Size(144, 17);
            this.radioButtonIncludePageRange.TabIndex = 3;
            this.radioButtonIncludePageRange.TabStop = true;
            this.radioButtonIncludePageRange.Text = "Include this page ranges:";
            this.radioButtonIncludePageRange.UseVisualStyleBackColor = true;
            this.radioButtonIncludePageRange.CheckedChanged += new System.EventHandler(this.radioButtonIncludePageRange_CheckedChanged);
            // 
            // DetailPanelPdf
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.radioButtonIncludePageRange);
            this.Controls.Add(this.radioButtonIncludeAllPages);
            this.Controls.Add(this.labelStatusPageSelection);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "DetailPanelPdf";
            this.Size = new System.Drawing.Size(357, 185);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelStatusPageSelection;
        private System.Windows.Forms.RadioButton radioButtonIncludeAllPages;
        private System.Windows.Forms.RadioButton radioButtonIncludePageRange;
    }
}
