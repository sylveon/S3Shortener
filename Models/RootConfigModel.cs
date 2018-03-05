namespace Sylveon.S3Shortener.Models
{
    public class RootConfigModel
    {
        public string Domain { get; set; }
        public bool UseHTTPS { get; set; }
        public AWSConfigModel AWS { get; set; }
    }
}