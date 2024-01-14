# HD Picture Viewer Converter 2
A converter for the HD Picture Viewer program for the TI-84 Plus CE. 
Compatible with HDPICV v2.0.x

## Compatible with:
* TI-84 Plus CE (-T) (Python) 
* TI-83 Premium CE (Python) 

Download the calculator viewer here: https://github.com/TheLastMillennial/HD-Picture-Viewer

## Installation 
This application does not require admin privileges. Simply ensure the included `convimg.exe` is in the same folder as `HDPictureViewerConverter.exe`.

## Using the converter
There are three major sections to this converter. 
* [Convert Pictures Section]
* [Options Section]
* [Information Section]


### Convert Pictures Section
#### Import Pictures
This will open a file dialog. You can select .png .jpg or .bmp files. If your picture is in a different format, there are online converters. The preferred format is .png
When a picture is selected it will appear on the [queue] below. 

#### Delete Queue
This will remove all images from the [queue]. 

#### Queue
This shows the list of images that will be converted. Due to technical limitations, you cannot remove a single image from the queue, you must click [Delete Queue] to remove all images.
There are two columns: Picture File Path and ID
##### Picture File Path
* The file path to the picture to be converted.
* This cannot be edited.
##### ID
* Each ID is randomly generated but can be edited.
* Each ID must be unique. Pictures with the same ID will be overwritten when sent to the calculator.
* The ID must have 2 characters
    * The first character must be a letter
	* The second character can be either a letter or number.

#### Convert Pictures
This will start converting all pictures in the queue to a format the calculator understands. It will be replaced by a [Stop Conversion] button.
One or more convimg command prompt windows will launch.
Unless the option 'Do not resize image' is selected, conversion should only take less than 30 seconds. Files should be between 2-5KB.
Once conversion is complete, you can find the converted files by clicking [Find Converted Pictures].

#### Stop Conversion
Clicking this sends a notice to the converter to stop. The converter may not stop until it has completed the current conversion phase.
convimg will not stop and must be closed manually. It may take a few seconds before convimg shuts down.
This will turn back into the [Convert Pictures] button.

#### Find Converted Pictures
This opens a file explorer window to the location the converted files are stored. There will be a folder with the name of the picture that was converted.
Inside the folder will be all of the .8xv files that can be sent to the calculator. If something is not right, refer to the [Issues] section below.


### Options Section
#### Resize Method
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
  - This will prevent any black boarders from showing, however the image will be distorted and will not maintain enough detail to be zoomed in on.
  - This will always output files small enough to fit on the calculator (assuming no other files are installed on the calculator)

#### Advanced Mode
This checkbox enables verbose logging as well as some hidden features.

##### Cleanup Files
This will delete all .png .8xv .yaml and .lst files from the directory that HD Picture Converter is located in.
It is useful for cleaning up leftover files if a conversion failed. Click Yes to delete files. Click No to cancel.

##### Max Cores
This is the number of convimg instances that will be launched when converting images.
The default number is 1 for the options 'Maintain aspect ratio' and 'Stretch to fit'.
When 'Do not resize image' is selected, the number of CPU cores on your machine will be detected and displayed here.
More cores results in faster conversion since the work is more split up. However, this is only necessary for large images.

##### Misc. Info
Below the logs there wil be some information about the original image, the image after resizing it, and how many 80*80 pixel squares will be used to represent the picture..
Note: If you see: "Squares Used: 4x3" Multiply 4 and 3 to get 12. Then add 1. This is the number of .8xv files that will be generated (13).


### Information Section
This contains the Logs box which will output any important information incuding warnings and errors. You should always read any text in red.
To enable verbose logging, enable [Advanced Mode].

## Issues
### FAQ
#### I can't find my picture to convert
* Ensure the picture is a .png file type since that is the preferred type. Also make sure you are looking in the correct drive or folder.

#### Where are converted pictures stored?
* Click [Find Converted Pictures]. The files are stored in folders named after the picture you converted.

#### Not enough space on calculator
* Delete calculator files by pressing [2nd]>[+]>[2]>[1] and press [del] to delete files.
* Change the dimensions of the picture to something smaller and try again. Alternatively, try a different [Resize Method].

### Contact
Report issues to the Github repo: https://github.com/TheLastMillennial/HDPictureViewerConverter2/issues

## Dependancies
This program utilizes convimg, a tool created by MateoConLechuga. It is used in the C toolchain for the TI-84 Plus CE. Its license is in the `CONVIMG_LICENSE` file.
You can find the source code for convimg here: https://github.com/mateoconlechuga/convimg
All the tools for the C toolchain can be found here: https://github.com/CE-Programming/toolchain