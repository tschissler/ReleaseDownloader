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

Once you have .Net SDK installed, you can install ReleaseDownloader by running this command.

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


## Examples

### Simple download
Imagine, you have a repositories where you want to download the latest release. 
Let's assume one is the ReleaseDownloader repositor which has an example release.

![Example Repository](/Images/ExampleRepository.png)

For ReleaseDownloader you need the owner of the repository (1) and the repository name (2).
It will then download the assets from the latest release in a local folder that you provide.
In this local folder it will create a subfolder named by the repository name and within this another subfolder named by the tag of the release (3) and in this folder it will download all assets.
Next time you run the tool, it will check if there is a new release and download it into a new subfolder. 
If you have already the latest release downloaded, it will be skipped.

So how would the command for this example look like?

```bash
releasedownloader.exe --DownloadDirectory c:\temp tschissler/releasedownloader
```

This will place the assets in your c:\temp folder. 
For Linux or macOS you have to ensure you are providing the path in the corresponding format e.g. ~/releases.

The resulting folder structure will be like

- c:
    - temp
        - releasedownloader
            - v1.0.0
                - [Assets here]

### Downloading from private repositories
If you are downloading releases from private repositories, you have to provide a GitHub access token so ReleaseDownloader can authorize on GitHub.
To create an access token, go to the GitHub and make sure you are logged in with your account.
On the right upper corner you see your profile image.
Click on it and select "Settings". Then select "Developer settings" from the left navigation (on the very bottom).
Here select "Personal Access Token" and then click "Generate new token".
Give the token a name, check the checkbox next to "repo" and click "Generate token" at the bottom of the page.

The token is now shown to you, but only once. So copy it and store it at a save place.

You can then provide this token via the ```-- GitHubToken``` parameter.

```bash
releasedownloader.exe --DownloadDirectory c:\temp --GitHubToken [Your Token] tschissler/releasedownloader
```

### Download multiple repositories
Now let's assume you have another repository where you also want to download release assets.
You can easily expand the command from above to do so.

```bash
releasedownloader.exe --DownloadDirectory c:\temp tschissler/releasedownloader dotnet/samples
```

The output will look like this.

```bash
################################################################################
  GitHub Release Downloader
################################################################################

This tool downloads assets from the latest release of a GitHub repository.
It creates a local folder with the repository name and herein another folder with the release tag.
If a local folder for the latest release already exists, the download will be skipped.
You can provide a list of repositories to update them in one run.

Type ReleaseDownloader.exe --help to see more options


--------------------------------------------------------------------------------
Latest Releases is: v1.0.0
Release does not exist locally, creating directory: c:\temp\releasedownloader\v1.0.0
    Downloading: Test.png ... Done
--------------------------------------------------------------------------------
Latest Releases is: 241566
Release does not exist locally, creating directory: c:\temp\samples\241566
    Downloading: _NET_Core_unit_testing_code_coverage.zip ... Done
--------------------------------------------------------------------------------
```

The resulting structure will be then this

- c:
    - temp
        - releasedownloader
            - v1.0.0
                - [Assets here]
        - samples
            - 241566
                - [Assets here]


## Updating the tool
You can easily update to the latest version by running
```bash
dotnet tool update --global ReleaseDownloader
```

