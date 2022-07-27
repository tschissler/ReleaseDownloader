using System.Net;
using CommandLine;
using ReleaseDownloader;

internal class Program
{
    private static int Main(string[] args)
    {
        Console.WriteLine(string.Concat(Enumerable.Repeat("#", 80)));
        Console.WriteLine("  GitHub Release Downloader");
        Console.WriteLine(string.Concat(Enumerable.Repeat("#", 80)));
        Console.WriteLine();
        Console.WriteLine("This tool downloads assets from the latest release of a GitHub repository.");
        Console.WriteLine("It creates a local folder with the repository name and herein another folder with the release tag.");
        Console.WriteLine("If a local folder for the latest release already exists, the download will be skipped.");
        Console.WriteLine("You can provide a list of repositories to update them in one run.");
        Console.WriteLine();
        Console.WriteLine("Type ReleaseDownloader.exe --help to see more options");
        Console.WriteLine();
        Console.WriteLine();

        var parser = new Parser(with =>
        {
            with.AutoHelp = true;
            with.AutoVersion = true;
            with.HelpWriter = Console.Out;            
        });

        var result = parser.ParseArguments<CommandLineOptions>(args);
        if (result.Errors.Any())
        {
            return -1;
        }

        var targetDirectory = result.Value.DownloadDirectory ?? (string?)Path.Combine(Path.GetTempPath(), "_ReleaseDownloader");  
        
        if (!string.IsNullOrWhiteSpace(result.Value.GitHubToken))
        {
            RepositoryManager.Authenticate(result.Value.GitHubToken);
        }

        try
        {            
            ReleaseDownloader.ReleaseDownloader.DownloadReleases(result.Value.Repositories, targetDirectory, result.Value.Force);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }

        return 0;
    }
}