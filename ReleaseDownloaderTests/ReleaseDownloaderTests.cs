using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReleaseDownloader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReleaseDownloaderTests
{
    [TestClass]
    public class ReleaseDownloaderTests
    {
        [TestMethod]
        public void DownloadFromSingleValidPublicRepositoryTwoAsset()
        {
            var targetDirectory = Path.Combine(Path.GetTempPath(), "_ReleaseDownloader");
            if (Directory.Exists(targetDirectory))
            {
                Directory.Delete(targetDirectory, true);
            }
            var repositories = new List<string> {"tschissler/ReleaseDownloader"};
            ReleaseDownloader.ReleaseDownloader.DownloadReleases(repositories, targetDirectory, false);

            Directory.Exists(Path.Combine(targetDirectory, "ReleaseDownloader")).Should().BeTrue();
            Directory.Exists(Path.Combine(targetDirectory, "ReleaseDownloader", "v1.0.1")).Should().BeTrue();
            File.Exists(Path.Combine(targetDirectory, "ReleaseDownloader", "v1.0.1", "test.png")).Should().BeTrue();
            File.Exists(Path.Combine(targetDirectory, "ReleaseDownloader", "v1.0.1", "Test2.jpg")).Should().BeTrue();
        }

        [TestMethod]
        public void DownloadFromSingleValidPrivateRepositoryWithPATFromEnvironmentVariable()
        {
            if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("GHTOKEN2")))
            {
                Assert.Fail("To execute this test you must provide a valid GitHub access token in the environment variable GHTOKEN2");
            }

            Environment.SetEnvironmentVariable("GHTOKEN", Environment.GetEnvironmentVariable("GHTOKEN2"), EnvironmentVariableTarget.Process);

            var targetDirectory = Path.Combine(Path.GetTempPath(), "_ReleaseDownloader");
            if (Directory.Exists(targetDirectory))
            {
                Directory.Delete(targetDirectory, true);
            }
            var repositories = new List<string> { "scrumorg/PSM" };
            ReleaseDownloader.ReleaseDownloader.DownloadReleases(repositories, targetDirectory, false);

            Directory.Exists(Path.Combine(targetDirectory, "PSM")).Should().BeTrue();
            var directory = Directory.GetDirectories(Path.Combine(targetDirectory, "PSM"))[0];
            var version = directory.Split("\\").Last();
            File.Exists(Path.Combine(directory, $"PSM.Reference.Guide.{version}.pptx")).Should().BeTrue();
        }

        [TestMethod]
        public void DownloadFromSingleValidPrivateRepositoryWithPATProvidedAsParameter()
        {
            if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("GHTOKEN2")))
            {
                Assert.Fail("To execute this test you must provide a valid GitHub access token in the environment variable GHTOKEN2");
            }

            Environment.SetEnvironmentVariable("GHTOKEN", "", EnvironmentVariableTarget.Process);


            var targetDirectory = Path.Combine(Path.GetTempPath(), "_ReleaseDownloader");
            if (Directory.Exists(targetDirectory))
            {
                Directory.Delete(targetDirectory, true);
            }
            var repositories = new List<string> { "scrumorg/PSM" };
            ReleaseDownloader.ReleaseDownloader.DownloadReleases(repositories, targetDirectory, false, Environment.GetEnvironmentVariable("GHTOKEN2"));

            Directory.Exists(Path.Combine(targetDirectory, "PSM")).Should().BeTrue();
            var directory = Directory.GetDirectories(Path.Combine(targetDirectory, "PSM"))[0];
            var version = directory.Split("\\").Last();
            File.Exists(Path.Combine(directory, $"PSM.Reference.Guide.{version}.pptx")).Should().BeTrue();
        }


        [TestMethod]
        public void ExceptionIfPATIsNotProvided()
        {
            Environment.SetEnvironmentVariable("GHTOKEN", "", EnvironmentVariableTarget.Process);

            var targetDirectory = Path.Combine(Path.GetTempPath(), "_ReleaseDownloader");
            var repositories = new List<string> { "scrumorg/PSM" };

            var action = new Action(() =>
                ReleaseDownloader.ReleaseDownloader.DownloadReleases(repositories, targetDirectory, false, "abc"));
            action.Should().Throw<AccessViolationException>("Cannot access the GitHub repository or asset. For private repositories make sure you have provided an valid access token and the URL is correct.");
        }
    }
}