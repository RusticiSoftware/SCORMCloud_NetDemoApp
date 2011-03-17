Software License Agreement (BSD License)

Copyright (c) 2010-2011, Rustici Software, LLC
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
    * Redistributions of source code must retain the above copyright
      notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright
      notice, this list of conditions and the following disclaimer in the
      documentation and/or other materials provided with the distribution.
    * Neither the name of the <organization> nor the
      names of its contributors may be used to endorse or promote products
      derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL Rustici Software, LLC BE LIABLE FOR ANY
DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.


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
