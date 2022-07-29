public interface IRepositoryClient
{
    IReleaseData GetLatestRelease(string repoOwner, string repoName);
}

