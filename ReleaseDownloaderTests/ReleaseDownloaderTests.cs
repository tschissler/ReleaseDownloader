using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Directory.Delete(targetDirectory, true);
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
            Directory.Delete(targetDirectory, true);
            var repositories = new List<string> { "scrumorg/PSM" };
            ReleaseDownloader.ReleaseDownloader.DownloadReleases(repositories, targetDirectory, false);

            Directory.Exists(Path.Combine(targetDirectory, "PSM")).Should().BeTrue();
            Directory.Exists(Path.Combine(targetDirectory, "PSM", "v1.0.0")).Should().BeTrue();
            File.Exists(Path.Combine(targetDirectory, "ReleaseDownloader", "v1.0.0", "test.png")).Should().BeTrue();
        }
    }
}