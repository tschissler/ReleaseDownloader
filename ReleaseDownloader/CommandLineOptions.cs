using CommandLine;

public class CommandLineOptions
{
    [Value(0, Min = 1)]
    public IEnumerable<string>? Repositories { get; set; }

    [Option('d', "DownloadDirectory", Required = false, HelpText = "The directory to download the files to.")]
    public string? DownloadDirectory { get; set; }

    [Option('g', "GitHubToken", Required = false, HelpText = "The GitHub token to use to connect to GitHub. The token can also be provided via an environment variable GHTOKEN. See https://bit.ly/3veXlYG for more information on how to greate an access token.")]
    public string? GitHubToken { get; set; }

    [Option('f', "Force", Required = false, HelpText = "Forces to download latest release even if it exists locally, will delete local folder before downloarding.")]
    public bool Force { get; set; } = false;
}