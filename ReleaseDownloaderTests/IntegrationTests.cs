namespace ReleaseDownloaderTests
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void TestMethod1()
    {
      var targetDirectory = "TestDirectory";
      var client = new OctokitRepositoryClient();
      var latestRelease = client.GetLatestRelease("tschissler", "ReleaseDownloader");

      var releaseDownloadManager = new ReleaseDownloadManager();
      releaseDownloadManager.Download(targetDirectory, latestRelease);

      Assert.IsTrue(Directory.Exists(Path.Combine(targetDirectory, latestRelease.TagName)));
    }
  }
}