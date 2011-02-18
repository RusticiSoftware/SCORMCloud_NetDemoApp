SCORM Cloud .Net Hosted Demo App
Rustici Software

About:
The hosted demo app is a .Net app intended to provide samples of the basic library functions to aid in the development of .Net applications that work with the SCORM Cloud Web Service.

Setup:
The HostedEngineNetClient folder should contain the SCORM Cloud .Net Library project and should be downloaded automatically from the project at https://github.com/RusticiSoftware/SCORMCloud_NetLibrary.  Verify that this project did in fact download, and if not, you will need to do so manually.  The Hosted demo app depends on the .Net Library.

The demo app will require a local database. The tables to be created are in the script within the db folder.

When the database is installed, configure the web.config file with your settings, including database connection string, smtp server settings, and your Scorm Cloud app id and secret key. 

Set up the IIS web site to point to the directory where you have unzipped the files, or alternatively, build and run within Visual Studio.

If running outside of Visual Studio, make sure to include the starting page of the application (Home.aspx) in the URL where you are running the application, (i.e. http://localhost/HostedDemoApp/Home.aspx)