# HD Picture Viewer Converter 2
This is the second version of the converter for my HD Picture Viewer program for the TI-84 Plus CE. Incompatible with HDPIC v4.x.x and below.

## Compatible with:
* TI-84 Plus CE (-T) (Python) 
* TI-83 Premium CE (Python) 

Download the viewer here: https://github.com/TheLastMillennial/HD-Picture-Viewer

## Installing:
This application does not require admin privileges. Simply ensure the included `convimg.exe` is in the same folder as `HDPictureViewerConverter.exe` and the program will run fine.

## Using the converter
If anything is wrong, look at the Log inside the Advanced Information box!

### Convert Pictures Box
The `Select and Convert Pictures` button will open up a window for you to select any .png or .jpg files. If your pictures are not one of these formats, you will need to convert them. Once you select an image, click `open` to convert it.
* You will be asked to enter an Appvar name. This can be any combination of a letter, followed by a letter or number (i.e. Anything from AA to ZZ, or A0 to Z9). Make the combination unique so images don't get overwritten. Click `ok` when you`ve typed in something valid.
* The converter`s prgress bar will start getting complete. The text just above the progress bar tells you what the converter is actively doing.
* A black box will appear briefly, this is normal and is apart of the conversion process. 
* When the black box disappears, the converter will clean up any and all .c, .h, and .png files within its current folder. 
* All the converted files will be in their own folder which is named after the image. To find them, click `Find Converted Pictures`.
* Send all .8xv files to the calculator. If you get an error about not enough space, change the dimensions of the picture to something smaller and try again. Alternatively, try different Resize Options in the Options box.

The `Find Converted Pictures` button will open file explorer wherever the converted files are stored. 
* The files will be inside folders named after the pictures you converted.

### Options Box
Click on the drop down arrow next to `Resize Options`. You have three options:
1. Do Not Resize Image (default)
  - This will keep the original resolution of the image you provided. It will maintain the most amount of detail and can be zoomed in.
  - It may generate files that can't all fit on the calculator. In this case, reduce the resolution of the image and try again.
2. Maintain Aspect Ratio
  - This will reduce the resolution of the image you provided to at most 320x240 pixels (the resolution of the calculator screen). 
  - This provideds the best looking image right out of the box, however it will not maintain enough detail to be zoomed in on.
  - This will always output files small enough to fit on the calculator (assuming no other files are installed on the calculator)
3. Stretch to Fit
  - This will force the resolution of the image you provided to 320x240 pixels (the resolution of the calculator screen). 
  - This will prevent any black boarders from showing, however it will not maintain enough detail to be zoomed in on.
  - This will always output files small enough to fit on the calculator (assuming no other files are installed on the calculator)

You also have a checkbox to enable Verbose Logging which will output much more data to the Message box.

## Advanced Information Box
This tells you the resolution of the image you provided, and the resize resolution (if applicable).
It also contains the Message box which will output any important information incuding warnings and errors. You should always read any text in red or orange.

## Issues:
Report issues to the Github repo: https://github.com/TheLastMillennial/HDPictureViewerConverter2/issues

## Utilized Programs
This program utilizes convimg, a tool created by MateoConLechuga. It is used in the C toolchain for the TI-84 Plus CE. Its license is in the `CONVIMG_LICENSE` file.
You can find the source code for convimg here: https://github.com/mateoconlechuga/convimg
All the tools for the C toolchain can be found here: https://github.com/CE-Programming/toolchain