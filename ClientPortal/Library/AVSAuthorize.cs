using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientPortal.Library
{
    public class AVSAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException("httpContext");

            // Make sure the user is authenticated.
            var cookie = httpContext.Request.Cookies.Get(Library.Helper.COOKIE_BEARER_TOKEN);
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                return true;
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new System.Web.Mvc.HttpStatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
            filterContext.HttpContext.Response.Redirect("~/Account/Login?returnUrl=" + HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.ToString()), true);
        }
    }
}