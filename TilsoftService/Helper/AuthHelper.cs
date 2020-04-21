using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace TilsoftService.Helper
{
    public static class AuthHelper
    {
        public static int GetAuthUserId(this HttpControllerContext context)
        {
            int returnVal = 0;

            if (int.TryParse(context.Request.GetOwinContext().Authentication.User.Claims.FirstOrDefault(o => o.Type == "subid").Value, out returnVal))
                return returnVal;

            return 0;
        }

        public static string GetAuthUserName(this HttpControllerContext context)
        {
            return context.Request.GetOwinContext().Authentication.User.Claims.FirstOrDefault(o => o.Type == "sub").Value;
        }
        public static string GetAuthUseCompanyID(this HttpControllerContext context)
        {
            return context.Request.GetOwinContext().Authentication.User.Claims.FirstOrDefault(o => o.Type == "companyid").Value;
        }
        public static string GetAuthUserBranchID(this HttpControllerContext context)
        {
            return context.Request.GetOwinContext().Authentication.User.Claims.FirstOrDefault(o => o.Type == "branchid").Value;
        }
        public static string GetAuthUserFactoryID(this HttpControllerContext context)
        {
            return context.Request.GetOwinContext().Authentication.User.Claims.FirstOrDefault(o => o.Type == "factoryid").Value;
        }

        public static int GetAuthUserClientID(this HttpControllerContext context)
        {
            int returnVal = 0;

            if (int.TryParse(context.Request.GetOwinContext().Authentication.User.Claims.FirstOrDefault(o => o.Type == "clientid").Value, out returnVal))
                return returnVal;

            return 0;
        }

        public static string GetCurrentUserFolder(this HttpControllerContext context)
        {
            int userId = context.GetAuthUserId();
            if (userId == 0)
            {
                throw new Exception("Access denied");
            }
            string userFolder = HttpContext.Current.Server.MapPath("~/" + System.Configuration.ConfigurationManager.AppSettings["TempUrl"]) + userId.ToString() + @"\";
            if (!Directory.Exists(userFolder))
            {
                Directory.CreateDirectory(userFolder);
            }

            return userFolder;
        }


        public static string PostRequestAPI(string iBaseUrl, string iPostData, string iAccept, string iContentType, string iAuthorization, out string errMsg)
        {
            string responseString = string.Empty;
            errMsg = string.Empty;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(iBaseUrl);
                req.Method = "POST";
                req.ContentType = iContentType;
                req.Accept = iAccept;
                req.ContentType = iContentType;
                req.Timeout = 10000;
                if (!string.IsNullOrEmpty(iAuthorization))
                    req.Headers.Add("Authorization:" + iAuthorization);

                // add post data
                string data = iPostData;
                byte[] dataStream = System.Text.Encoding.UTF8.GetBytes(data);
                req.ContentLength = dataStream.Length;
                Stream newStream = req.GetRequestStream();
                newStream.Write(dataStream, 0, dataStream.Length);
                newStream.Close();

                // send request and get response
                var response = req.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream());
                responseString = sr.ReadToEnd();
                sr.Close();
            }
            catch
            {
                errMsg = "Unable to connect to the remote server!";
                responseString = string.Empty;
            }

            return responseString;
        }

        public static bool AuthorizeAPI(string userName, string password, out string errMsg)
        {
            System.Web.Mvc.UrlHelper Url = new System.Web.Mvc.UrlHelper();
            string backendServiceUrl = System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"].ToString();
            string providedLoginInfo = "grant_type=password&username=" + userName + "&password=" + Url.Encode(password);
            
            string token = PostRequestAPI(backendServiceUrl + "token", providedLoginInfo, "application/json", "application-x-www-form-urlencode", string.Empty, out errMsg);
            return !string.IsNullOrEmpty(token);
        }
    }
}