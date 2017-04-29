using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace aspnetcore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel((options) =>
                {
                    options.ThreadCount = 3;
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls("http://localhost:9292")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
