using TestNinja.Mocking.VideoService;

namespace TestNinja
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new VideoService();
            var title = service.ReadVideoTitle();

            // Passing class implementing our interface in dependency injection via Method Parameters
            //var title = service.ReadVideoTitle(new FileReader());
        }
    }
}
