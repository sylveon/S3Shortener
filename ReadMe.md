# S3Shortener

A small ASP.NET Core app that allows you to use Amazon S3 as URL shortener for ShareX.

## Configuration

Create a `settings.json` file at the root of this repo following this format:

```javascript
{
  "Domain": "short.my-website.com", // Domain to use. Ignore to use regular S3 endpoint.
  "UseHTTPS": true, // Set to false to return URLs with "http://"
  "AWS": {
    "Bucket": "my-bucket", // Bucket name.
    "Region": "aws-region-descriptor", // For example "us-east-1"
    "AccessKey": "my-access-key", // Get those from IAM
    "SecretKey": "my-secret-key"
  }
}
```

You can use the following custom uploader for ShareX configuration:

```javascript
{
  "Name": "Amazon S3",
  "DestinationType": "URLShortener",
  "RequestType": "POST",
  "RequestURL": "http://localhost:5000/shorten", // Change this to point to your own instance
  "Arguments": {
    "url": "$input$"
  },
  "URL": "$json:url$"
}
```