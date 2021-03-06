﻿using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Sylveon.S3Shortener
{
    public class Program
    {
        public static Task Main(string[] args) =>
            new WebHostBuilder()
                .ConfigureAppConfiguration(config =>
                {
                    config
                        .SetBasePath(Path.GetDirectoryName(typeof(Program).Assembly.Location))
                        .AddJsonFile("settings.json");
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                })
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Loopback, 5000, listenOptions =>
                    {
                        listenOptions.NoDelay = false;
                    });

                    options.UseSystemd();
                })
                .UseLibuv()
                .UseStartup<Startup>()
                .Build().RunAsync();
    }
}