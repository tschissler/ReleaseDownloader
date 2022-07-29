public interface IReleaseData
{
    string Name { get; }
    string TagName { get; }
    IEnumerable<(string Name, string DownloadUrl)> Assets { get; }
}

