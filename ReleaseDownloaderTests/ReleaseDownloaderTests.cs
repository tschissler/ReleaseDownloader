using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReleaseDownloader;
using System;
using System.Collections.Generic;
using System.IO;

namespace ReleaseDownloaderTests
{
    [TestClass]
    public class ReleaseDownloaderTests
    {
        [TestMethod]
        public void DownloadFromSingleValidPublicRepositorySingleAsset()
        {
            var targetDirectory = Path.Combine(Path.GetTempPath(), "_ReleaseDownloader");
            if (Directory.Exists(targetDirectory))
            {
                Directory.Delete(targetDirectory, true);
            }
            var repositories = new List<string> {"tschissler/ReleaseDownloader"};
            ReleaseDownloader.ReleaseDownloader.DownloadReleases(repositories, targetDirectory, false);

            Directory.Exists(Path.Combine(targetDirectory, "ReleaseDownloader")).Should().BeTrue();
            Directory.Exists(Path.Combine(targetDirectory, "ReleaseDownloader", "v1.0.0")).Should().BeTrue();
            File.Exists(Path.Combine(targetDirectory, "ReleaseDownloader", "v1.0.0", "test.png")).Should().BeTrue();
        }

        [TestMethod]
        public void DownloadFromSingleValidPrivateRepositorySingleAsset()
        {
            var targetDirectory = Path.Combine(Path.GetTempPath(), "_ReleaseDownloader");
            if (Directory.Exists(targetDirectory))
            {
                Directory.Delete(targetDirectory, true);
            }
            var repositories = new List<string> { "scrumorg/PSM" };
            ReleaseDownloader.ReleaseDownloader.DownloadReleases(repositories, targetDirectory, false);

            Directory.Exists(Path.Combine(targetDirectory, "PSM")).Should().BeTrue();
            var directory = Directory.GetDirectories(Path.Combine(targetDirectory, "PSM"))[0];
            File.Exists(Path.Combine(targetDirectory, "ReleaseDownloader", "PSM", directory, "PSM.Reference.Guide.pptx")).Should().BeTrue();
        }

        [TestMethod]
        public void ExceptionIfPATIsNotProvided()
        {
            var targetDirectory = Path.Combine(Path.GetTempPath(), "_ReleaseDownloader");
            var repositories = new List<string> { "scrumorg/PSM" };
            RepositoryManager.Authenticate("abc");

            var action = new Action(() =>
                ReleaseDownloader.ReleaseDownloader.DownloadReleases(repositories, targetDirectory, false));
            action.Should().Throw<AccessViolationException>("Cannot access the GitHub repository or asset. For private repositories make sure you have provided an valid access token and the URL is correct.");
        }
    }
}