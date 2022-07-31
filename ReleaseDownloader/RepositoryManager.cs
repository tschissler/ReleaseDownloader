using Octokit;

namespace ReleaseDownloader
{

    /// <summary>
    /// This class is used to access releases of a GitHub repository.
    /// </summary>    
    public class RepositoryManager
    {       
        private static GitHubClient CreateClient(string token)
        {
            var client = new GitHubClient(new ProductHeaderValue("ReleaseDownloader"));

            if (!string.IsNullOrWhiteSpace(token))
            {
                client.Credentials = new Credentials(token);
            }
            else if (Environment.GetEnvironmentVariable("GHTOKEN") != null)
            {
                client.Credentials = new Credentials(Environment.GetEnvironmentVariable("GHTOKEN"));
            }

            return client;
        }

        /// <summary>
        /// Get the latest release of a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="repository">The name of the repository.</param>
        public static ReleaseData GetLatestRelease(string repoOwner, string repoName, string token)
        {
            var client = CreateClient(token);
            var latestRelease = client.Repository.Release.GetLatest(repoOwner, repoName).Result;
            return new ReleaseData
            {
                Name = latestRelease.Name,
                TagName = latestRelease.TagName,
                Assets = new List<ReleaseAssetsData>(latestRelease.Assets.Select(a => new ReleaseAssetsData
                {
                    Name = a.Name,
                    BrowserDownloadUrl = a.Url
                }).ToList())
            };
        }

        public static void DownloadAssetFromRepo(string downloadUrl, string targetPath, string token)
        {
            var client = CreateClient(token);
            var responseRaw = client.Connection.Get<Byte[]>(new Uri(downloadUrl), new Dictionary<string, string>(), "application/octet-stream").Result;
            File.WriteAllBytes(targetPath, responseRaw.Body);
        }
    }
}