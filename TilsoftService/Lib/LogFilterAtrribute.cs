using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TilsoftService.Helper;

namespace TilsoftService.Lib
{
    public class LogFilterAtrribute : ActionFilterAttribute
    {
        private string _HttpContext = "MS_HttpContext";
        private string _RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private string _OwinContext = "MS_OwinContext";

        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            var properties = context.ActionContext.Request.Properties;
            string parameter = string.Empty;

            //try
            //{
            //    parameter = Newtonsoft.Json.JsonConvert.SerializeObject(context.ActionContext.ActionArguments);
            //}
            //catch { }

            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            fwBll.LogAction(
                context.ActionContext.ControllerContext.GetAuthUserId(),
                context.ActionContext.ControllerContext.ControllerDescriptor.ControllerName,
                context.ActionContext.ActionDescriptor.ActionName,
                parameter,
                GetBrowserInfo(context.Request),
                GetClientIpString(context.Request)
            );
        }


        private string GetClientIpString(HttpRequestMessage request)
        {
            //Web-hosting
            if (request.Properties.ContainsKey(_HttpContext))
            {
                dynamic ctx = request.Properties[_HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }
            //Self-hosting
            if (request.Properties.ContainsKey(_RemoteEndpointMessage))
            {
                dynamic remoteEndpoint = request.Properties[_RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }
            //Owin-hosting
            if (request.Properties.ContainsKey(_OwinContext))
            {
                dynamic ctx = request.Properties[_OwinContext];
                if (ctx != null)
                {
                    return ctx.Request.RemoteIpAddress;
                }
            }
            if (System.Web.HttpContext.Current != null)
            {
                return System.Web.HttpContext.Current.Request.UserHostAddress;
            }
            // Always return all zeroes for any failure
            return "0.0.0.0";
        }

        private string GetBrowserInfo(HttpRequestMessage request)
        {
            ////Web-hosting
            //if (request.Properties.ContainsKey(_HttpContext))
            //{
            //    dynamic ctx = request.Properties[_HttpContext];
            //    if (ctx != null)
            //    {
            //        return ctx.Request.Header;
            //    }
            //}
            ////Self-hosting
            //if (request.Properties.ContainsKey(_RemoteEndpointMessage))
            //{
            //    dynamic remoteEndpoint = request.Properties[_RemoteEndpointMessage];
            //    if (remoteEndpoint != null)
            //    {
            //        return remoteEndpoint.Address;
            //    }
            //}
            //Owin-hosting
            if (request.Properties.ContainsKey(_OwinContext))
            {
                dynamic ctx = request.Properties[_OwinContext];
                if (ctx != null)
                {
                    return ctx.Request.Headers.Get("User-Agent");
                }
            }
            if (System.Web.HttpContext.Current != null)
            {
                return System.Web.HttpContext.Current.Request.Headers.Get("User-Agent");
            }
            // Always return all zeroes for any failure
            return string.Empty;
        }
    }
}