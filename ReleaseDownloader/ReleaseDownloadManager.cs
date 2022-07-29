using System.Net;

public class ReleaseDownloadManager
{
  
    public void Download(string targetDirectory, IReleaseData releaseData)
    {
       var releaseDirectory = Path.Combine(targetDirectory, releaseData.TagName);
        if (!Directory.Exists(releaseDirectory))
        {
            Directory.CreateDirectory(releaseDirectory);
            foreach (var asset in releaseData.Assets)
            {
                var downloadUrl = asset.DownloadUrl;
                var targetFile = Path.Join(releaseDirectory, asset.Name);
                DownloadFileFromUrl(downloadUrl, targetFile);
            }
        }
    }

     private static void DownloadFileFromUrl(string downloadUrl, string targetPath)
    {
        using WebClient webClient = new WebClient();
        
        webClient.DownloadFile(downloadUrl, targetPath);
    }
}