using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Sylveon.S3Shortener.Attributes
{
    public class SecretAttribute : ActionFilterAttribute
    {
        private readonly string _secret;

        public SecretAttribute(string secret = null) => _secret = secret;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string auth = filterContext.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(_secret) || (!string.IsNullOrEmpty(auth) && auth == _secret))
            {
                return;
            }
            filterContext.Result = new StatusCodeResult(403);
        }
    }
}