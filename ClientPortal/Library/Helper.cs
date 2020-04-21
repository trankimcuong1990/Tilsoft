using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientPortal.Library
{
    public static class Helper
    {
        public const string COOKIE_BEARER_TOKEN = "avs-user-token";
        public const string COOKIE_USER_INFO = "avs-user-info";

        //public static string GetBaseUrl(HttpRequestBase request)
        //{
        //    return request.Url.Scheme + "://" + request.Url.Authority + request.ApplicationPath.TrimEnd('/') + "/";
        //}
    }
}