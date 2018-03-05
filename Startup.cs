using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sylveon.S3Shortener.Models;
using Sylveon.S3Shortener.Utilities;
using System;
using System.IO;

namespace Sylveon.S3Shortener
{
    internal class Startup
    {
        private readonly RootConfigModel _config;

        public Startup(IConfiguration config)
        {
            _config = config.Get<RootConfigModel>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string data = Path.Combine(Path.GetDirectoryName(typeof(Startup).Assembly.Location), "Data");

            services
                .AddAWSService<IAmazonS3>(new AWSOptions
                {
                    Credentials = new BasicAWSCredentials(_config.AWS.AccessKey, _config.AWS.SecretKey),
                    Region = RegionEndpoint.GetBySystemName(_config.AWS.Region ?? RegionEndpoint.USEast1.SystemName)
                })
                .AddSingleton<RandomNameGenerator>(new RandomNameGenerator(data))
                .AddMvc();
        }
    }
}