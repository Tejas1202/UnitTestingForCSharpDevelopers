using System.IO;

namespace TestNinja.Mocking.VideoService
{
    // 1st step: Encapsulating the logic of Reading file (i.e. an external resouce) into FileReader class
    class FileReader : IFileReader
    {
        public string Read(string path)
        {
            return File.ReadAllText(path);
        }
    }
}