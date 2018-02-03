# S3Shortener

A small .NET Core app that allows you to use Amazon S3 as URL shortener for ShareX.

## Configuration

Create a `settings.json` file at the root of this repo following this format:

```json
{
  "Settings": {
    "Bucket": "short.charlesmilette.net", // Bucket name.
    "Domain": "short.charlesmilette.net", // Domain to use. Ignore to use regular S3 endpoint.
    "UseHTTPS": true // Set to false to return URLs with "http://"
  },
  "AWS": {
    "Region": "aws-region-descriptor", // For example "us-east-1"
    "Credentials": {
      "AccessKey": "my-access-key", // Get those from IAM
      "SecretKey": "my-secret-key"
    }
  }
}
```

You can use the following custom uploader for ShareX configuration:

```json
{
  "Name": "Amazon S3",
  "DestinationType": "URLShortener",
  "RequestType": "GET",
  "RequestURL": "http://localhost:5000/", // Change this to point to your own instance
  "Headers": {
    "url": "$input$"
  },
  "URL": "$json:url$"
}
```