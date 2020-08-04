using System.Net;

namespace TestNinja.Mocking.InstallerHelper
{
    public class InstallerHelper
    {
        private string _setupDestinationFile;
        private IFileDownloader _fileDownloader;

        public InstallerHelper(IFileDownloader fileDownloader)
        {
            _fileDownloader = fileDownloader;
        }

        #region Legacy code calling WebClient i.e. external dependency/resource in the class itself
        //public bool DownloadInstaller(string customerName, string installerName)
        //{
        //    var client = new WebClient();
        //    try
        //    {
        //        client.DownloadFile(
        //            string.Format("http://example.com/{0}/{1}",
        //                customerName,
        //                installerName),
        //            _setupDestinationFile);

        //        return true;
        //    }
        //    catch (WebException)
        //    {
        //        return false;
        //    }
        //}
        #endregion

        // Hence we're just constructing the url here (as this class has the information we require) and 
        // delegating the task of Downloading the file to FileDownloader
        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                _fileDownloader.DownloadFile(
                    string.Format("http://example.com/{0}/{1}",
                        customerName,
                        installerName),
                    _setupDestinationFile);

                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}