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
            this.importPicBtn = new System.Windows.Forms.Button();
            this.FindConvertedPicBtn = new System.Windows.Forms.Button();
            this.OptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.advancedMode = new System.Windows.Forms.CheckBox();
            this.maxCores = new System.Windows.Forms.NumericUpDown();
            this.CoresLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ResizeDescLabel = new System.Windows.Forms.Label();
            this.resizeLabel = new System.Windows.Forms.Label();
            this.resizeComboBox = new System.Windows.Forms.ComboBox();
            this.AllToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.idLbl = new System.Windows.Forms.Label();
            this.picturePathLbl = new System.Windows.Forms.Label();
            this.convertPicBtn = new System.Windows.Forms.Button();
            this.DeleteAllFilesBtn = new System.Windows.Forms.Button();
            this.DeleteQueueBtn = new System.Windows.Forms.Button();
            this.selectImagesDialog = new System.Windows.Forms.OpenFileDialog();
            this.progBar = new System.Windows.Forms.ProgressBar();
            this.infoBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.squaresLbl = new System.Windows.Forms.Label();
            this.newDimensionsLbl = new System.Windows.Forms.Label();
            this.origDimensionsLbl = new System.Windows.Forms.Label();
            this.creditLabel = new System.Windows.Forms.Label();
            this.convertBox = new System.Windows.Forms.GroupBox();
            this.progInfoLbl = new System.Windows.Forms.Label();
            this.pictureListTable = new System.Windows.Forms.TableLayoutPanel();
            this.overlayConvert = new System.Windows.Forms.Label();
            this.StopConversionBtn = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.subPicBox = new System.Windows.Forms.PictureBox();
            this.OptionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxCores)).BeginInit();
            this.infoBox.SuspendLayout();
            this.convertBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subPicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // importPicBtn
            // 
            this.importPicBtn.AllowDrop = true;
            this.importPicBtn.ForeColor = System.Drawing.Color.Black;
            this.importPicBtn.Location = new System.Drawing.Point(13, 31);
            this.importPicBtn.Name = "importPicBtn";
            this.importPicBtn.Size = new System.Drawing.Size(170, 48);
            this.importPicBtn.TabIndex = 0;
            this.importPicBtn.Text = "Import Pictures";
            this.AllToolTip.SetToolTip(this.importPicBtn, "Select pictures to be converted.");
            this.importPicBtn.UseVisualStyleBackColor = true;
            this.importPicBtn.Click += new System.EventHandler(this.OpenImgBtn_Click);
            // 
            // FindConvertedPicBtn
            // 
            this.FindConvertedPicBtn.ForeColor = System.Drawing.Color.Black;
            this.FindConvertedPicBtn.Location = new System.Drawing.Point(293, 576);
            this.FindConvertedPicBtn.Name = "FindConvertedPicBtn";
            this.FindConvertedPicBtn.Size = new System.Drawing.Size(265, 57);
            this.FindConvertedPicBtn.TabIndex = 5;
            this.FindConvertedPicBtn.Text = "Find Converted Pictures";
            this.AllToolTip.SetToolTip(this.FindConvertedPicBtn, "Find where converted pictures were saved.");
            this.FindConvertedPicBtn.UseVisualStyleBackColor = true;
            this.FindConvertedPicBtn.Click += new System.EventHandler(this.OpenConvertedBtn_Click);
            // 
            // OptionsGroupBox
            // 
            this.OptionsGroupBox.Controls.Add(this.advancedMode);
            this.OptionsGroupBox.Controls.Add(this.maxCores);
            this.OptionsGroupBox.Controls.Add(this.CoresLabel);
            this.OptionsGroupBox.Controls.Add(this.label1);
            this.OptionsGroupBox.Controls.Add(this.ResizeDescLabel);
            this.OptionsGroupBox.Controls.Add(this.resizeLabel);
            this.OptionsGroupBox.Controls.Add(this.resizeComboBox);
            this.OptionsGroupBox.Location = new System.Drawing.Point(594, 21);
            this.OptionsGroupBox.Name = "OptionsGroupBox";
            this.OptionsGroupBox.Size = new System.Drawing.Size(571, 192);
            this.OptionsGroupBox.TabIndex = 2;
            this.OptionsGroupBox.TabStop = false;
            this.OptionsGroupBox.Text = "Options";
            // 
            // advancedMode
            // 
            this.advancedMode.AutoSize = true;
            this.advancedMode.Location = new System.Drawing.Point(12, 148);
            this.advancedMode.Name = "advancedMode";
            this.advancedMode.Size = new System.Drawing.Size(200, 29);
            this.advancedMode.TabIndex = 7;
            this.advancedMode.Text = "Advanced Mode";
            this.AllToolTip.SetToolTip(this.advancedMode, "Enable verbose logging and show hidden features.");
            this.advancedMode.UseVisualStyleBackColor = true;
            this.advancedMode.CheckedChanged += new System.EventHandler(this.verboseLogging_CheckedChanged_1);
            // 
            // maxCores
            // 
            this.maxCores.Location = new System.Drawing.Point(478, 147);
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
            this.maxCores.TabIndex = 8;
            this.maxCores.ThousandsSeparator = true;
            this.AllToolTip.SetToolTip(this.maxCores, "Maximum number of convimg instances that will be launched when converting large i" +
        "mages.");
            this.maxCores.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxCores.Visible = false;
            this.maxCores.ValueChanged += new System.EventHandler(this.maxCores_ValueChanged);
            // 
            // CoresLabel
            // 
            this.CoresLabel.AutoSize = true;
            this.CoresLabel.Location = new System.Drawing.Point(350, 149);
            this.CoresLabel.Name = "CoresLabel";
            this.CoresLabel.Size = new System.Drawing.Size(122, 25);
            this.CoresLabel.TabIndex = 14;
            this.CoresLabel.Text = "Max Cores:";
            this.CoresLabel.Visible = false;
            this.CoresLabel.Click += new System.EventHandler(this.CoresLabel_Click);
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
            this.ResizeDescLabel.Location = new System.Drawing.Point(8, 70);
            this.ResizeDescLabel.Name = "ResizeDescLabel";
            this.ResizeDescLabel.Size = new System.Drawing.Size(162, 75);
            this.ResizeDescLabel.TabIndex = 4;
            this.ResizeDescLabel.Text = "Resize Details: \r\n+\r\n+";
            // 
            // resizeLabel
            // 
            this.resizeLabel.AutoSize = true;
            this.resizeLabel.Location = new System.Drawing.Point(7, 34);
            this.resizeLabel.Name = "resizeLabel";
            this.resizeLabel.Size = new System.Drawing.Size(162, 25);
            this.resizeLabel.TabIndex = 2;
            this.resizeLabel.Text = "Resize Method:";
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
            "Stretch to fit."});
            this.resizeComboBox.Location = new System.Drawing.Point(176, 31);
            this.resizeComboBox.Name = "resizeComboBox";
            this.resizeComboBox.Size = new System.Drawing.Size(378, 33);
            this.resizeComboBox.TabIndex = 6;
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
            // idLbl
            // 
            this.idLbl.AutoSize = true;
            this.idLbl.Location = new System.Drawing.Point(433, 110);
            this.idLbl.Name = "idLbl";
            this.idLbl.Size = new System.Drawing.Size(32, 25);
            this.idLbl.TabIndex = 16;
            this.idLbl.Text = "ID";
            this.AllToolTip.SetToolTip(this.idLbl, "Randomly assigned ID for the picture. \r\nPictures with the same ID will overwrite " +
        "eachother.\r\nFirst character must be a letter.\r\nSecond character can be either a " +
        "letter or number.");
            // 
            // picturePathLbl
            // 
            this.picturePathLbl.AutoSize = true;
            this.picturePathLbl.Location = new System.Drawing.Point(13, 110);
            this.picturePathLbl.Name = "picturePathLbl";
            this.picturePathLbl.Size = new System.Drawing.Size(170, 25);
            this.picturePathLbl.TabIndex = 15;
            this.picturePathLbl.Text = "Picture File Path";
            this.AllToolTip.SetToolTip(this.picturePathLbl, "The images that were selected.\r\n");
            // 
            // convertPicBtn
            // 
            this.convertPicBtn.ForeColor = System.Drawing.Color.Black;
            this.convertPicBtn.Location = new System.Drawing.Point(13, 576);
            this.convertPicBtn.Name = "convertPicBtn";
            this.convertPicBtn.Size = new System.Drawing.Size(265, 57);
            this.convertPicBtn.TabIndex = 4;
            this.convertPicBtn.Text = "Convert Picures";
            this.AllToolTip.SetToolTip(this.convertPicBtn, "Start conversion.");
            this.convertPicBtn.UseVisualStyleBackColor = true;
            this.convertPicBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // DeleteAllFilesBtn
            // 
            this.DeleteAllFilesBtn.ForeColor = System.Drawing.Color.Firebrick;
            this.DeleteAllFilesBtn.Location = new System.Drawing.Point(388, 31);
            this.DeleteAllFilesBtn.Name = "DeleteAllFilesBtn";
            this.DeleteAllFilesBtn.Size = new System.Drawing.Size(170, 48);
            this.DeleteAllFilesBtn.TabIndex = 3;
            this.DeleteAllFilesBtn.Text = "Cleanup Files";
            this.AllToolTip.SetToolTip(this.DeleteAllFilesBtn, "Deletes all files associated with this converter including all .png files.");
            this.DeleteAllFilesBtn.UseVisualStyleBackColor = true;
            this.DeleteAllFilesBtn.Visible = false;
            this.DeleteAllFilesBtn.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // DeleteQueueBtn
            // 
            this.DeleteQueueBtn.ForeColor = System.Drawing.Color.Black;
            this.DeleteQueueBtn.Location = new System.Drawing.Point(201, 31);
            this.DeleteQueueBtn.Name = "DeleteQueueBtn";
            this.DeleteQueueBtn.Size = new System.Drawing.Size(170, 48);
            this.DeleteQueueBtn.TabIndex = 2;
            this.DeleteQueueBtn.Text = "Delete Queue";
            this.AllToolTip.SetToolTip(this.DeleteQueueBtn, "Remove all pictures from queue.");
            this.DeleteQueueBtn.UseVisualStyleBackColor = true;
            this.DeleteQueueBtn.Click += new System.EventHandler(this.DeleteQueueBtn_Click);
            // 
            // selectImagesDialog
            // 
            this.selectImagesDialog.FileName = "selectImagesDialog";
            this.selectImagesDialog.Multiselect = true;
            // 
            // progBar
            // 
            this.progBar.Location = new System.Drawing.Point(13, 686);
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(545, 65);
            this.progBar.TabIndex = 7;
            this.progBar.Click += new System.EventHandler(this.progBar_Click);
            // 
            // infoBox
            // 
            this.infoBox.Controls.Add(this.label3);
            this.infoBox.Controls.Add(this.logBox);
            this.infoBox.Controls.Add(this.squaresLbl);
            this.infoBox.Controls.Add(this.newDimensionsLbl);
            this.infoBox.Controls.Add(this.origDimensionsLbl);
            this.infoBox.Location = new System.Drawing.Point(594, 219);
            this.infoBox.Name = "infoBox";
            this.infoBox.Size = new System.Drawing.Size(572, 570);
            this.infoBox.TabIndex = 9;
            this.infoBox.TabStop = false;
            this.infoBox.Text = "Advanced Information";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 25);
            this.label3.TabIndex = 12;
            this.label3.Text = "Information";
            // 
            // logBox
            // 
            this.logBox.BackColor = System.Drawing.Color.Black;
            this.logBox.ForeColor = System.Drawing.Color.Ivory;
            this.logBox.HideSelection = false;
            this.logBox.Location = new System.Drawing.Point(13, 28);
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(541, 446);
            this.logBox.TabIndex = 9;
            this.logBox.Text = "Logs:";
            this.logBox.TextChanged += new System.EventHandler(this.errorsTxtBox_TextChanged);
            // 
            // squaresLbl
            // 
            this.squaresLbl.AutoSize = true;
            this.squaresLbl.Location = new System.Drawing.Point(17, 535);
            this.squaresLbl.Name = "squaresLbl";
            this.squaresLbl.Size = new System.Drawing.Size(154, 25);
            this.squaresLbl.TabIndex = 2;
            this.squaresLbl.Text = "Squares Used:";
            this.squaresLbl.Visible = false;
            // 
            // newDimensionsLbl
            // 
            this.newDimensionsLbl.AutoSize = true;
            this.newDimensionsLbl.Location = new System.Drawing.Point(17, 510);
            this.newDimensionsLbl.Name = "newDimensionsLbl";
            this.newDimensionsLbl.Size = new System.Drawing.Size(178, 25);
            this.newDimensionsLbl.TabIndex = 1;
            this.newDimensionsLbl.Text = "New Dimensions:";
            this.newDimensionsLbl.Visible = false;
            // 
            // origDimensionsLbl
            // 
            this.origDimensionsLbl.AutoSize = true;
            this.origDimensionsLbl.Location = new System.Drawing.Point(17, 485);
            this.origDimensionsLbl.Name = "origDimensionsLbl";
            this.origDimensionsLbl.Size = new System.Drawing.Size(216, 25);
            this.origDimensionsLbl.TabIndex = 0;
            this.origDimensionsLbl.Text = "Original Dimensions: ";
            this.origDimensionsLbl.Visible = false;
            // 
            // creditLabel
            // 
            this.creditLabel.AutoSize = true;
            this.creditLabel.Location = new System.Drawing.Point(12, 819);
            this.creditLabel.Name = "creditLabel";
            this.creditLabel.Size = new System.Drawing.Size(248, 25);
            this.creditLabel.TabIndex = 10;
            this.creditLabel.Text = "TheLastMillennial - 2024";
            // 
            // convertBox
            // 
            this.convertBox.Controls.Add(this.DeleteQueueBtn);
            this.convertBox.Controls.Add(this.DeleteAllFilesBtn);
            this.convertBox.Controls.Add(this.idLbl);
            this.convertBox.Controls.Add(this.picturePathLbl);
            this.convertBox.Controls.Add(this.progInfoLbl);
            this.convertBox.Controls.Add(this.pictureListTable);
            this.convertBox.Controls.Add(this.convertPicBtn);
            this.convertBox.Controls.Add(this.overlayConvert);
            this.convertBox.Controls.Add(this.importPicBtn);
            this.convertBox.Controls.Add(this.FindConvertedPicBtn);
            this.convertBox.Controls.Add(this.progBar);
            this.convertBox.Controls.Add(this.StopConversionBtn);
            this.convertBox.Location = new System.Drawing.Point(17, 21);
            this.convertBox.Name = "convertBox";
            this.convertBox.Size = new System.Drawing.Size(571, 768);
            this.convertBox.TabIndex = 11;
            this.convertBox.TabStop = false;
            this.convertBox.Text = "Convert Pictures";
            // 
            // progInfoLbl
            // 
            this.progInfoLbl.AutoSize = true;
            this.progInfoLbl.Location = new System.Drawing.Point(13, 647);
            this.progInfoLbl.Name = "progInfoLbl";
            this.progInfoLbl.Size = new System.Drawing.Size(443, 25);
            this.progInfoLbl.TabIndex = 14;
            this.progInfoLbl.Text = "Click \'Import Pictures\' then \'Convert Pictures\'.";
            // 
            // pictureListTable
            // 
            this.pictureListTable.AutoScroll = true;
            this.pictureListTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.pictureListTable.ColumnCount = 2;
            this.pictureListTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.77778F));
            this.pictureListTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.22222F));
            this.pictureListTable.Location = new System.Drawing.Point(13, 141);
            this.pictureListTable.Name = "pictureListTable";
            this.pictureListTable.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.pictureListTable.RowCount = 1;
            this.pictureListTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pictureListTable.Size = new System.Drawing.Size(552, 422);
            this.pictureListTable.TabIndex = 13;
            this.pictureListTable.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureListTable_Paint);
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
            // StopConversionBtn
            // 
            this.StopConversionBtn.ForeColor = System.Drawing.Color.Firebrick;
            this.StopConversionBtn.Location = new System.Drawing.Point(13, 576);
            this.StopConversionBtn.Name = "StopConversionBtn";
            this.StopConversionBtn.Size = new System.Drawing.Size(265, 57);
            this.StopConversionBtn.TabIndex = 17;
            this.StopConversionBtn.Text = "Stop Conversion";
            this.StopConversionBtn.UseVisualStyleBackColor = true;
            this.StopConversionBtn.Visible = false;
            this.StopConversionBtn.Click += new System.EventHandler(this.StopConversionBtn_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(372, 794);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(100, 50);
            this.pictureBox.TabIndex = 12;
            this.pictureBox.TabStop = false;
            this.pictureBox.Visible = false;
            // 
            // subPicBox
            // 
            this.subPicBox.Location = new System.Drawing.Point(266, 795);
            this.subPicBox.Name = "subPicBox";
            this.subPicBox.Size = new System.Drawing.Size(100, 50);
            this.subPicBox.TabIndex = 13;
            this.subPicBox.TabStop = false;
            this.subPicBox.Visible = false;
            // 
            // HDpicConverterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(8)))), ((int)(((byte)(8)))));
            this.ClientSize = new System.Drawing.Size(1211, 881);
            this.Controls.Add(this.subPicBox);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.convertBox);
            this.Controls.Add(this.creditLabel);
            this.Controls.Add(this.infoBox);
            this.Controls.Add(this.OptionsGroupBox);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HDpicConverterForm";
            this.Text = "HD Picture Viewer Converter 2";
            this.OptionsGroupBox.ResumeLayout(false);
            this.OptionsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxCores)).EndInit();
            this.infoBox.ResumeLayout(false);
            this.infoBox.PerformLayout();
            this.convertBox.ResumeLayout(false);
            this.convertBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subPicBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button importPicBtn;
        private System.Windows.Forms.Button FindConvertedPicBtn;
        private System.Windows.Forms.GroupBox OptionsGroupBox;
        public System.Windows.Forms.ToolTip AllToolTip;
        private System.Windows.Forms.ComboBox resizeComboBox;
        private System.Windows.Forms.Label resizeLabel;
        private System.Windows.Forms.OpenFileDialog selectImagesDialog;
        private System.Windows.Forms.ProgressBar progBar;
        private System.Windows.Forms.GroupBox infoBox;
        private System.Windows.Forms.Label squaresLbl;
        private System.Windows.Forms.Label newDimensionsLbl;
        private System.Windows.Forms.Label origDimensionsLbl;
        private System.Windows.Forms.RichTextBox logBox;
        private System.Windows.Forms.Label creditLabel;
        private System.Windows.Forms.GroupBox convertBox;
        private System.Windows.Forms.Label ResizeDescLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label overlayConvert;
        private System.Windows.Forms.NumericUpDown maxCores;
        private System.Windows.Forms.Label CoresLabel;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.PictureBox subPicBox;
        private System.Windows.Forms.Button convertPicBtn;
        private System.Windows.Forms.CheckBox advancedMode;
        private System.Windows.Forms.TableLayoutPanel pictureListTable;
        private System.Windows.Forms.Label progInfoLbl;
        private System.Windows.Forms.Label idLbl;
        private System.Windows.Forms.Label picturePathLbl;
        private System.Windows.Forms.Button StopConversionBtn;
        private System.Windows.Forms.Button DeleteAllFilesBtn;
        private System.Windows.Forms.Button DeleteQueueBtn;
    }
}

