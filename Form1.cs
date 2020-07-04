using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
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
            SetFullAccessPermission(AppDomain.CurrentDomain.BaseDirectory,"Brian");
            resizeComboBox.SelectedIndex = 1;
        }

        //Gives me the ability to write to sub-folders.
        public static void SetFullAccessPermission(string directoryPath, string username)
        {
            DirectorySecurity dir_security = Directory.GetAccessControl(directoryPath);

            FileSystemAccessRule full_access_rule = new FileSystemAccessRule(username,
                             FileSystemRights.FullControl, InheritanceFlags.ContainerInherit |
                             InheritanceFlags.ObjectInherit, PropagationFlags.None,
                             AccessControlType.Allow);

            dir_security.AddAccessRule(full_access_rule);

            Directory.SetAccessControl(directoryPath, dir_security);
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
                subPicLabel.Visible = true;
                subPicBox.Visible = true;
                foreach (String File in selectImagesDialog.FileNames)
                {
                    imagesToConvert++;
                    img = Image.FromFile(File);
                    // Bitmap bmp = (Bitmap)Bitmap.FromFile(File); Unused.
                    filename =Path.GetFileName(File);
                    if (char.IsDigit(filename[0]))
                    {
                        errors += "\"" + filename + "\" Was NOT converted because it does not have a valid name. Your image file name MUST start with a letter. Please rename this file and try again!\n\n";
                        continue;
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

                    /* Do not resize image
                    Maintain aspect ratio
                    Stretch to fit */

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

                    //Stretch to fit
                    if (resizeComboBox.SelectedIndex == 2)
                    {
                        img = ResizeImage(img, 320, 240);
                        pictureBox.Width = img.Width;
                        pictureBox.Height = img.Height;
                        pictureBox.Image = img;
                    }
                    if (width * height > 3000000)
                        errors += "\"" + filename + "\" was converted however, it is incredibly large (" + width * height + " bytes) and will likely not fit on the calculator! Please make the file under 3,000,000 bytes or use the resizing tools provided in this application.";

                    else if (width * height > 1000000)
                        errors += "\"" + filename + "\" was converted however, it is very large (" + width * height + " bytes) and you may need to delete files before you send over this image!";




                    //Slicing image
                    //gets current dir of this program
                    String AppDir = AppDomain.CurrentDomain.BaseDirectory;
                    //finds how many 80x80 squares are needed to fit this image
                    int horizSquares = (int)Math.Ceiling(width / 80), horizOffset=0;
                    int vertSquares = (int)Math.Ceiling(height / 80), vertOffset=0;

                    /*
                     * This creates new background image the width and height of the rounded values above.
                     * The actual image will be overlayed on top of it.
                     * This ensures that the image will be wide and tall enought to fit in all those squares.
                     * It is black so it goes unnoticed in the calc program.
                     */

                    /*Bitmap backgroundimg = new Bitmap(80 * (horizSquares), 80 * (vertSquares),PixelFormat.Format32bppArgb);
                    
                    using (Graphics gfx = Graphics.FromImage(backgroundimg))
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(0,0,0)))
                    {
                        gfx.FillRectangle(brush, 0, 0, 80 * horizSquares, 80 * vertSquares);
                    }
                    subPicBox.Image = backgroundimg;*/

                    Image backgroundimg= Image.FromFile("blackSquare.bmp");
                    backgroundimg= ResizeImage(backgroundimg, horizSquares * 80, vertSquares * 80);


                    //Overlays the images
                    Image finalImage = new Bitmap(backgroundimg.Width, backgroundimg.Height);
                    using (Graphics gr = Graphics.FromImage(finalImage))
                    {
                        gr.DrawImage(backgroundimg, new Point(0, 0));
                        gr.DrawImage(img, new Point(0, 0));
                    }
                    finalImage.Save("output.png", ImageFormat.Png);
                    pictureBox.Image = finalImage;
                    finalImage = img;

                    /*Bitmap baseImage = backgroundimg, overlayImage = (Bitmap)img;
                    var finalImage = new Bitmap(80 * horizSquares, 80 * vertSquares);

                    var graphics = Graphics.FromImage(finalImage);
                    graphics.CompositingMode = CompositingMode.SourceOver;

                    graphics.DrawImage(baseImage, 0, 0);
                    graphics.DrawImage(overlayImage, 0, 0);
                    pictureBox.Image = finalImage;*/


                    /*****************************************************************************************************************/
                    //                                       BUG: Image gets resized too small
                    /*****************************************************************************************************************/
                    //show in a winform picturebox
                    pictureBox.Width = 80 * horizSquares;
                    pictureBox.Height = 80 * vertSquares;
                    pictureBox.Image = finalImage;

                    //save the final composite image to disk
                    System.IO.Directory.CreateDirectory(AppDir+@"\bin\"+filename);
                    finalImage.Save(AppDir +  filename, ImageFormat.Png);



                    //Creates a rectangle that will be used to cut each individual square
                    Rectangle cropRect = new Rectangle(0,0,80,80);
                    //Bitmap src = Image.FromFile(File) as Bitmap;
                    Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);
                    String saveName;


                    //Cuts each 80x80 square
                    for (vertOffset = 0; vertOffset <= vertSquares; vertOffset++)
                        for (horizOffset = 0; horizOffset <= horizSquares; horizOffset++) 
                        {
                            saveName = AppDir + @"bin\" + filename + @"\";

                            cropRect.X = horizOffset*80;
                            cropRect.Y = vertOffset*80;
                            target = CropImage(finalImage, cropRect, null);
                            subPicBox.Image = target;
                            //accounts for leading 0s
                            if (horizOffset < 10)
                                saveName += "00";
                            else if (horizOffset < 100)
                                saveName += "0";
                            saveName += horizOffset.ToString();

                            if (vertOffset < 10)
                                saveName += "00";
                            else if (vertOffset < 100)
                                saveName += "0";
                            saveName += vertOffset.ToString();
                            saveName += filename+".png";
                            MessageBox.Show(saveName);
                            Bitmap save2 = new Bitmap(target);
                            save2.Save(saveName);
                        }
                    


                }
                subPicLabel.Visible = false;
                subPicBox.Visible = false;
                if (errors!=null)
                {
                    MessageBox.Show("Some pictures may not have been converted due to the following errors:\n"+errors);
                }
                
            }
        }
        static Bitmap CropImage(Image originalImage, Rectangle sourceRectangle, Rectangle? destinationRectangle = null)
        {
            if (destinationRectangle == null)
            {
                destinationRectangle = new Rectangle(Point.Empty, sourceRectangle.Size);
            }

            var croppedImage = new Bitmap(destinationRectangle.Value.Width,
                destinationRectangle.Value.Height);
            using (var graphics = Graphics.FromImage(croppedImage))
            {
                graphics.DrawImage(originalImage, destinationRectangle.Value,
                    sourceRectangle, GraphicsUnit.Pixel);
            }
            return croppedImage;
        }
    }
}
