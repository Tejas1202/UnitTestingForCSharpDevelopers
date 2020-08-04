using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TestNinja.Mocking.VideoService
{
    public class VideoService
    {
        #region DI via Properties
        //public IFileReader FileReader { get; set; }

        //public VideoService()
        //{
        //    FileReader = new FileReader();
        //}

        //public string ReadVideoTitle()
        //{
        //    var str = FileReader.Read("video.txt");
        //    // ...
        //}
        #endregion

        #region DI via method parameters
        //public string ReadVideoTitle(IFileReader reader)
        //{
        //    var str = reader.Read("video.txt");
        //    // ....
        //}
        #endregion

        #region DI via ctor
        private IFileReader _fileReader;
        private readonly IVideoRepository _repository;

        // But this is poor man's DI. So always use DI frameworks instead of this because
        // it's creating new FileReader here, if we've multiple dependencies, then we need to pass all of them as optional parameters
        // and initialize them here which looks ugly
        public VideoService(IFileReader fileReader = null, IVideoRepository repository = null)
        {
            // (Not ideal) Hence this way, we're not breaking anything in our production code as we don't have to pass depedency 
            // And in our test code, we'll pass our MockFileReader to intialize _fileReader
            _fileReader = fileReader ?? new FileReader();
            _repository = repository ?? new VideoRepository();
        }
        #endregion

        public string ReadVideoTitle()
        {
            #region Legacy Code calling external resources in the class itself
            // var str = File.ReadAllText("video.txt");
            // Or when we do this, it's still having a dependency on the concrete class
            // var str = new FileReader().Read("video.txt");
            #endregion

            var str = _fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        #region Legacy code calling the database in the class itself, hence we need to change this for loose coupling
        //public string GetUnprocessedVideosAsCsv()
        //{
        //    var videoIds = new List<int>();

        //    using (var context = new VideoContext())
        //    {
        //        var videos =
        //            (from video in context.Videos
        //             where !video.IsProcessed
        //             select video).ToList();

        //        foreach (var v in videos)
        //            videoIds.Add(v.Id);

        //        return String.Join(",", videoIds);
        //    }
        //}
        #endregion

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();

            var videos = _repository.GetUnprocessedVideos();

            foreach (var v in videos)
                videoIds.Add(v.Id);

            return string.Join(",", videoIds);
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}
