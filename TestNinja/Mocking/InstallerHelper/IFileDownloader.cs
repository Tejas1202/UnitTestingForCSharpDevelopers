namespace TestNinja.Mocking.InstallerHelper
{
    public interface IFileDownloader
    {
        void DownloadFile(string url, string path);
    }
}