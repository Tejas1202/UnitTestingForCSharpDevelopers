using Moq;
using NUnit.Framework;
using System.Net;
using TestNinja.Mocking.InstallerHelper;

namespace TestNinja.UnitTests.MockingTests
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloader;
        private InstallerHelper _installerHelper;

        [SetUp]
        public void SetUp()
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloader.Object);
        }

        [Test]
        public void DownloadInstaller_DownloadFails_ReturnFalse()
        {
            // This will not throw an exception and return true because when we Setup mock objects and want to throw exception, we need to pass the exact same arguments
            // _fileDownloader.Setup(fd => fd.DownloadFile("", "")).Throws<WebException>();
            // Hence we should do:
            // _fileDownloader.Setup(fd => fd.DownloadFile("http://example.com/customer/installer", null)).Throws<WebException>();
            // But as the above line is a bit noisy, and sometimes we may not have access to those arguments, so a more generic way to do will be:
            _fileDownloader.Setup(fd => 
            fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).
            Throws<WebException>();
            // So any string passed as parameter will throw WebException

            var result = _installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.False);
        }

        [Test]
        public void DownloadInstaller_DownloadCompletes_ReturnTrue()
        {
            // No need to Setup anything here as by default DownloadFile method doesn't do anything

            var result = _installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.True);
        }
    }
}
