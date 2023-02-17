namespace HDPictureViewerConverter
{
    partial class HDpicConverterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HDpicConverterForm));
            this.OpenImgBtn = new System.Windows.Forms.Button();
            this.OpenConvertedBtn = new System.Windows.Forms.Button();
            this.OptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.maxCores = new System.Windows.Forms.NumericUpDown();
            this.CoresLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ResizeDescLabel = new System.Windows.Forms.Label();
            this.verboseLogging = new System.Windows.Forms.CheckBox();
            this.resizeLabel = new System.Windows.Forms.Label();
            this.resizeComboBox = new System.Windows.Forms.ComboBox();
            this.AllToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.selectImagesDialog = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.subPicBox = new System.Windows.Forms.PictureBox();
            this.subPicLabel = new System.Windows.Forms.Label();
            this.MainPicLabel = new System.Windows.Forms.Label();
            this.progBar = new System.Windows.Forms.ProgressBar();
            this.progInfoLbl = new System.Windows.Forms.Label();
            this.infoBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.errorsTxtBox = new System.Windows.Forms.RichTextBox();
            this.squaresLbl = new System.Windows.Forms.Label();
            this.newDimensionsLbl = new System.Windows.Forms.Label();
            this.origDimensionsLbl = new System.Windows.Forms.Label();
            this.creditLabel = new System.Windows.Forms.Label();
            this.convertBox = new System.Windows.Forms.GroupBox();
            this.overlayConvert = new System.Windows.Forms.Label();
            this.OptionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxCores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subPicBox)).BeginInit();
            this.infoBox.SuspendLayout();
            this.convertBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpenImgBtn
            // 
            this.OpenImgBtn.AllowDrop = true;
            this.OpenImgBtn.ForeColor = System.Drawing.Color.Black;
            this.OpenImgBtn.Location = new System.Drawing.Point(13, 51);
            this.OpenImgBtn.Name = "OpenImgBtn";
            this.OpenImgBtn.Size = new System.Drawing.Size(265, 97);
            this.OpenImgBtn.TabIndex = 0;
            this.OpenImgBtn.Text = "Select and Convert Pictures";
            this.OpenImgBtn.UseVisualStyleBackColor = true;
            this.OpenImgBtn.Click += new System.EventHandler(this.OpenImgBtn_Click);
            // 
            // OpenConvertedBtn
            // 
            this.OpenConvertedBtn.ForeColor = System.Drawing.Color.Black;
            this.OpenConvertedBtn.Location = new System.Drawing.Point(289, 51);
            this.OpenConvertedBtn.Name = "OpenConvertedBtn";
            this.OpenConvertedBtn.Size = new System.Drawing.Size(265, 97);
            this.OpenConvertedBtn.TabIndex = 1;
            this.OpenConvertedBtn.Text = "Find Converted Pictures";
            this.OpenConvertedBtn.UseVisualStyleBackColor = true;
            this.OpenConvertedBtn.Click += new System.EventHandler(this.OpenConvertedBtn_Click);
            // 
            // OptionsGroupBox
            // 
            this.OptionsGroupBox.Controls.Add(this.maxCores);
            this.OptionsGroupBox.Controls.Add(this.CoresLabel);
            this.OptionsGroupBox.Controls.Add(this.label1);
            this.OptionsGroupBox.Controls.Add(this.ResizeDescLabel);
            this.OptionsGroupBox.Controls.Add(this.verboseLogging);
            this.OptionsGroupBox.Controls.Add(this.resizeLabel);
            this.OptionsGroupBox.Controls.Add(this.resizeComboBox);
            this.OptionsGroupBox.Location = new System.Drawing.Point(594, 21);
            this.OptionsGroupBox.Name = "OptionsGroupBox";
            this.OptionsGroupBox.Size = new System.Drawing.Size(571, 316);
            this.OptionsGroupBox.TabIndex = 2;
            this.OptionsGroupBox.TabStop = false;
            this.OptionsGroupBox.Text = "Options";
            // 
            // maxCores
            // 
            this.maxCores.Location = new System.Drawing.Point(135, 159);
            this.maxCores.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.maxCores.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxCores.Name = "maxCores";
            this.maxCores.Size = new System.Drawing.Size(76, 31);
            this.maxCores.TabIndex = 15;
            this.maxCores.ThousandsSeparator = true;
            this.maxCores.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxCores.ValueChanged += new System.EventHandler(this.maxCores_ValueChanged);
            // 
            // CoresLabel
            // 
            this.CoresLabel.AutoSize = true;
            this.CoresLabel.Location = new System.Drawing.Point(7, 161);
            this.CoresLabel.Name = "CoresLabel";
            this.CoresLabel.Size = new System.Drawing.Size(122, 25);
            this.CoresLabel.TabIndex = 14;
            this.CoresLabel.Text = "Max Cores:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 25);
            this.label1.TabIndex = 12;
            this.label1.Text = "Options";
            // 
            // ResizeDescLabel
            // 
            this.ResizeDescLabel.AutoSize = true;
            this.ResizeDescLabel.Location = new System.Drawing.Point(7, 73);
            this.ResizeDescLabel.Name = "ResizeDescLabel";
            this.ResizeDescLabel.Size = new System.Drawing.Size(132, 25);
            this.ResizeDescLabel.TabIndex = 4;
            this.ResizeDescLabel.Text = "Description: ";
            // 
            // verboseLogging
            // 
            this.verboseLogging.AutoSize = true;
            this.verboseLogging.Location = new System.Drawing.Point(12, 267);
            this.verboseLogging.Name = "verboseLogging";
            this.verboseLogging.Size = new System.Drawing.Size(207, 29);
            this.verboseLogging.TabIndex = 3;
            this.verboseLogging.Text = "Verbose Logging";
            this.verboseLogging.UseVisualStyleBackColor = true;
            this.verboseLogging.CheckedChanged += new System.EventHandler(this.verboseLogging_CheckedChanged);
            // 
            // resizeLabel
            // 
            this.resizeLabel.AutoSize = true;
            this.resizeLabel.Location = new System.Drawing.Point(7, 31);
            this.resizeLabel.Name = "resizeLabel";
            this.resizeLabel.Size = new System.Drawing.Size(164, 25);
            this.resizeLabel.TabIndex = 2;
            this.resizeLabel.Text = "Resize Options:";
            // 
            // resizeComboBox
            // 
            this.resizeComboBox.AllowDrop = true;
            this.resizeComboBox.BackColor = System.Drawing.Color.Black;
            this.resizeComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.resizeComboBox.FormattingEnabled = true;
            this.resizeComboBox.Items.AddRange(new object[] {
            "Do not resize image.",
            "Maintain aspect ratio.",
            "Stretch to fit screen."});
            this.resizeComboBox.Location = new System.Drawing.Point(177, 31);
            this.resizeComboBox.Name = "resizeComboBox";
            this.resizeComboBox.Size = new System.Drawing.Size(335, 33);
            this.resizeComboBox.TabIndex = 1;
            this.AllToolTip.SetToolTip(this.resizeComboBox, "Which resizing method should the program use?");
            this.resizeComboBox.SelectedIndexChanged += new System.EventHandler(this.resizeComboBox_SelectedIndexChanged);
            // 
            // AllToolTip
            // 
            this.AllToolTip.AutoPopDelay = 10000;
            this.AllToolTip.InitialDelay = 500;
            this.AllToolTip.ReshowDelay = 100;
            this.AllToolTip.ToolTipTitle = "Description:";
            // 
            // selectImagesDialog
            // 
            this.selectImagesDialog.FileName = "selectImagesDialog";
            this.selectImagesDialog.Multiselect = true;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(702, 578);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(320, 240);
            this.pictureBox.TabIndex = 3;
            this.pictureBox.TabStop = false;
            this.pictureBox.Visible = false;
            // 
            // subPicBox
            // 
            this.subPicBox.Location = new System.Drawing.Point(702, 361);
            this.subPicBox.Name = "subPicBox";
            this.subPicBox.Size = new System.Drawing.Size(222, 211);
            this.subPicBox.TabIndex = 4;
            this.subPicBox.TabStop = false;
            this.subPicBox.Visible = false;
            this.subPicBox.Click += new System.EventHandler(this.subPicBox_Click);
            // 
            // subPicLabel
            // 
            this.subPicLabel.AutoSize = true;
            this.subPicLabel.Location = new System.Drawing.Point(595, 361);
            this.subPicLabel.Name = "subPicLabel";
            this.subPicLabel.Size = new System.Drawing.Size(93, 25);
            this.subPicLabel.TabIndex = 5;
            this.subPicLabel.Text = "Sub-Pic:";
            this.subPicLabel.Visible = false;
            // 
            // MainPicLabel
            // 
            this.MainPicLabel.AutoSize = true;
            this.MainPicLabel.Location = new System.Drawing.Point(595, 578);
            this.MainPicLabel.Name = "MainPicLabel";
            this.MainPicLabel.Size = new System.Drawing.Size(101, 25);
            this.MainPicLabel.TabIndex = 6;
            this.MainPicLabel.Text = "Main Pic:";
            this.MainPicLabel.Visible = false;
            // 
            // progBar
            // 
            this.progBar.Location = new System.Drawing.Point(13, 194);
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(541, 65);
            this.progBar.TabIndex = 7;
            // 
            // progInfoLbl
            // 
            this.progInfoLbl.AutoSize = true;
            this.progInfoLbl.Location = new System.Drawing.Point(16, 166);
            this.progInfoLbl.MaximumSize = new System.Drawing.Size(500, 0);
            this.progInfoLbl.Name = "progInfoLbl";
            this.progInfoLbl.Size = new System.Drawing.Size(98, 25);
            this.progInfoLbl.TabIndex = 8;
            this.progInfoLbl.Text = "Progress";
            // 
            // infoBox
            // 
            this.infoBox.Controls.Add(this.label3);
            this.infoBox.Controls.Add(this.errorsTxtBox);
            this.infoBox.Controls.Add(this.squaresLbl);
            this.infoBox.Controls.Add(this.newDimensionsLbl);
            this.infoBox.Controls.Add(this.origDimensionsLbl);
            this.infoBox.Location = new System.Drawing.Point(17, 343);
            this.infoBox.Name = "infoBox";
            this.infoBox.Size = new System.Drawing.Size(572, 446);
            this.infoBox.TabIndex = 9;
            this.infoBox.TabStop = false;
            this.infoBox.Text = "Advanced Information";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(220, 25);
            this.label3.TabIndex = 12;
            this.label3.Text = "Advanced Information";
            // 
            // errorsTxtBox
            // 
            this.errorsTxtBox.BackColor = System.Drawing.Color.Black;
            this.errorsTxtBox.ForeColor = System.Drawing.Color.Green;
            this.errorsTxtBox.Location = new System.Drawing.Point(13, 117);
            this.errorsTxtBox.Name = "errorsTxtBox";
            this.errorsTxtBox.ReadOnly = true;
            this.errorsTxtBox.Size = new System.Drawing.Size(541, 305);
            this.errorsTxtBox.TabIndex = 10;
            this.errorsTxtBox.Text = "Logs:";
            // 
            // squaresLbl
            // 
            this.squaresLbl.AutoSize = true;
            this.squaresLbl.Location = new System.Drawing.Point(12, 89);
            this.squaresLbl.Name = "squaresLbl";
            this.squaresLbl.Size = new System.Drawing.Size(154, 25);
            this.squaresLbl.TabIndex = 2;
            this.squaresLbl.Text = "Squares Used:";
            this.squaresLbl.Visible = false;
            // 
            // newDimensionsLbl
            // 
            this.newDimensionsLbl.AutoSize = true;
            this.newDimensionsLbl.Location = new System.Drawing.Point(12, 64);
            this.newDimensionsLbl.Name = "newDimensionsLbl";
            this.newDimensionsLbl.Size = new System.Drawing.Size(178, 25);
            this.newDimensionsLbl.TabIndex = 1;
            this.newDimensionsLbl.Text = "New Dimensions:";
            // 
            // origDimensionsLbl
            // 
            this.origDimensionsLbl.AutoSize = true;
            this.origDimensionsLbl.Location = new System.Drawing.Point(12, 39);
            this.origDimensionsLbl.Name = "origDimensionsLbl";
            this.origDimensionsLbl.Size = new System.Drawing.Size(216, 25);
            this.origDimensionsLbl.TabIndex = 0;
            this.origDimensionsLbl.Text = "Original Dimensions: ";
            // 
            // creditLabel
            // 
            this.creditLabel.AutoSize = true;
            this.creditLabel.Location = new System.Drawing.Point(12, 819);
            this.creditLabel.Name = "creditLabel";
            this.creditLabel.Size = new System.Drawing.Size(248, 25);
            this.creditLabel.TabIndex = 10;
            this.creditLabel.Text = "TheLastMillennial - 2022";
            // 
            // convertBox
            // 
            this.convertBox.Controls.Add(this.overlayConvert);
            this.convertBox.Controls.Add(this.OpenImgBtn);
            this.convertBox.Controls.Add(this.OpenConvertedBtn);
            this.convertBox.Controls.Add(this.progInfoLbl);
            this.convertBox.Controls.Add(this.progBar);
            this.convertBox.Location = new System.Drawing.Point(17, 21);
            this.convertBox.Name = "convertBox";
            this.convertBox.Size = new System.Drawing.Size(571, 316);
            this.convertBox.TabIndex = 11;
            this.convertBox.TabStop = false;
            this.convertBox.Text = "Convert Pictures";
            // 
            // overlayConvert
            // 
            this.overlayConvert.AutoSize = true;
            this.overlayConvert.Location = new System.Drawing.Point(12, 0);
            this.overlayConvert.Name = "overlayConvert";
            this.overlayConvert.Size = new System.Drawing.Size(171, 25);
            this.overlayConvert.TabIndex = 9;
            this.overlayConvert.Text = "Convert Pictures";
            // 
            // HDpicConverterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(8)))), ((int)(((byte)(8)))));
            this.ClientSize = new System.Drawing.Size(1211, 881);
            this.Controls.Add(this.convertBox);
            this.Controls.Add(this.creditLabel);
            this.Controls.Add(this.infoBox);
            this.Controls.Add(this.MainPicLabel);
            this.Controls.Add(this.subPicLabel);
            this.Controls.Add(this.subPicBox);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.OptionsGroupBox);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HDpicConverterForm";
            this.Text = "HD Picture Viewer Converter 2";
            this.OptionsGroupBox.ResumeLayout(false);
            this.OptionsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxCores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subPicBox)).EndInit();
            this.infoBox.ResumeLayout(false);
            this.infoBox.PerformLayout();
            this.convertBox.ResumeLayout(false);
            this.convertBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenImgBtn;
        private System.Windows.Forms.Button OpenConvertedBtn;
        private System.Windows.Forms.GroupBox OptionsGroupBox;
        public System.Windows.Forms.ToolTip AllToolTip;
        private System.Windows.Forms.ComboBox resizeComboBox;
        private System.Windows.Forms.Label resizeLabel;
        private System.Windows.Forms.OpenFileDialog selectImagesDialog;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.PictureBox subPicBox;
        private System.Windows.Forms.Label subPicLabel;
        private System.Windows.Forms.Label MainPicLabel;
        private System.Windows.Forms.ProgressBar progBar;
        private System.Windows.Forms.Label progInfoLbl;
        private System.Windows.Forms.GroupBox infoBox;
        private System.Windows.Forms.Label squaresLbl;
        private System.Windows.Forms.Label newDimensionsLbl;
        private System.Windows.Forms.Label origDimensionsLbl;
        private System.Windows.Forms.RichTextBox errorsTxtBox;
        private System.Windows.Forms.Label creditLabel;
        private System.Windows.Forms.CheckBox verboseLogging;
        private System.Windows.Forms.GroupBox convertBox;
        private System.Windows.Forms.Label ResizeDescLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label overlayConvert;
        private System.Windows.Forms.NumericUpDown maxCores;
        private System.Windows.Forms.Label CoresLabel;
    }
}

