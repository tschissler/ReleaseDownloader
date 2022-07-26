namespace ReleaseDownloader
{

    public class ReleaseData
    {
        public string? Name { get; set; }
        public string? TagName { get; set; }
        public IReadOnlyList<ReleaseAssetsData>? Assets { get; set; }
    }

    public class ReleaseAssetsData
    {
        public string? Name { get; set; }
        public string? BrowserDownloadUrl { get; set; }
    }
}