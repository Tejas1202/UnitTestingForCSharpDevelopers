using System.Net;

namespace TestNinja.Mocking.InstallerHelper
{
    // If WebClient had an interface like IWebClient, then we wouldn't have needed to create this seperate class for extracting
    // external dependency, we could've mocked it directly in the Test by passing IWebClient in ctor
    public class FileDownloader : IFileDownloader
    {
        public void DownloadFile(string url, string path)
        {
            var client = new WebClient();
            client.DownloadFile(url, path);
        }
    }
}
