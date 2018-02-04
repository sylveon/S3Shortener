using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;

namespace Sylveon.S3Shortener
{
    internal class Server
    {
        public void Configure(IApplicationBuilder app, IConfiguration config)
        {
            ConfigModel configModel = new ConfigModel();
            config.GetSection("Settings").Bind(configModel);

            // CreateServiceClient can't load credentials from config, so just set env vars with them
            Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", config.GetSection("AWS").GetSection("Credentials")["AccessKey"], EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", config.GetSection("AWS").GetSection("Credentials")["SecretKey"], EnvironmentVariableTarget.Process);

            IAmazonS3 s3Client = config.GetAWSOptions().CreateServiceClient<IAmazonS3>();
            string protocol = configModel.UseHTTPS ? "https" : "http";
            string domain = configModel.Domain ?? $"{configModel.Bucket}.s3.{s3Client.Config.RegionEndpoint.SystemName}.amazonaws.com";

            app.Run(async context =>
            {
                var url = context.Request.Headers["url"].ToString();
                if (!(Uri.TryCreate(url, UriKind.Absolute, out var uri)
                    && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)))
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync(string.Empty);
                    return;
                }

                var s3Object = new PutObjectRequest()
                {
                    BucketName = configModel.Bucket,
                    Key = RandomNameGenerator.GetRandomLinkName(),
                    WebsiteRedirectLocation = url
                };
                await s3Client.PutObjectAsync(s3Object);

                string redirectUrl = $"{protocol}://{domain}/{s3Object.Key}";
                string response = "{'url':'" + redirectUrl + "'}";
                context.Response.ContentLength = response.Length;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(response);
            });
        }
    }
}