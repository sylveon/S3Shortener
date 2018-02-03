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
                .ConfigureAppConfiguration((_, config) =>
                {
                    config.AddJsonFile("settings.json");
                })
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Loopback, 5000, listenOptions =>
                    {
                        listenOptions.NoDelay = false;
                    });
                })
                .UseStartup<Server>()
                .Build().RunAsync();
    }
}