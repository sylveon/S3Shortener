using System;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sylveon.S3Shortener.Models;
using Sylveon.S3Shortener.Utilities;

namespace Sylveon.S3Shortener.Controllers
{
    [Route("[controller]")]
    public class ShortController : Controller
    {
        private readonly RandomNameGenerator _rng;
        private readonly IAmazonS3 _s3;
        private readonly RootConfigModel _config;

        public ShortController(RandomNameGenerator rng, IAmazonS3 s3, IConfiguration config)
        {
            _rng = rng;
            _s3 = s3;
            _config = config.Get<RootConfigModel>();
        }

        [HttpPost]
        public async Task<IActionResult> Shorten(string url)
        {
            if (!(Uri.TryCreate(url, UriKind.Absolute, out var uri)
                && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)))
            {
                return StatusCode(400);
            }

            var s3Object = new PutObjectRequest
            {
                BucketName = _config.AWS.Bucket,
                Key = _rng.GetRandomLinkName(),
                WebsiteRedirectLocation = url
            };

            await _s3.PutObjectAsync(s3Object);

            string protocol = _config.UseHTTPS ? "https" : "http";
            string domain = _config.Domain ?? $"{_config.AWS.Bucket}.s3.{_s3.Config.RegionEndpoint.SystemName}.amazonaws.com";
            string redirectUrl = $"{protocol}://{domain}/{s3Object.Key}";
            return Json(new { url = redirectUrl });
        }
    }
}