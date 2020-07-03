using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HDPictureViewerConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            resizeComboBox.SelectedIndex = 1;
        }


        private void InitializeOpenFileDialog()
        {
            //filters out all images but png
            this.selectImagesDialog.Filter = "Images (*.PNG;*.JPG;*.JPEG;*.BMP)|*.PNG;*.JPG;*.JPEG*.BMP|" +
                                            "All files (*.*)|*.*";
            this.selectImagesDialog.Title = "Select image";

        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void OpenImgBtn_Click(object sender, EventArgs e)
        {
            uint imagesToConvert = 0, imagesConverted = 0;
            double width, height, scale;
            Image img;
            String errors=null,filename;

            //Opens the dialog for user to select images to convert
            InitializeOpenFileDialog();
            DialogResult Dlg = this.selectImagesDialog.ShowDialog();
            if (Dlg == System.Windows.Forms.DialogResult.OK)
            {
                foreach (String File in selectImagesDialog.FileNames)
                {
                    imagesToConvert++;
                    img = Image.FromFile(File);
                    // Bitmap bmp = (Bitmap)Bitmap.FromFile(File); Unused.
                    filename =Path.GetFileName(File);
                    if (char.IsDigit(filename[0]))
                    {
                        errors += "\"" + filename + "\" Is not a valid name. Your image file name MUST start with a letter. Please rename this file and try again!\n";
                    }
                    //Checks if the file is a png. If it's not, convert it to png
                    if (!(Path.GetExtension(filename).Equals(".png")))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            img.Save(ms, ImageFormat.Png);
                        }
                    }

                    //Loads Image
                    width = pictureBox.Width = img.Width;
                    height = pictureBox.Height = img.Height;
                    

                    pictureBox.Image = img;


                    /*Do not resize image
                    Maintain aspect ratio
                    Stretch to fit
                    */

                    //maintain aspect ratio
                    if (resizeComboBox.SelectedIndex == 1)
                    {
                        //gets the width correct
                        scale = (double)img.Width / 320;
                        height = (double)img.Height / scale;
                        width = (double)img.Width / scale;
                        //checks if the height will fit on screen. If not, get correct height and resize width accordingly
                        if(height > 240)
                        {
                            scale = (double)img.Height / 240;
                            height = (double)img.Height / scale;
                            width = (double)img.Width / scale;
                        }
                        //actually resize the image and picture box
                        img = ResizeImage(img, (int)Math.Ceiling(width), (int)Math.Ceiling(height));
                        pictureBox.Width = img.Width;
                        pictureBox.Height = img.Height;
                        pictureBox.Image = img;
                        MessageBox.Show("Height: " + height + "Width: " + width);
                    }
                    if (resizeComboBox.SelectedIndex == 2)
                    {
                        img = ResizeImage(img, 320, 240);
                        pictureBox.Width = img.Width;
                        pictureBox.Height = img.Height;
                        pictureBox.Image = img;
                    }

                    

                }
            }
        }
    }
}
