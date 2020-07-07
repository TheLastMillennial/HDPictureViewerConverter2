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
            this.label1 = new System.Windows.Forms.Label();
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
            this.errorsTxtBox = new System.Windows.Forms.RichTextBox();
            this.squaresLbl = new System.Windows.Forms.Label();
            this.newDimensionsLbl = new System.Windows.Forms.Label();
            this.origDimensionsLbl = new System.Windows.Forms.Label();
            this.OptionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subPicBox)).BeginInit();
            this.infoBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpenImgBtn
            // 
            this.OpenImgBtn.AllowDrop = true;
            this.OpenImgBtn.Location = new System.Drawing.Point(12, 13);
            this.OpenImgBtn.Name = "OpenImgBtn";
            this.OpenImgBtn.Size = new System.Drawing.Size(205, 97);
            this.OpenImgBtn.TabIndex = 0;
            this.OpenImgBtn.Text = "Select and Convert Images";
            this.OpenImgBtn.UseVisualStyleBackColor = true;
            this.OpenImgBtn.Click += new System.EventHandler(this.OpenImgBtn_Click);
            // 
            // OpenConvertedBtn
            // 
            this.OpenConvertedBtn.Location = new System.Drawing.Point(12, 116);
            this.OpenConvertedBtn.Name = "OpenConvertedBtn";
            this.OpenConvertedBtn.Size = new System.Drawing.Size(204, 92);
            this.OpenConvertedBtn.TabIndex = 1;
            this.OpenConvertedBtn.Text = "Find Converted Images";
            this.OpenConvertedBtn.UseVisualStyleBackColor = true;
            this.OpenConvertedBtn.Click += new System.EventHandler(this.OpenConvertedBtn_Click);
            // 
            // OptionsGroupBox
            // 
            this.OptionsGroupBox.Controls.Add(this.label1);
            this.OptionsGroupBox.Controls.Add(this.resizeComboBox);
            this.OptionsGroupBox.Location = new System.Drawing.Point(12, 239);
            this.OptionsGroupBox.Name = "OptionsGroupBox";
            this.OptionsGroupBox.Size = new System.Drawing.Size(571, 132);
            this.OptionsGroupBox.TabIndex = 2;
            this.OptionsGroupBox.TabStop = false;
            this.OptionsGroupBox.Text = "Options";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Resize Options:";
            // 
            // resizeComboBox
            // 
            this.resizeComboBox.AllowDrop = true;
            this.resizeComboBox.FormattingEnabled = true;
            this.resizeComboBox.Items.AddRange(new object[] {
            "Do not resize image",
            "Maintain aspect ratio",
            "Stretch to fit"});
            this.resizeComboBox.Location = new System.Drawing.Point(9, 69);
            this.resizeComboBox.Name = "resizeComboBox";
            this.resizeComboBox.Size = new System.Drawing.Size(335, 33);
            this.resizeComboBox.TabIndex = 1;
            this.AllToolTip.SetToolTip(this.resizeComboBox, "Which resizing method should the program use?");
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
            this.pictureBox.Location = new System.Drawing.Point(719, 22);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(320, 240);
            this.pictureBox.TabIndex = 3;
            this.pictureBox.TabStop = false;
            // 
            // subPicBox
            // 
            this.subPicBox.Location = new System.Drawing.Point(362, 22);
            this.subPicBox.Name = "subPicBox";
            this.subPicBox.Size = new System.Drawing.Size(222, 211);
            this.subPicBox.TabIndex = 4;
            this.subPicBox.TabStop = false;
            this.subPicBox.Click += new System.EventHandler(this.subPicBox_Click);
            // 
            // subPicLabel
            // 
            this.subPicLabel.AutoSize = true;
            this.subPicLabel.Location = new System.Drawing.Point(263, 13);
            this.subPicLabel.Name = "subPicLabel";
            this.subPicLabel.Size = new System.Drawing.Size(93, 25);
            this.subPicLabel.TabIndex = 5;
            this.subPicLabel.Text = "Sub-Pic:";
            // 
            // MainPicLabel
            // 
            this.MainPicLabel.AutoSize = true;
            this.MainPicLabel.Location = new System.Drawing.Point(612, 22);
            this.MainPicLabel.Name = "MainPicLabel";
            this.MainPicLabel.Size = new System.Drawing.Size(101, 25);
            this.MainPicLabel.TabIndex = 6;
            this.MainPicLabel.Text = "Main Pic:";
            // 
            // progBar
            // 
            this.progBar.Location = new System.Drawing.Point(11, 419);
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(572, 65);
            this.progBar.TabIndex = 7;
            // 
            // progInfoLbl
            // 
            this.progInfoLbl.AutoSize = true;
            this.progInfoLbl.Location = new System.Drawing.Point(11, 391);
            this.progInfoLbl.MaximumSize = new System.Drawing.Size(500, 0);
            this.progInfoLbl.Name = "progInfoLbl";
            this.progInfoLbl.Size = new System.Drawing.Size(139, 25);
            this.progInfoLbl.TabIndex = 8;
            this.progInfoLbl.Text = "Progress Info";
            // 
            // infoBox
            // 
            this.infoBox.Controls.Add(this.errorsTxtBox);
            this.infoBox.Controls.Add(this.squaresLbl);
            this.infoBox.Controls.Add(this.newDimensionsLbl);
            this.infoBox.Controls.Add(this.origDimensionsLbl);
            this.infoBox.Location = new System.Drawing.Point(11, 507);
            this.infoBox.Name = "infoBox";
            this.infoBox.Size = new System.Drawing.Size(572, 446);
            this.infoBox.TabIndex = 9;
            this.infoBox.TabStop = false;
            this.infoBox.Text = "Advanced Information:";
            // 
            // errorsTxtBox
            // 
            this.errorsTxtBox.BackColor = System.Drawing.SystemColors.Control;
            this.errorsTxtBox.Location = new System.Drawing.Point(13, 117);
            this.errorsTxtBox.Name = "errorsTxtBox";
            this.errorsTxtBox.ReadOnly = true;
            this.errorsTxtBox.ShortcutsEnabled = false;
            this.errorsTxtBox.Size = new System.Drawing.Size(541, 305);
            this.errorsTxtBox.TabIndex = 10;
            this.errorsTxtBox.Text = "Messages Encountered:\n";
            // 
            // squaresLbl
            // 
            this.squaresLbl.AutoSize = true;
            this.squaresLbl.Location = new System.Drawing.Point(12, 89);
            this.squaresLbl.Name = "squaresLbl";
            this.squaresLbl.Size = new System.Drawing.Size(154, 25);
            this.squaresLbl.TabIndex = 2;
            this.squaresLbl.Text = "Squares Used:";
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
            // HDpicConverterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1838, 1040);
            this.Controls.Add(this.infoBox);
            this.Controls.Add(this.progInfoLbl);
            this.Controls.Add(this.progBar);
            this.Controls.Add(this.MainPicLabel);
            this.Controls.Add(this.subPicLabel);
            this.Controls.Add(this.subPicBox);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.OptionsGroupBox);
            this.Controls.Add(this.OpenConvertedBtn);
            this.Controls.Add(this.OpenImgBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HDpicConverterForm";
            this.Text = "HD Picture Viewer Converter 2";
            this.OptionsGroupBox.ResumeLayout(false);
            this.OptionsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subPicBox)).EndInit();
            this.infoBox.ResumeLayout(false);
            this.infoBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenImgBtn;
        private System.Windows.Forms.Button OpenConvertedBtn;
        private System.Windows.Forms.GroupBox OptionsGroupBox;
        public System.Windows.Forms.ToolTip AllToolTip;
        private System.Windows.Forms.ComboBox resizeComboBox;
        private System.Windows.Forms.Label label1;
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
    }
}

