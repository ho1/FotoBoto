# FotoBoto

FotoBoto is a VB.NET app that links an input device, a web cam and a printer to provide complete photo booth functionality. The software is designed to make minimum use of the GUI and mainly relies on audio cues to indicate the state to the users.

It uses OpenCV with the Emgu CV .NET wrapper to establish communication with the web cam and acquire photos. The rest of the software is using standard Windows .NET functionality to manage things like assemblying photos and printing them.

The code has been tested in VS2015 Community and all dependencies are included in the /bin/debug/ folder for ease of use. 

FotoBoto uses Emgu CV which is released under GPL v3, more info: http://www.emgu.com/wiki/index.php/Licensing:

