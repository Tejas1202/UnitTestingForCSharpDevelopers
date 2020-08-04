using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking.VideoService
{
    // This is referred as Repository pattern
    // Hence VideoRepository is encapsulating the code that touches external resource
    class VideoRepository : IVideoRepository
    {
        public IEnumerable<Video> GetUnprocessedVideos()
        {
            using(var context = new VideoContext())
            {
                var videos =
                    (from video in context.Videos
                     where !video.IsProcessed
                     select video).ToList();

                return videos;
            }
        }
    }
}
