using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Myvas.AspNetCore.EmailApi.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder().UseStartup<Startup>();
    }
}
