internal class Program
{
    private static void Main(string[] args)
    {
        var gitHubAccessToken = Environment.GetEnvironmentVariable("GitHubAccessToken");
        var targetDirectory = Path.Combine(Path.GetTempPath(), "_ReleaseDownloader");
        var client = new OctokitRepositoryClient(gitHubAccessToken);
        var latestRelease =  client.GetLatestRelease("tschissler", "ReleaseDownloader");

        var releaseDownloadManager = new ReleaseDownloadManager();
        releaseDownloadManager.Download(targetDirectory, latestRelease);

        Console.WriteLine($"Releases found: {latestRelease.Name}");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
