using Octokit;

public class OctokitReleaseData : IReleaseData
{
    private Release _release;

    public OctokitReleaseData(Release release)
    {
        _release = release;
    }

    public string Name => _release.Name;
    public string TagName => _release.TagName;
    public IEnumerable<(string Name, string DownloadUrl)> Assets => _release.Assets.Select(a => (a.Name, a.BrowserDownloadUrl));
}

