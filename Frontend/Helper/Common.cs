using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Frontend.Helper
{
    public static class Common
    {
        public static string GetCurrentSeason()
        {
            int curYear = DateTime.Now.Year;
            if (DateTime.Now.Month >= 9)
            {
                return curYear.ToString() + "/" + (curYear + 1).ToString();
            }
            return (curYear-1).ToString() + "/" + curYear.ToString();
        }

        public static string SendRequest(string url, string method, string postedData, string token, string jsonToken)
        {
            string jsonText = string.Empty;
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.Accept, "application/json");
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    client.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token);
                    client.Encoding = System.Text.Encoding.UTF8;
                    var jsonData = JObject.Parse(client.UploadString(url, method, postedData)).SelectToken(jsonToken);
                    return jsonData.ToString();
                }
            }
            catch (Exception ex) { }

            return string.Empty;
        }
    }
}