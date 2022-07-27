![example workflow](https://github.com/tschissler/releasedownloader/actions/workflows/dotnet.yml/badge.svg)
[![NuGet](https://img.shields.io/nuget/v/releasedownloader)](https://www.nuget.org/packages/ReleaseDownloader)

# ReleaseDownloader

This tool allows to download assets from the latest release in one or multiple GitHub repositories. 
The assets are put into a folder with the same name as the tag of a realease, downloads are only executed if the latest version is not already stored locally. 
This allows to maintain and update a nice version history of releases.

## Installation
The tool is distributed as a [dotnet tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools). 

A prerequisit to install the tool is to have a .Net SDK installed. 
Go to [https://dotnet.microsoft.com/en-us/download](https://dotnet.microsoft.com/en-us/download) to download the latest .Net version.
It is available for Windows, Linux, macOS and Docker.

Once ypu have .Net SDK installed, you can install ReleaseDownloader by running this command.

```bash
dotnet tool install --global ReleaseDownloader
```

This will install the latest version.

## Executing the tool
You can now execute the tool.
To see the various command line options just run 
```bash
releasedownloader.exe --help
```

## Updating the tool
You can easily update to the latest version by running
```bash
dotnet tool update --global ReleaseDownloader
```

