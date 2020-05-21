using Microsoft.Extensions.Hosting;

namespace ConfigurationTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .SetCurrentDirectory(args)
                .UseWindowsService()
                .ConfigureAppConfiguration()
                .ConfigureLogging()
                .ConfigureServices();
    }
}
