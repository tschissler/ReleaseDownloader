using Octokit;

public class OctokitRepositoryClient : IRepositoryClient
{
    private readonly GitHubClient _client;
    public OctokitRepositoryClient(string? accessToken = null)
    {
        _client = new GitHubClient(new ProductHeaderValue("ReleaseDownloader"));
        if (accessToken != null)
        {
            var tokenAuth = new Credentials(accessToken);
            _client.Credentials = tokenAuth;
        }
    }

    public IReleaseData GetLatestRelease(string repoOwner, string repoName)
    {
        var release = _client.Repository.Release.GetLatest(repoOwner, repoName).Result;
        return new OctokitReleaseData(release);
    }
}

