namespace HDPictureViewerConverter
{
    partial class Form1
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
            this.OpenImgBtn = new System.Windows.Forms.Button();
            this.OpenConvertedBtn = new System.Windows.Forms.Button();
            this.OptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.resizeComboBox = new System.Windows.Forms.ComboBox();
            this.AllToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.selectImagesDialog = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.OptionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenImgBtn
            // 
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
            // 
            // OptionsGroupBox
            // 
            this.OptionsGroupBox.Controls.Add(this.label1);
            this.OptionsGroupBox.Controls.Add(this.resizeComboBox);
            this.OptionsGroupBox.Location = new System.Drawing.Point(12, 239);
            this.OptionsGroupBox.Name = "OptionsGroupBox";
            this.OptionsGroupBox.Size = new System.Drawing.Size(571, 325);
            this.OptionsGroupBox.TabIndex = 2;
            this.OptionsGroupBox.TabStop = false;
            this.OptionsGroupBox.Text = "Advanced Options";
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
            this.pictureBox.Location = new System.Drawing.Point(655, 25);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(329, 245);
            this.pictureBox.TabIndex = 3;
            this.pictureBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 655);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.OptionsGroupBox);
            this.Controls.Add(this.OpenConvertedBtn);
            this.Controls.Add(this.OpenImgBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.OptionsGroupBox.ResumeLayout(false);
            this.OptionsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

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
    }
}

