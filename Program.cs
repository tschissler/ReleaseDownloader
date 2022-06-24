using System.Net;
using Octokit;

internal class Program
{
    private static void Main(string[] args)
    {
        var targetDirectory = Path.Combine(Path.GetTempPath(), "_ReleaseDownloader");
        Release latestRelease = GetLatestRelease("tschissler", "ReleaseDownloader");

        var latestReleaseDirectory = Path.Combine(targetDirectory, latestRelease.TagName);
        if (!Directory.Exists(latestReleaseDirectory))
        {
            Directory.CreateDirectory(latestReleaseDirectory);
            var asset = latestRelease.Assets[0];
            var downloadUrl = asset.BrowserDownloadUrl;
            var targetFile = Path.Join(latestReleaseDirectory, asset.Name);
            DownloadFileFromUrl(downloadUrl, targetFile);
        }

        Console.WriteLine($"Releases found: {latestRelease.Name}");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    private static Release GetLatestRelease(string repoOwner, string repoName)
    {
        var client = new GitHubClient(new ProductHeaderValue("ReleaseDownloader"));
        var latestRelease = client.Repository.Release.GetLatest(repoOwner, repoName).Result;
        return latestRelease;
    }

    private static void DownloadFileFromUrl(string downloadUrl, string targetPath)
    {
        using WebClient webClient = new WebClient();

        webClient.DownloadFile(downloadUrl, targetPath);
    }
}