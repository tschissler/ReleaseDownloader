namespace ReleaseDownloader
{
    public class ReleaseDownloader
    {
        public static void DownloadReleases(IEnumerable<string> repositories, string targetDirectory)
        {
            foreach (var repository in repositories)
            {
                if (repository.Count(c => c == '/') != 1)
                {
                    System.Console.WriteLine($"Invalid repository format {repository}. Expected: owner/repository");
                    break;
                }

                var repositoryParts = repository.Split('/');
                var latestRelease = RepositoryManager.GetLatestRelease(repositoryParts[0], repositoryParts[1]);

                Console.WriteLine($"Latest Releases is: {latestRelease.TagName}");

                var latestReleaseDirectory = Path.Combine(targetDirectory, latestRelease.TagName);
                if (!Directory.Exists(latestReleaseDirectory))
                {
                    Console.WriteLine($"Release does not exist locally, creating directory: {latestReleaseDirectory}");

                    Directory.CreateDirectory(latestReleaseDirectory);

                    foreach (var asset in latestRelease.Assets)
                    {
                        var downloadUrl = asset.BrowserDownloadUrl;
                        var targetFile = Path.Join(latestReleaseDirectory, asset.Name);
                        Console.Write($"    Downloading: {asset.Name} ... ");
                        DownloadManager.DownloadFileFromUrl(downloadUrl, targetFile);
                        Console.WriteLine("Done");
                    }
                }
                else
                {
                    Console.WriteLine($"Release exists locally, skipping download: {latestReleaseDirectory}");
                }

            }
        }
    }    
}