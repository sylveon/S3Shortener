using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Sylveon.S3Shortener.Attributes
{
    public class SecretAttribute : ActionFilterAttribute
    {
        protected string Secret { get; set; }

        public SecretAttribute(string secret = null) => this.Secret = secret;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string auth = filterContext.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(this.Secret) || (!string.IsNullOrEmpty(auth) && auth == this.Secret))
            {
                return;
            }
            filterContext.Result = new StatusCodeResult(403);
        }
    }
}