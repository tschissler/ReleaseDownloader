using System.Net;

namespace ReleaseDownloader
{
    public class DownloadManager
    {
        public static void DownloadFileFromUrl(string downloadUrl, string targetPath)
        {
            using WebClient webClient = new WebClient();
            webClient.DownloadFileAsync(new Uri(downloadUrl), targetPath);
        }
    }
}