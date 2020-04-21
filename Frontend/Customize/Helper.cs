using Frontend.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Customize
{
    public static class Helper
    {
        public static HtmlString CustomValidationSummary(this HtmlHelper helper, string validationMessage = "")
        {
            string retVal = "";
            if (helper.ViewData.ModelState.IsValid) return new HtmlString("");
            if (!String.IsNullOrEmpty(validationMessage)) retVal += helper.Encode(validationMessage);

            foreach (var key in helper.ViewData.ModelState.Keys)
            {
                foreach (var err in helper.ViewData.ModelState[key].Errors)
                    retVal += "<div class='alert alert-warning fade in'><button data-dismiss='alert' class='close' style='font-size: 14px;'>x</button><i class='fa-fw fa fa-warning'></i><strong>Warning</strong> " + helper.Encode(err.ErrorMessage) + "</div>";
            }
            return new HtmlString(retVal.ToString());
        }

        //public static string PostRequestAPI(string iBaseUrl, string iPostData, string iAccept, string iContentType, string iAuthorization, out string errMsg, out string errDescription)
        //{
        //    string responseString = string.Empty;
        //    errMsg = string.Empty;
        //    errDescription = string.Empty;
        //    try
        //    {
        //        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(iBaseUrl);
        //        req.Method = "POST";
        //        req.ContentType = iContentType;
        //        req.Accept = iAccept;
        //        req.ContentType = iContentType;
        //        req.Timeout = 10000;
        //        if (!string.IsNullOrEmpty(iAuthorization))
        //            req.Headers.Add("Authorization:" + iAuthorization);

        //        // add post data
        //        string data = iPostData;
        //        byte[] dataStream = Encoding.UTF8.GetBytes(data);
        //        req.ContentLength = dataStream.Length;
        //        Stream newStream = req.GetRequestStream();
        //        newStream.Write(dataStream, 0, dataStream.Length);
        //        newStream.Close();

        //        // send request and get response
        //        var response = req.GetResponse();
        //        StreamReader sr = new StreamReader(response.GetResponseStream());
        //        responseString = sr.ReadToEnd();
        //        sr.Close();
        //    }
        //    catch(WebException e)
        //    {
        //        errMsg = "Unable to connect to the remote server!";
        //        if (e.Response != null)
        //        {
        //            OAuhErrMsg error = JsonConvert.DeserializeObject<OAuhErrMsg>(ExtractResponseString(e));
        //            errMsg = error.error;
        //            errDescription = error.error_description;
        //        }
        //        responseString = string.Empty;
        //    }

        //    return responseString;
        //}

        private static string ExtractResponseString(WebException webException)
        {
            if (webException == null || webException.Response == null)
                return null;

            var responseStream =
                webException.Response.GetResponseStream() as MemoryStream;

            if (responseStream == null)
                return null;

            var responseBytes = responseStream.ToArray();

            var responseString = Encoding.UTF8.GetString(responseBytes);
            return responseString;
        }

        //
        // api authorization
        //
        public static string GetAPIToken(string apiURL, string username, string password, out string errorMsg)
        {
            string data = string.Empty;
            errorMsg = string.Empty;

            var tokenDataDefinition = new { access_token = "", token_type = "", expires_in = 0 };
            var errorDataDefinition = new { error = "" };
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                    data = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(
                            client.UploadString(apiURL, "grant_type=password&username=" + username + "&password=" + password), 
                            tokenDataDefinition
                        ).access_token;
                }
            }
            catch (WebException wex)
            {
                errorMsg = "Unable to connect to the remote server!";
                if (wex.Response != null)
                {
                    string errorResponseString = ExtractResponseString(wex);
                    if (!string.IsNullOrEmpty(errorResponseString))
                    {
                        errorMsg = JsonConvert.DeserializeAnonymousType(errorResponseString, errorDataDefinition).error;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public static string GetAPIUserData(string apiURL, string token, out string errorMsg)
        {
            string data = string.Empty;
            errorMsg = string.Empty;
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    client.Headers.Add(HttpRequestHeader.Accept, "application/json");
                    client.Headers.Add(HttpRequestHeader.Authorization, "bearer " + token);
                    data = client.UploadString(apiURL, string.Empty);
                }
            }
            catch (WebException wex)
            {
                errorMsg = "Unknow error from api server!";
                if (wex.Response != null)
                {
                    string errorResponseString = ExtractResponseString(wex);
                    if (!string.IsNullOrEmpty(errorResponseString))
                    {
                        errorMsg = errorResponseString;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }
    }
}