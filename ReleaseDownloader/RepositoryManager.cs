using Octokit;

namespace ReleaseDownloader
{
    public class RepositoryManager
    {
        private readonly GitHubClient client;

        public RepositoryManager()
        {
            client = new GitHubClient(new ProductHeaderValue("ReleaseDownloader"));
        }

        public Release GetLatestRelease(string repoOwner, string repoName)
        {
            return client.Repository.Release.GetLatest(repoOwner, repoName).Result;
        }
    }
}