using CommandLine;
using CommandLine.Text;

public class CommandLineOptions
{
    [Value(0, Min = 1, MetaName = "Repositories", HelpText="Provide a list of repositories in the form [owner]/[reponame], e.g tschissler/releasedownloader tschissler/esp32demo")]
    public IEnumerable<string>? Repositories { get; set; }

    [Option('d', "DownloadDirectory", Required = true, HelpText = "The directory to download the files to.")]
    public string? DownloadDirectory { get; set; }

    [Option('g', "GitHubToken", Required = false, HelpText = "The GitHub token to use to connect to GitHub. The token can also be provided via an environment variable GHTOKEN. See https://bit.ly/3veXlYG for more information on how to greate an access token.")]
    public string? GitHubToken { get; set; }

    [Option('f', "Force", Required = false, HelpText = "Forces to download latest release even if it exists locally, will delete local folder before downloarding.")]
    public bool Force { get; set; } = false;

    [Usage (ApplicationAlias = "ReleaseDownloader.exe")]
    public static IEnumerable<Example> Examples
    {
        get
        {
            yield return new Example("Download release from single repository", new CommandLineOptions { Repositories = new List<string> { "tschissler/releasedownloader" }, DownloadDirectory = "c:\\temp" });
            yield return new Example("Download release from multiple repositories", new CommandLineOptions { Repositories = new List<string> { "tschissler/releasedownloader", "tschissler/ESP32Demo" }, DownloadDirectory = "c:\\temp" });
        }
    }
}