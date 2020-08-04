using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestNinja.Mocking.VideoService;

namespace TestNinja.UnitTests.MockingTests
{
    // Hence by doing loose coupling, we're able create a fake FileReader whose Read method just returns "" as our Unit tests
    // should not touch external resources like the original FileReader's read method does. Because if we touch external resources,
    // then it becomes Integration testing and not Unit testing
    [TestFixture]
    public class VideoServiceTests
    {
        private VideoService _videoService;
        private Mock<IFileReader> _fileReader;
        private Mock<IVideoRepository> _repository;

        [SetUp]
        public void SetUp()
        {
            // Here we're telling the Moq library that we want an object that implements IFileReader interface
            // hence this fileReader is a mock object. 
            _fileReader = new Mock<IFileReader>();
            _repository = new Mock<IVideoRepository>();
            // DI via ctor
            _videoService = new VideoService(_fileReader.Object, _repository.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            // Now we need to program this mock, as by default it doesn't have any behaviour which we do by Setup Method
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            #region DI via property/method
            // DI via property
            // service.FileReader = new MockFileReader();

            // DI via method parameter
            // var result = service.ReadVideoTitle(new MockFileReader());
            #endregion

            var result = _videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnAnEmptyString()
        {
            _repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Get_UnprocessedVideosAsCsv_AFewUnprocessedVideos_ReturnAStringWithIdOfUnprocessedVideos()
        {
            _repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>
            { new Video {Id = 1},
              new Video {Id = 2},
              new Video {Id = 3}
            });

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }

    }

    // We use Moq library so that we don't have to create class for every different execution path like this
    public class MockFileReader : IFileReader
    {
        public string Read(string path)
        {
            return "";
        }
    }
}
