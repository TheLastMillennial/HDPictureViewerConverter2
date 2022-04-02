using Microsoft.VisualBasic;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HDPictureViewerConverter
{
    public partial class HDpicConverterForm : Form
    {
        public HDpicConverterForm()
        {
            InitializeComponent();
            SetFullAccessPermission(AppDomain.CurrentDomain.BaseDirectory, System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            resizeComboBox.SelectedIndex = 0;
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
        public static Boolean isAlphaNumeric(string strToCheck)
        {
            if (!Regex.IsMatch(strToCheck.ToString(), "[A-Z]", RegexOptions.IgnoreCase))
                return false;
            if (strToCheck[1] < 128)
                return true;
            return false;
            
        }

        //User clicked 'Open Images to Convert'
        private void OpenImgBtn_Click(object sender, EventArgs e)
        {
            //Opens the dialog for user to select images to convert
            if (!convimgReady())
            {
                MessageBox.Show("A critical file is missing! Check the red text box for more information!", "ERROR");
                return;
            }
               
            InitializeOpenFileDialog();
            DialogResult Dlg = this.selectImagesDialog.ShowDialog();
            if (Dlg == System.Windows.Forms.DialogResult.OK)
                convertImg(selectImagesDialog.FileNames);
        }

        
        private void OpenImgBtn_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                OpenImgBtn.Text = "Drop file here!";
            }
            else
            {
                e.Effect = DragDropEffects.None;
                OpenImgBtn.Text = "Select and Convert Images.";
            }
        }
        private void OpenImgBtn_DragDrop(object sender, DragEventArgs e)
        {
            convertImg((string[])e.Data.GetData(DataFormats.FileDrop, false));
        }

        //checks if convimg.exe and convimg.yaml exists
        private bool convimgReady()
        {
            String AppDir = AppDomain.CurrentDomain.BaseDirectory,errors="";
            bool ready = true ;

            if (!System.IO.File.Exists(AppDir + @"\convimg.exe"))
            {

                errors += "ERROR: All images not converted! Make sure you have convimg.exe at the following directory: \n" + AppDir + "\n\n";
                errorsTxtBox.AppendText(errors,Color.Red);
                
                ready = false ;
            }
            if (!System.IO.File.Exists(AppDir + @"\convimg.yaml"))
            {
                try
                {

                    using (File.Create(AppDir + @"\convimg.yaml")) { }
                    if(verboseLogging.Checked)
                        errorsTxtBox.AppendText("Information: convimg.yaml was not found but has been successfully created at the following directory: Due to a bug,this is considered a FAIL. \n" + AppDir + "\n\n", Color.Red);
                    ready = true;
                }
                catch (Exception ex)
                {
                    errors += "ERROR: convimg.yaml was not found and could not be automatically created at the following directory: \n" + AppDir + "\n\n";
                    errorsTxtBox.AppendText(errors, Color.Red);
                    ready = false;
                }
            }
            return ready;
        }

        private void convertImg(String[] f)
        {

            bool validFilename = true;
            uint imagesToConvert = 0, imagesConverted = 0;
            double width, height, scale;
            Image img;
            String errors = null, filename, filenamewe, filenamee;
            //gets current dir of this program
            String AppDir = AppDomain.CurrentDomain.BaseDirectory;

            subPicLabel.Visible = true;
            subPicBox.Visible = true;

            
            //sets errors box colors to nominal
            errorsTxtBox.ForeColor = SystemColors.ControlText;

            foreach (String file in f)
            {
                //Sets progress bar
                progress(0, 1, "Initial Image Loading");

                imagesToConvert++;
                img = Image.FromFile(file);
                filename = Path.GetFileName(file);
                filenamewe = Path.GetFileNameWithoutExtension(file);
                filenamee = Path.GetExtension(file);

                if (file.Contains(' ') || char.IsDigit(filename[0]))
                {
                    validFilename = false;
                    try
                    {
                        //copy filename to then make it valid
                        string renamedFile = "";
                        //get only alphanumeric chars from string
                        foreach(char c in filenamewe)
                        {
                            if (char.IsLetterOrDigit(c))
                            {
                                renamedFile += c;
                            }
                        }


                        //first character must be a letter.
                        if (char.IsDigit(renamedFile[0]))
                        {
                   
                            //Z will ensure image shows up at the bottom of calculator's memory management screen and out of the way of more important appvars
                            renamedFile = "Z" + renamedFile;
                        }
                            
                        //makes new image with the new name with the extension tacked on.
                        File.Copy(file, renamedFile+filenamee);

                        //overwrite outdated variables
                        //img = Image.FromFile(renamedFile+filenamee);                       
                        filename = renamedFile + filenamee; 
                        filenamewe =renamedFile;
                        
                    }
                    catch(Exception e)
                    {
                        errors += "ERROR: \"" + filename + "\" Was NOT converted because it does not have a valid name. The converter attempted to create a renamed copy but failed. Your image file name must NOT contain whitespace (Use underscores instead). Your image file name should also start with a letter. Please rename this file and try again!\n\n";
                        errorsTxtBox.AppendText(errors, Color.Red);
                        //skips rest of conversion for this loop
                        continue;
                    }
                }


                errorsTxtBox.AppendText("\nInformation: Starting Conversion of " + filename + "\n", Color.Gray);

                //Checks if the file is a png. If it's not, convert it to png
                if (!filenamee.Equals(".png"))
                {
                    //validFilename = false;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms, ImageFormat.Png);
                        ms.Flush();

                    }
                }

                if (verboseLogging.Checked && !validFilename)
                    errorsTxtBox.AppendText("\nInformation: The file name was not valid or the file was not already a PNG. A corrected copy of the image will be made. The copy will be deleted when the program finishes (the original image will not be modified or deleted in any way)", Color.Gray);

                //if the file name is valid and it was already a PNG, then there no local copy of the image was made. 
                // since there was no local copy made, just directly access the image from the file location the user gave us.
                // note: file is the file path the user provided. filename is the local copy.
                using (img = (validFilename) ? Image.FromFile(file) : Image.FromFile(filename))
                {

                    //Loads Image
                    width = pictureBox.Width = img.Width;
                    height = pictureBox.Height = img.Height;
                    origDimensionsLbl.Text = "Original Dimensions: " + width + "x" + height;
                    if (width * height >= 50000000)//50,000,000
                    {
                        DialogResult result = MessageBox.Show("\"" + filename + "\" is insanely large and as a result may take a long time to convert or outright crash this program due to high RAM usage.\nNote: Your calculator will most liekly not be able handle such a large image if you did not select to resize it. \nDo you want to continue anyways?", "Warning: Large Image", MessageBoxButtons.YesNo);
                        if (result == DialogResult.No)
                            break;
                        errorsTxtBox.AppendText("Information: Insanely large image conversion manually activated by user.\n", Color.Orange);

                    }

                    pictureBox.Image = img;

                    progress(1);

                    /* Do not resize image
                    Maintain aspect ratio
                    Stretch to fit */
                    progress(0, 1, "Resizing Image");
                    errorsTxtBox.AppendText("Resizing image to desired setting", Color.Gray);
                    //maintain aspect ratio
                    if (resizeComboBox.SelectedIndex == 1)
                    {
                        //if images is already 320 wide or 240 tall, no need to resize.
                        if (width == 320 || height == 240)
                            errorsTxtBox.AppendText("Information: \"" + filename + "\" already has dimesnions of " + width + "x" + height + " and cannot be resized any better with Maintain aspect ratio as the setting.\n", Color.Gray);
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
                                errorsTxtBox.AppendText(errors, Color.Red);

                                GC.Collect();
                                GC.WaitForPendingFinalizers();
                                break;
                            }
                            pictureBox.Width = img.Width;
                            pictureBox.Height = img.Height;
                            pictureBox.Image = img;

                            //MessageBox.Show("Height: " + height + "Width: " + width);
                        }
                        errorsTxtBox.AppendText("Successfully resized to desired setting!", Color.Gray);
                    }
                    else if (resizeComboBox.SelectedIndex == 2)
                    {
                        //Stretch to fit
                        if (width == 320 || height == 240)
                            errorsTxtBox.AppendText("Information: \"" + filename + "\" already has dimensions of " + width + "x" + height + " and cannot be resized any better with Stretch to fit as the setting.\n");
                        else
                            img = ResizeImage(img, 320, 240);
                        width = 320;
                        height = 240;
                        pictureBox.Width = img.Width;
                        pictureBox.Height = img.Height;
                        pictureBox.Image = (Image)img;
                        errorsTxtBox.AppendText("Information: Successfully resized to desired setting!", Color.Gray);
                    }
                    else
                        errorsTxtBox.AppendText("Information: Did not resize image", Color.Gray);


                    //checks if image will fit on calculator
                    if (width * height > 3000000)
                        errors += "Warning: \"" + filename + "\" is incredibly large (" + width * height + " bytes) and will likely not fit on the calculator! Please make the file under 3,000,000 bytes or use the resizing tools provided in this application.\n\n";
                    else if (width * height > 1000000)
                        errors += "Warning: \"" + filename + "\" is very large (" + width * height + " bytes) and you may need to delete files before you send over this image!\n";

                    newDimensionsLbl.Text = "New Dimensions: " + Math.Round(width).ToString() + "x" + Math.Round(height).ToString();
                    progress(1);

                    progress(0, 4, "Setting up image to slice");
                    //Slicing image
                    //finds how many 80x80 squares are needed to fit this image
                    int horizSquares = (int)Math.Ceiling(width / 80), horizOffset = 0;
                    int vertSquares = (int)Math.Ceiling(height / 80), vertOffset = 0;
                    squaresLbl.Text = "Squares Used: " + horizSquares.ToString() + "x" + vertSquares.ToString();

                    /*
                     * This creates new background image the width and height of the rounded values above.
                     * The actual image will be overlayed on top of it.
                     * This ensures that the image will be wide and tall enought to exactly fit in all those squares.
                     * It is black so it goes unnoticed in the calc program.
                     */
                    Bitmap backgroundimg = new Bitmap(80 * (horizSquares), 80 * (vertSquares), PixelFormat.Format32bppArgb);

                    using (Graphics gfx = Graphics.FromImage(backgroundimg))
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 0, 0)))
                    {
                        gfx.FillRectangle(brush, 0, 0, 80 * horizSquares, 80 * vertSquares);
                    }
                    progress(1);

                    Image firstImage = img, secondImage = backgroundimg;
                    var finalImage = new Bitmap(80 * horizSquares, 80 * vertSquares);
                    if ((double)width / 80 != Math.Ceiling(width / 80) || (double)height / 80 != Math.Ceiling(height / 80))
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

                    //checks if convPNG exists, if so, try to copy it. If not, abort.
                    if (convimgReady())
                    {
                        try
                        {
                            if (File.Exists(AppDir + filename + ".png"))
                                File.Delete(AppDir + filename + ".png");

                            //for some reason you need a memory stream to avoid "A generic error occurred in GDI+" exception
                            using (MemoryStream ms = new MemoryStream())
                            {
                                finalImage.Save(ms, ImageFormat.Png);
                                Image tstImg = Image.FromStream(ms);
                                tstImg.Save(AppDir + filename + ".png", ImageFormat.Png);
                            }


                        }
                        catch (Exception ex)
                        {
                            errorsTxtBox.AppendText("An error occured while saving files: " + ex.ToString(), Color.Red);
                            MessageBox.Show("An error occured while accessing files. Check red text for more information.", "ERROR");
                            return;
                        }


                    }
                    progress(3);

                    //Creates a rectangle that will be used to cut each individual square
                    Rectangle cropRect = new Rectangle(0, 0, 80, 80);
                    //Bitmap src = Image.FromFile(File) as Bitmap;
                    Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);
                    string saveName = "", num = "", lettersID = "  ";

                    

                    lettersID = Interaction.InputBox("Enter an alphabetic character followed by any ASCII character (A-Z and 0-9).\n" +
                        "To avoid issues, run the HDPICV program on your calculator, press [mode] and look at the bottom where it says\"Safe Appvar Name\"",
                        "Enter Appvar Name", "AA");
                    if (lettersID.Length == 0)
                        return;

                    while (lettersID.Length != 2 || !isAlphaNumeric(lettersID))
                    {
                        lettersID = Interaction.InputBox("Error: You did not enter alphanumeric characters!\nEnter two capital alphanumeric characters (A-Z and 0-9).\n" +
                        "To avoid issues, run the HDPICV program on your calculator, press [mode] and look at the bottom where it says\"Safe Appvar Name\"\n" +
                        "Do you think you shouldn't be getting this error message and you can't get rid of it? Type \"terminate\" to immediately kill this program. Then contact the developer on Github",
                        "Enter Appvar Name", "AA");
                        if (lettersID.Length == 0)
                            return;
                        if (lettersID.Equals("terminate"))
                        {
                            Application.Exit();
                            return;
                        }
                    }

                    //filename 8 is the 8 character version of filename. Used for header of appvar where character count consistency is necessary
                    string filename8 = filenamewe;
                    if (filename8.Length > 8)
                        filename8 = filename8.Substring(0, 8);
                    while (filename8.Length < 8)
                    {
                        filename8 += "_";
                    }
                    /*paletteName8 is used to identify what appvar is the palette for an image with ID of lettersID. 
                     * HP is just to show it's 
                     * The 0s at the end are just for potential future features*/
                    string paletteName8 = "HP" + lettersID + "0000";
                    if (filename8.Length > 8)
                        filename8 = filename8.Substring(0, 8);
                    while (filename8.Length < 8)
                    {
                        filename8 += "_";
                    }


                    //Converts using convIMG Starts yaml file
                    List<string> yamlLinesList = new List<string>();
                    List<string> yamlPalettes = new List<string>();
                    List<string> yamlOutputsPal = new List<string>();
                    List<string> yamlOutputsImg = new List<string>();
                    List<string> yamlConverts = new List<string>();
                    List<string> yamlLinesAppvarName = new List<string>();

                    //starts setting up for the convimg yaml file.
                    yamlPalettes.Add("\npalettes:" +
                        "\n  - name: my_palette" +
                        "\n    fixed-entries:" +
                        "\n      - color: { index: 0,   r: 0,   g: 0,   b: 0}" +
                        "\n      - color: { index: 255, r: 255, g: 255, b: 255}" +
                        "\n    images: automatic");

                    yamlConverts.Add("\n\nconverts:");

                    yamlOutputsImg.Add("\n\noutputs:");


                    progress(4);
                    progress(0, vertSquares * horizSquares, "Slicing Image:");
                    if (verboseLogging.Checked)
                        errorsTxtBox.AppendText("Information: Starting slicing of image.\n", Color.Gray);
                    //Cuts each 80x80 square
                    int sliced = 0;

                    for (vertOffset = 0; vertOffset < vertSquares; vertOffset++)
                        for (horizOffset = 0; horizOffset < horizSquares; horizOffset++)
                        {
                            saveName = "";//@"bin\" + filename + @"\"
                            num = "";
                            cropRect.X = horizOffset * 80;
                            cropRect.Y = vertOffset * 80;
                            target = CropImage(finalImage, cropRect, null);
                            subPicBox.Image = target;
                            //accounts for leading 0s
                            if (horizOffset < 10)
                            {
                                num += "00";
                                saveName += "00";
                            }
                            else if (horizOffset < 100)
                            {
                                num += "0";
                                saveName += "0";

                            }
                            saveName += horizOffset.ToString();
                            num += horizOffset.ToString();

                            if (vertOffset < 10)
                            {
                                num += "00";
                                saveName += "00";
                            }
                            else if (vertOffset < 100)
                            {
                                num += "0";
                                saveName += "0";
                            }
                            saveName += vertOffset.ToString();
                            num += vertOffset.ToString();
                            saveName += filenamewe + ".png";
                            //MessageBox.Show(saveName);
                            Bitmap save2 = new Bitmap(target);
                            save2.Save(AppDir + saveName);


                            yamlConverts.Add("\n  - name: " + lettersID + num +
                                "\n    palette: my_palette" +
                                "\n    images:" +
                                "\n      - " + saveName);//.Substring(0, saveName.Length - 4)) ;

                            yamlOutputsImg.Add("\n  - type: appvar \n" +
                                "    name: " + lettersID + num + "\n" +
                                "    source-format: ice\n" + //only ice because Mateo said it can be this and C doesn't work.
                                "    header-string: " + " HDPICCV4" + filename8 + "\n" +
                                "    archived: true \n" +
                                "    converts: \n" +
                                "      - " + lettersID + num);
                            
                            //dispalys progress back to user
                            progress(sliced++);
                            progInfoLbl.Text = "Slicing Image: " + sliced.ToString() + "/" + vertSquares * horizSquares;
                        }
                    errorsTxtBox.AppendText("\nInformation: Slice Successful!\n", Color.Gray);

                    //This saves the palette for the image
                    yamlOutputsPal.Add("\n  - type: appvar" +
                        "\n    name: HP" + lettersID + "0000" + //0000 used as placeholders, they don't signify anything
                        "\n    source-format: ice" +
                        "\n    header-string: HDPALV10" + filename8 + lettersID + num +
                        "\n    archived: true" +
                        "\n    palettes:" +
                        "\n      - my_palette");

                    yamlLinesList = yamlPalettes.Concat(yamlConverts).Concat(yamlOutputsImg).Concat(yamlOutputsPal).ToList();

                    //saves the ini text and runs convimg
                    try
                    {
                        if (verboseLogging.Checked)
                            errorsTxtBox.AppendText("Information: writing to convimg.yaml.\n");
                        // write a line of text to the file
                        using (StreamWriter tw = new StreamWriter(AppDir + @"convimg.yaml")) //@"\bin\" + filename +
                        {
                            foreach (String s in yamlLinesList)
                                tw.WriteLine(s);
                        }
                        if (verboseLogging.Checked)
                            errorsTxtBox.AppendText("Information: write successful! Starting convimg.exe\n", Color.Green);

                        //starts the converter application and allows it 30 seconds to convert before erroring out
                        var convimgRunning = Process.Start(AppDir + @"\convimg.exe"); 
                        //give convimg more time to run if converting large image.
                        if (width * height <= 3000000)
                            convimgRunning.WaitForExit(45000);
                        else
                            convimgRunning.WaitForExit();
                        if (verboseLogging.Checked)
                            errorsTxtBox.AppendText("Information: convimg returned successfully!\n",Color.Gray);
                    }
                    catch (Exception ex)
                    {
                        errors += "ERROR: All images not converted! Make sure you have convimg.exe at the following directory: \n" + AppDir + "\n\n";
                        errorsTxtBox.AppendText(errors, Color.Red);
                        return;
                    }

                }


                progress(0, 5, "Cleaning up");
                string[] findFiles = Directory.GetFiles(AppDir, "*.c", 0);
                
                //deletes unnecessary files
                try
                {
                    //deletes unnecessary files
                    foreach (string s in findFiles)
                    {
                        System.IO.File.Delete(s);
                        if (verboseLogging.Checked) 
                            errorsTxtBox.AppendText("Information: \"" + s + "\" was deleted\n", Color.Gray);
                    }
                    progress(1);
                    findFiles = Directory.GetFiles(AppDir, "*.h", 0);
                    foreach (string s in findFiles)
                    {
                        System.IO.File.Delete(s);
                        if (verboseLogging.Checked)
                            errorsTxtBox.AppendText("Information: \"" + s + "\" was deleted\n", Color.Gray);
                    }
                    progress(2);
                    findFiles = Directory.GetFiles(AppDir, "*.png", 0);
                    foreach (string s in findFiles)
                    {
                        System.IO.File.Delete(s);
                        if (verboseLogging.Checked)
                            errorsTxtBox.AppendText("Information: \"" + s + "\" was deleted\n", Color.Gray);
                    }
                    progress(3);
                    findFiles = Directory.GetFiles(AppDir, "*"+filenamee, 0);
                    foreach (string s in findFiles)
                    {
                        System.IO.File.Delete(s);
                        if (verboseLogging.Checked)
                            errorsTxtBox.AppendText("Information: \"" + s + "\" was deleted\n", Color.Gray);
                    }
                    progress(4);
                }catch (Exception ex)
                {
                    errorsTxtBox.AppendText("ERROR: Although images were converted, an error occured while deleting unnecessary files: \n" + ex.ToString() + "\n", Color.Red);
                    //MessageBox.Show("An error occured while deleting unnecesary files. Check red text for more information.", "ERROR");
                    
                }


                //checks if directory already has appvars in it. If so, delete them.
                try
                {
                    if (!Directory.Exists(AppDir + filenamewe))
                        Directory.CreateDirectory(AppDir + filenamewe);
                    else
                    {
                        Directory.Delete(AppDir + filenamewe,true);
                        //waits for directory to be deleted before creating it again.
                        while (Directory.Exists(AppDir + filenamewe))
                        {
                            System.Threading.Thread.Sleep(200);
                        }
                        Directory.CreateDirectory(AppDir + filenamewe);
                    }
                        
                }
                catch (Exception ex)
                {
                    errorsTxtBox.AppendText("ERROR: Although images were converted, an error occured while deleting/ creating a directory: \n" + ex.ToString() +"\n", Color.Red);
                    MessageBox.Show("An error occured while deleting or creating a directory. Check red text for more information.", "ERROR");
                    break;
                }

                //moves appvars to correct lovation
                int location;
                string newName,test;
                findFiles = Directory.GetFiles(AppDir, "*.8xv", 0);
                foreach (string s in findFiles)
                {
                    //gets file name
                    location = 0;
                    for(int i = 0; i < s.Length; i++)
                        if (s[i].Equals('\\'))
                            location = i;
                    newName = s.Substring(location + 1);

                    try
                    {
                        test = AppDir + filenamewe + @"\" + newName;
                        System.IO.File.Move(s, test);
                        if(verboseLogging.Checked)
                            errorsTxtBox.AppendText("Information: \"" + s + "\" was moved\n", Color.Gray);
                        progress(5);
                    }catch (Exception ex)
                    {
                        errorsTxtBox.AppendText("ERROR: Although images were converted, an error occured while moving files: \n" + ex.ToString() + "\n", Color.Red);
                        MessageBox.Show("An error occured while moving files. Check red text for more information.", "ERROR");
                        break;
                    }

                }


            }
            subPicLabel.Visible = false;
            subPicBox.Visible = false;
            if (errors != null)
            {
                MessageBox.Show(errors, "Information:");
            }
            errorsTxtBox.AppendText("Finished!\n\n", Color.Green);
            progress(1, 1, "Finished!");
        }

        //crops image to desired size
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

        private void verboseLogging_CheckedChanged(object sender, EventArgs e)
        {
            //this stuff is just distracting unless you need to view it.
            MainPicLabel.Visible = verboseLogging.Checked;
            subPicLabel.Visible = verboseLogging.Checked;
            pictureBox.Visible = verboseLogging.Checked;
            subPicBox.Visible = verboseLogging.Checked;

            squaresLbl.Visible = verboseLogging.Checked;
        }

        private void resizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //change the description based on what the user selected.
            switch (resizeComboBox.SelectedIndex)
            {
                case 0:
                    ResizeDescLabel.Text = "Best quality.\nOriginal Resolution.\nLargest file size.";
                    break;
                case 1:
                    ResizeDescLabel.Text= "Lower quality.\nReduces picture resolution to at most 320x240.\nSmallest file size.";
                    break;
                case 2:
                    ResizeDescLabel.Text = "Lower quality.\nForce picture resolution to be exactly 320x240.\nSmall file size.";
                    break;
                default:
                    ResizeDescLabel.Text = "Description:";
                    break;
            }
         
        }
    }
}

public static class RichTextBoxExtensions
{
    public static void AppendText(this RichTextBox box, string text, Color color)
    {
        box.SelectionStart = box.TextLength;
        box.SelectionLength = 0;

        box.SelectionColor = color;
        box.AppendText(text);
        box.SelectionColor = box.ForeColor;
    }
}