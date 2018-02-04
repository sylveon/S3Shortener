using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Sylveon.S3Shortener
{
    public class Program
    {
        public static Task Main(string[] args) =>
            new WebHostBuilder()
                .ConfigureAppConfiguration(config =>
                {
                    config.AddJsonFile("settings.json");
                })
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Any, 5000, listenOptions =>
                    {
                        listenOptions.NoDelay = false;
                    });

                    options.UseSystemd();
                })
                .UseLibuv()
                .UseStartup<Server>()
                .Build().RunAsync();
    }
}