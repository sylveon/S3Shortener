﻿namespace Sylveon.S3Shortener.Models
{
    public class AWSConfigModel
    {
        public string Bucket { get; set; }
        public string Region { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }
}