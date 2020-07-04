using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        private void progress(int v)
        {
            progBar.Value = v;
        }
        private void progress(int v, int m, String s)
        {
            progInfoLbl.Text = s;
            progBar.Maximum = m;
            progBar.Value = v;
        }
        //User clicked 'Open Images to Convert'
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
                    //Sets progress bar
                    progress(0, 1, "Initial Image Loading");

                    imagesToConvert++;
                    img = Image.FromFile(File);
                    // Bitmap bmp = (Bitmap)Bitmap.FromFile(File); Unused.
                    filename =Path.GetFileName(File);
                    if (char.IsDigit(filename[0]))
                    {
                        errors += "ERROR: \"" + filename + "\" Was NOT converted because it does not have a valid name. Your image file name MUST start with a letter. Please rename this file and try again!\n\n";
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
                    if (width * height >= 100000000)
                    {
                        DialogResult result = MessageBox.Show("\"" + filename + "\" is insanely large and as a result may take a long time to convert or outright crash this program due to high RAM usage.\nNote: Your calculator will most liekly not be able handle such a large image if you did not select to resize it. \nDo you want to continue anyways?", "Warning: Large Image", MessageBoxButtons.YesNo);
                        if (result == DialogResult.No)
                            break;
                    }
                    

                    pictureBox.Image = img;
                    progress(1);



                    /* Do not resize image
                    Maintain aspect ratio
                    Stretch to fit */
                    progress(0, 1, "Resizing Image");
                    //maintain aspect ratio
                    if (resizeComboBox.SelectedIndex == 1)
                    {
                        //if images is already 320 wide or 240 tall, no need to resize.
                        if (width == 320 || height == 240)
                            errors += "Information: \"" + filename + "\" already has dimesnions of " + width + "x" + height + " and cannot be resized any better with Maintain aspect ratio as the setting.\n\n";
                        else
                        {
                            //gets the width correct
                            scale = (double)img.Width / 320;
                            height = (double)img.Height / scale;
                            width = (double)img.Width / scale;
                            //checks if the height will fit on screen. If not, get correct height and resize width accordingly
                            if (height > 240)
                            {
                                scale = (double)img.Height / 240;
                                height = (double)img.Height / scale;
                                width = (double)img.Width / scale;
                            }
                            //actually resize the image and picture box
                            try
                            {
                                img = ResizeImage(img, (int)Math.Ceiling(width), (int)Math.Ceiling(height));
                            }
                            catch (Exception ex)
                            {
                                
                                errors += "ERROR: \"" + filename + "\" could not be resized. Perhaps the image is too large. Error returned:\n " + ex.ToString() + "\n\n";
                                GC.Collect();
                                GC.WaitForPendingFinalizers();
                                break;
                            }
                        
                            pictureBox.Width = img.Width;
                            pictureBox.Height = img.Height;
                            pictureBox.Image = img;
                            MessageBox.Show("Height: " + height + "Width: " + width);
                        }
                        
                    }

                    //Stretch to fit
                    if (resizeComboBox.SelectedIndex == 2)
                    {
                        if (width == 320 || height == 240)
                            errors += "Information: \"" + filename + "\" already has dimesnions of " + width + "x" + height + " and cannot be resized any better with Stretch to fit as the setting.\n\n";

                        img = ResizeImage(img, 320, 240);
                        pictureBox.Width = 320;
                        pictureBox.Height = 240;
                        pictureBox.Image = img;
                    }
                    if (width * height > 3000000)
                        errors += "Warning: \"" + filename + "\" is incredibly large (" + width * height + " bytes) and will likely not fit on the calculator! Please make the file under 3,000,000 bytes or use the resizing tools provided in this application.\n\n";

                    else if (width * height > 1000000)
                        errors += "Warning: \"" + filename + "\" is very large (" + width * height + " bytes) and you may need to delete files before you send over this image!\n\n";

                    progress(1);

                    progress(0, 4, "Setting up image to slice");
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

                    Bitmap backgroundimg = new Bitmap(80 * (horizSquares), 80 * (vertSquares),PixelFormat.Format32bppArgb);
                    
                    using (Graphics gfx = Graphics.FromImage(backgroundimg))
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(0,0,0)))
                    {
                        gfx.FillRectangle(brush, 0, 0, 80 * horizSquares, 80 * vertSquares);
                    }
                    progress(1);

                    Image firstImage = img, secondImage = backgroundimg;
                    var finalImage = new Bitmap(80 * horizSquares, 80 * vertSquares);
                    if ((double)width/80 != Math.Ceiling(width/80) || (double)height / 80 != Math.Ceiling(height / 80))
                    {
                        using (Graphics graphics = Graphics.FromImage(finalImage))
                        {
                            graphics.DrawImage(firstImage, new Rectangle(new Point(), firstImage.Size),
                                new Rectangle(new Point(), firstImage.Size), GraphicsUnit.Pixel);
                            graphics.DrawImage(secondImage, new Rectangle(new Point(0, firstImage.Height + 1), secondImage.Size),
                                new Rectangle(new Point(), secondImage.Size), GraphicsUnit.Pixel);
                        }


                    }
                    else
                    {
                        finalImage = (Bitmap)img;
                    }
                    progress(2);
                    
                    //show in a winform picturebox used 
                    pictureBox.Width = 80 * horizSquares;
                    pictureBox.Height = 80 * vertSquares;
                    pictureBox.Image = finalImage;

                    //save the final composite image to disk
                    System.IO.Directory.CreateDirectory(AppDir+@"\bin\"+filename);
                    finalImage.Save(AppDir +  filename, ImageFormat.Png);

                    progress(3);

                    //Creates a rectangle that will be used to cut each individual square
                    Rectangle cropRect = new Rectangle(0,0,80,80);
                    //Bitmap src = Image.FromFile(File) as Bitmap;
                    Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);
                    String saveName;
                    progress(4);
                    progress(0, vertSquares * horizSquares,"Slicing Image:");
                    //Cuts each 80x80 square
                    int sliced = 0;
                    for (vertOffset = 0; vertOffset < vertSquares; vertOffset++)
                        for (horizOffset = 0; horizOffset < horizSquares; horizOffset++) 
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
                            //MessageBox.Show(saveName);
                            Bitmap save2 = new Bitmap(target);
                            save2.Save(saveName);
                            //dispalys progress back to user
                            progress(sliced++);
                            progInfoLbl.Text = "Slicing Image: "+ sliced.ToString() + "/" + vertSquares * horizSquares;
                        }
                    

            
                }
                
                subPicLabel.Visible = false;
                subPicBox.Visible = false;
                if (errors!=null)
                {
                    MessageBox.Show(errors,"The following messages were encountered:");
                }
                progress(1, 1, "Finished!");
                
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

        private void OpenConvertedBtn_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void subPicBox_Click(object sender, EventArgs e)
        {

        }
    }
}
