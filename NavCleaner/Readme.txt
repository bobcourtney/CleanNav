
Abstract

NavCleaner is a Microsoft Windows 7 program that was developed 
to efficiently examine and correct navigational data collected 
during marine expeditions of the Geological Survey of Canada. 

Two types of data streams are considered here: (1) navigational 
position data derived from GPS receivers logged on research vessels 
and (2) navigational data embedded in SEGY (Norris and Faichney, 2002) 
data collected during high resolution seismic surveys conducted 
during marine expeditions.

Since many high-resolution seismic systems emit an impulsive sound
at a repetition rate based on two-way travel time (the time for a 
sound wave to travel from the sounder to the seabed and back) , 
the records derived from these sounders are often biased to 
oversample in shallow waters when compared to number of soundings 
made in deeper water.

This program offers a method to resample the seismic section 
with a prescribed constant shot distance spacing. This approach 
uses the Douglas-Peucker algorithm ( DOUGLAS and PEUCKER, 1973) 
to simplify the survey vessel’s track, reducing the effects of 
GPS and other sources of positional error that contributes to 
errors in the estimate of cumulative distance along track.

The program was written in C# using Microsoft Visual Studio 2013. 
All source code is freely available for any use. A compiled 
deployment version is included in this release which can be 
used and redistributed without restriction.



Release Details

This release package contains 2 separate deployment packages:

(1) A version that can be installed in a local folder.

(2) More formal deployment that installs in C:\Program Files (x86) 
and puts entries in Start->Program menu and short-cut on Desktop.


Details

(1) A release that can be installed in local folder with no need for 
administrative permission: Unzip the file NavCleanerl.zip to a local folder. 
NavCleaner.exe is the executable image. Double-click on this item to proceed.
Two sample file are included: 97009PGC.NAV – a sample ASCII file 
and (2) test3_246_1408_to_246_1909.sgy a sample SEG-Y file.

(2) A Windows 7 install package that needs administrative privileges: 
Unzip the NavCleanerInstaller.zip file and follow standard installation procedures. 
Contains the same files as (1) but packaged to created shortcuts and menus



Mainfest of Release:

NavCleaner.exe			- Main program
Converters.dll			- Convert to IBM fp to IEEE fp
DouglasPeucker.dll		- Line thinning library
Geographic.dll			- GeographicLib library
msvcp110.dll			- Microsoft C++ redistributable package
msvcr110.dll			- Microsoft C++ runtime redistributable package
NETGeographic.dll		- .NET wrappers for GeographicLib
Oracle.ManagedDataAccess.dll	- Oracle ODP.NET database operations
SEGYlib.dll			- SEGY library
TeeChart.dll			- Graphics library
97009PGC.NAV			- Sample ASCII nav file
test3_246_1408_to_246_1909.sgy	- Sample SEGY file
NavCleaner.docx			- MS Word user manual
Readme.txt 			-  This file
NavCleanerInstall.zip		- Windows 7 Installer package

Contact ; Bob Courtney, Geologcical Survey of Canada Atlantic, Natural Resources Canada, bob.courtney@canada.ca

Februeary 23, 2016.