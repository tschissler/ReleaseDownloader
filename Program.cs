using System.Net;
using CommandLine;
using ReleaseDownloader;

internal class Program
{
    private static int Main(string[] args)
    {
        var result = Parser.Default.ParseArguments<CommandLineOptions>(args);
        if (result.Errors.Any())
        {
            return -1;
        }

        var targetDirectory = result.Value.DownloadDirectory ?? (string?)Path.Combine(Path.GetTempPath(), "_ReleaseDownloader");

        ReleaseDownloader.ReleaseDownloader.DownloadReleases(result.Value.Repositories, targetDirectory);

        return 0;
    }
}