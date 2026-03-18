# HD Picture Viewer Converter
A converter for the HD Picture Viewer program for the TI-84 Plus CE. 
Compatible with HDPICV v3.0.0-alpha.x

## Compatible with:
* TI-84 Plus CE (-T) (Python) 
* TI-83 Premium CE (Python) 

Download the calculator viewer here: https://github.com/TheLastMillennial/HD-Picture-Viewer

## Installation 
This application does not require admin privileges. Simply ensure the included `working/convimg.exe` file is in the same folder as the converter.

## Using the converter
There are three major sections to this converter. 
* [Top Buttons]
* [Settings]
* [Information Section]


### Top Buttons
#### Add Picture
This will open a file dialog. You can select .gif .png .jpg .bmp, or .tiff files. If your picture is in a different format, there are online converters. The preferred format is .png
When a picture is selected it will appear on the [queue] below. 

#### Queue
This shows the list of images that will be converted. 
##### Picture Name
The picture name that will show on the calculator will be the file name. This cannot be edited at this time.
##### Picture ID
* Each ID is randomly generated but can be edited.
* Each ID must be unique. Pictures with the same ID will be overwritten when sent to the calculator.
* The ID must have 2 characters
    * The first character must be a letter
	* The second character can be either a letter or number.

#### Start Conversion
This will start converting all pictures in the queue to a format the calculator understands. This cannot be canceled at this time.
Unless the option 'Do not resize image' is selected, conversion should only take less than 30 seconds. GIFs will take substantially longer to convert.
Once conversion is complete, you can find the converted files by clicking [Find Converted Pictures].


#### Find Converted Pictures
This opens a file explorer window to the location the converted files are stored. There will be a folder with the name of the picture that was converted.
Inside the folder will be all of the .8xv files that can be sent to the calculator. If something is not right, refer to the [Issues] section below.


### Settings

#### Image Settings

##### Resize
There are 3 options:
1. Do Not Resize Image (default)
  - This will keep the original resolution of the image you provided. It will maintain the most amount of detail and can be zoomed in.
  - It may generate files that can't all fit on the calculator. In this case, reduce the resolution of the image and try again.
2. Maintain Aspect Ratio
  - This will reduce the resolution of the image you provided to at most 320x240 pixels (the resolution of the calculator screen). 
  - This provides the best looking image right out of the box, however it will not maintain enough detail to be zoomed in on.
  - This will always output files small enough to fit on the calculator (assuming no other files are installed on the calculator)
3. Stretch to Fit
  - This will force the resolution of the image you provided to 320x240 pixels (the resolution of the calculator screen). 
  - This will prevent any black boarders from showing, however the image will be distorted and will not maintain enough detail to be zoomed in on.
  - This will always output files small enough to fit on the calculator (assuming no other files are installed on the calculator)
  
##### Color Bit Level

Higher values allow the picture to contain more colors, however it significantly increases file size.
Intended use cases:
- 2 bit:  black and white pictures
- 4 bit:  greyscale pictures
- 8 bit:  color pictures (best balance of size and quality)
- 16 bit: color pictures that contain gradients

##### Dither Level

Higher values improve perceived color depth, however it marginally increases file size.
Intended use cases:
- 0.0: Documents or pictures that need to be as small as possible
- 0.8: Most pictures (best balance of size and quality)

#### GIF Settings

##### Color Quality:

Higher values improve color quality. However, it significantly increases file size and marginally decreases frame rate.
Intended use cases:
- 4: GIFs with very few colors
- 16-32: Most GIFs with colors
- 64: GIFs with many colors


### Log
This displays progress as the converter runs. Errors should be printed here.

## Issues
### FAQ
#### I can't find my picture to convert
* Ensure the picture is a .png file type since that is the preferred type. Also make sure you are looking in the correct drive or folder.

#### Where are converted pictures stored?
* Click [Find Converted Pictures]. The files are stored in folders named after the picture you converted.

#### Not enough space on calculator
* Delete calculator files in HD Picture Viewer by opening the picture on the calculator and pressing [del].
* Delete calculator files by pressing [2nd]>[+]>[2]>[1] and press [del] to delete files.
* Change the dimensions of the picture to something smaller and try again. Alternatively, try a different [Resize Method].

### Contact
Report issues to the Github repo: https://github.com/TheLastMillennial/HDPictureViewerConverter2/issues

## Dependancies
This program utilizes convimg, a tool created by MateoConLechuga. It is used in the C toolchain for the TI-84 Plus CE. Its license is in the `CONVIMG_LICENSE` file.
You can find the source code for convimg here: https://github.com/mateoconlechuga/convimg
All the tools for the C toolchain can be found here: https://github.com/CE-Programming/toolchain