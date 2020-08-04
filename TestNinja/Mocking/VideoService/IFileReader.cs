namespace TestNinja.Mocking.VideoService
{
    // 2nd step: Extracting an interface from that class and make classes implement this interface
    public interface IFileReader
    {
        string Read(string path);
    }
}