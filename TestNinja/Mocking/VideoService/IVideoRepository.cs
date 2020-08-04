using System.Collections.Generic;

namespace TestNinja.Mocking.VideoService
{
    public interface IVideoRepository
    {
        IEnumerable<Video> GetUnprocessedVideos();
    }
}
