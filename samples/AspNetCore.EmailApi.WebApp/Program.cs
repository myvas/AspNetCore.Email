using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace AspNetCore.EmailApi.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var host = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build())
            {
                host.Run();
            }
        }
    }
}
