namespace ReleaseDownloader
{
    public class ReleaseDownloader
    {
        public static void DownloadReleases(IEnumerable<string> repositories, string targetDirectory, bool force, string? token = null)
        {
            foreach (var repository in repositories)
            {
                ReleaseData latestRelease;

                if (repository.Count(c => c == '/') != 1)
                {
                    throw new ArgumentException($"Invalid repository format {repository}. Expected: owner/repository");
                }

                var repositoryParts = repository.Split('/');
                try
                {
                    latestRelease = RepositoryManager.GetLatestRelease(repositoryParts[0], repositoryParts[1], token);
                }
                catch (Exception ex)
                {
                    throw new AccessViolationException("Cannot access the GitHub repository or asset. For private repositories make sure you have provided an valid access token and the URL is correct.", ex);
                }

                Console.WriteLine(string.Concat(Enumerable.Repeat("-", 80)));
                Console.WriteLine($"Latest Releases is: {latestRelease.TagName}");

                var latestReleaseDirectory = Path.Combine(targetDirectory, repositoryParts[1], latestRelease.TagName);
                if (!Directory.Exists(latestReleaseDirectory))
                {
                    Console.WriteLine($"Release does not exist locally, creating directory: {latestReleaseDirectory}");
                    DownloadAssets(latestRelease, latestReleaseDirectory, token);
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
                            DownloadAssets(latestRelease, latestReleaseDirectory, token);                            
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

        private static void DownloadAssets(ReleaseData latestRelease, string latestReleaseDirectory, string token)
        {
            Directory.CreateDirectory(latestReleaseDirectory);

            try
            {
                foreach (var asset in latestRelease.Assets)
                {
                    var downloadUrl = asset.BrowserDownloadUrl;
                    var targetFile = Path.Join(latestReleaseDirectory, asset.Name);
                    Console.Write($"    Downloading: {asset.Name} ... ");
                    RepositoryManager.DownloadAssetFromRepo(downloadUrl, targetFile, token);
                    Console.WriteLine("Done");
                }
            }
            catch(Exception ex)
            {
                throw new HttpRequestException($"Error downloading asset from GitHub. Errormessage: {ex.Message}", ex);
            }
        }
    }    
}