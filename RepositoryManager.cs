using Octokit;

namespace ReleaseDownloader
{

    /// <summary>
    /// This class is used to access releases of a GitHub repository.
    /// </summary>    
    public class RepositoryManager
    {
        private static readonly GitHubClient client;
        
        static RepositoryManager()
        {
            client = new GitHubClient(new ProductHeaderValue("ReleaseDownloader"));

            if (Environment.GetEnvironmentVariable("GHTOKEN") != null)
            {
                client.Credentials = new Credentials(Environment.GetEnvironmentVariable("GHTOKEN"));
            }
        }

        /// <summary>
        /// Authenticating GitHub access with a token.
        /// </summary>
        /// <param name="token">The GitHub token to use to connect to GitHub.</param>
        public static void Authenticate(string token)
        {
            client.Credentials = new Credentials(token);
        }

        /// <summary>
        /// Authenticate GitHub access with username and password. 
        /// </summary>
        /// <param name="username">The GitHub username.</param>
        /// <param name="password">The GitHub password.</param>
        public static void Authenticate(string username, string password)
        {
            client.Credentials = new Credentials(username, password);
        }

        /// <summary>
        /// Get the latest release of a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="repository">The name of the repository.</param>
        public static ReleaseData GetLatestRelease(string repoOwner, string repoName)
        {
            var latestRelease = client.Repository.Release.GetLatest(repoOwner, repoName).Result;
            return new ReleaseData
            {
                Name = latestRelease.Name,
                TagName = latestRelease.TagName,
                Assets = new List<ReleaseAssetsData>(latestRelease.Assets.Select(a => new ReleaseAssetsData
                {
                    Name = a.Name,
                    BrowserDownloadUrl = a.BrowserDownloadUrl
                }).ToList())
            };
        }
    }
}