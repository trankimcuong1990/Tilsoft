using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using TilsoftService.Helper;
using System.Collections;
using System.IO;
using System.Collections.Specialized;
using System.Text;

namespace TilsoftService.Controllers
{
    [Authorize]
    [RoutePrefix("api/warehouseconnect")]
    public class WarehouseConnectMngController : ApiController
    {
        private string moduleCode = "WarehouseConnectMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.WarehouseConnectMng.Executor, Module.WarehouseConnectMng");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();        

        [HttpGet]
        [Route("freetosale")]
        [AllowAnonymous]
        public IHttpActionResult GetFreeToSale(string id) // id: asp net id string
        {
            try
            {
                IdentityUser user = null;
                using (AuthRepository _repo = new AuthRepository())
                {
                    //_repo.RetrieveHash();
                    user = _repo.FinByIdentifier(id);
                    if (!fwBll.CanPerformAction(fwBll.GetUserID(user.UserName), moduleCode, Library.DTO.ModuleAction.CanRead))
                    {
                        user = null;
                    }
                }
                if (user == null) 
                {
                    throw new Exception("Not authorized!");
                }

                //// validate setting for wex connection
                //if(!System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("wex_api_url"))
                //    throw new Exception("Api url is missing!");

                //if (!System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("wex_api_username"))
                //    throw new Exception("Api username is missing!");

                //if (!System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("wex_api_password"))
                //    throw new Exception("Api password is missing!");

                //string jsonText = getWEXJsonData();
                string jsonText = string.Empty;
                Library.DTO.Notification notification;
                Hashtable param = new Hashtable();
                param["wexdata"] = jsonText;
                object result = executor.CustomFunction(-1, "getfreetosale", param, out notification);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(Library.Helper.GetInnerException(ex).Message);
            }
        }

        [HttpGet]
        [Route("downloadfile")]
        [AllowAnonymous]
        public IHttpActionResult DownloadFile(string id) // id: asp net id string
        {
            try
            {
                IdentityUser user = null;
                int userId = 0;
                int companyId = 0;
                using (AuthRepository _repo = new AuthRepository())
                {
                    //_repo.RetrieveHash();
                    user = _repo.FinByIdentifier(id);
                    userId = fwBll.GetUserID(user.UserName);
                    if (!fwBll.CanPerformAction(userId, moduleCode, Library.DTO.ModuleAction.CanRead))
                    {
                        user = null;
                    }
                }
                if (user == null)
                {
                    throw new Exception("Not authorized!");
                }

                //// validate setting for wex connection
                //if (!System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("wex_api_url"))
                //    throw new Exception("Api url is missing!");

                //if (!System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("wex_api_username"))
                //    throw new Exception("Api username is missing!");

                //if (!System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("wex_api_password"))
                //    throw new Exception("Api password is missing!");

                //string jsonText = getWEXJsonData();
                string jsonText = string.Empty;
                Library.DTO.Notification notification;
                Hashtable param = new Hashtable();
                param["wexdata"] = jsonText;
                //try
                //{
                //    companyId = fwBll.GetCompanyID(userId).Value;
                //}
                //catch
                //{
                //    companyId = 0;
                //}

                //switch (companyId)
                //{
                //    case 4: // eurofar
                //    case 11: // tuinmeubel
                //        param["exporterIdentifier"] = "EurofarExporter";
                //        break;

                //    case 2: // au viet soft
                //        param["exporterIdentifier"] = "EurofarStockExporter";
                //        break;

                //    default:
                //        param["exporterIdentifier"] = "TradeMaxExporter";
                //        break;
                //}
                param["exporterIdentifier"] = "EurofarStockExporter";

                object result = executor.CustomFunction(-1, "getfreetosalecsv", param, out notification);
                Hashtable output = (Hashtable)result;
                var dataStream = new MemoryStream((byte[])output["data"]);
                return new FileResult(dataStream, Request, param["exporterIdentifier"].ToString() + output["ext"].ToString());
            }
            catch (Exception ex)
            {
                return Ok(Library.Helper.GetInnerException(ex).Message);
            }
        }

        private string getWEXJsonData()
        {
            string wexTokenURL = System.Configuration.ConfigurationManager.AppSettings["wex_api_url"].ToString() + "token/";
            string wexAPIURL = System.Configuration.ConfigurationManager.AppSettings["wex_api_url"].ToString() + "eurofarstock/client/FREETOSALE";
            string wexAPIUserName = System.Configuration.ConfigurationManager.AppSettings["wex_api_username"].ToString();
            string wexAPIPassword = System.Configuration.ConfigurationManager.AppSettings["wex_api_password"].ToString();

            string jsonText = string.Empty;
            try
            {
                using (WebClient client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["username"] = wexAPIUserName;
                    values["password"] = wexAPIPassword;
                    var response = client.UploadValues(wexTokenURL, values);
                    var responseString = Encoding.Default.GetString(response);

                    var definition = new { Token = "" };
                    var token = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(responseString, definition);
                    client.Headers.Add(HttpRequestHeader.Authorization, "JWT " + token.Token);
                    jsonText = client.DownloadString(wexAPIURL);
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("Can not access WEX api! (Detail: " + Library.Helper.GetInnerException(ex).Message + ")");
                jsonText = string.Empty;
            }

            return jsonText;
        }
    }
}


public class FileResult : IHttpActionResult
{
    MemoryStream fileStuff;
    string fileName;
    HttpRequestMessage httpRequestMessage;
    HttpResponseMessage httpResponseMessage;
    public FileResult(MemoryStream data, HttpRequestMessage request, string filename)
    {
        fileStuff = data;
        httpRequestMessage = request;
        fileName = filename;
    }
    public System.Threading.Tasks.Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
    {
        httpResponseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.OK);
        httpResponseMessage.Content = new StreamContent(fileStuff);
        httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
        httpResponseMessage.Content.Headers.ContentDisposition.FileName = fileName;
        httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
        return System.Threading.Tasks.Task.FromResult(httpResponseMessage);
    }
}