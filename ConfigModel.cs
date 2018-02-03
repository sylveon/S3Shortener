namespace Sylveon.S3Shortener
{
    public class ConfigModel
    {
        public string Bucket { get; set; }
        public string Domain { get; set; }
        public bool UseHTTPS { get; set; }
    }
}