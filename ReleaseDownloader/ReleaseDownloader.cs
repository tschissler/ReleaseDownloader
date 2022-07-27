namespace ReleaseDownloader
{
    public class ReleaseDownloader
    {
        public static void DownloadReleases(IEnumerable<string> repositories, string targetDirectory, bool force)
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

                Console.WriteLine(string.Concat(Enumerable.Repeat("-", 80)));
                Console.WriteLine($"Latest Releases is: {latestRelease.TagName}");

                var latestReleaseDirectory = Path.Combine(targetDirectory, repositoryParts[1], latestRelease.TagName);
                if (!Directory.Exists(latestReleaseDirectory))
                {
                    Console.WriteLine($"Release does not exist locally, creating directory: {latestReleaseDirectory}");
                    DownloadAssets(latestRelease, latestReleaseDirectory);
                }
                else
                {
                    if (force)
                    {
                        Console.WriteLine($"Release exists but force parameter is set.{Environment.NewLine}");
                        Console.WriteLine($"Local folder {latestReleaseDirectory} will be deleted and recreated.");
                        Console.WriteLine($"Do you really want to continue? [Y]es [N]o");
                        var input = Console.ReadLine().ToLower();
                        if (input == "y" || input == "yes")
                        {                            
                            Directory.Delete(latestReleaseDirectory, true);
                            DownloadAssets(latestRelease, latestReleaseDirectory);                            
                        }
                        else
                        {
                            Console.WriteLine($"Aborted downloading release for {repository}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Release exists locally, skipping download: {latestReleaseDirectory}");
                    }
                }
            }
            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 80)));
        }

        private static void DownloadAssets(ReleaseData latestRelease, string latestReleaseDirectory)
        {
            Directory.CreateDirectory(latestReleaseDirectory);

            foreach (var asset in latestRelease.Assets)
            {
                var downloadUrl = asset.BrowserDownloadUrl;
                var targetFile = Path.Join(latestReleaseDirectory, asset.Name);
                Console.Write($"    Downloading: {asset.Name} ... ");
                RepositoryManager.DownloadAssetFromRepo(downloadUrl, targetFile);
                Console.WriteLine("Done");
            }
        }
    }    
}