using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [RoutePrefix("api/file")]
    public class FileController : ApiController
    {
        [Route("upload")]
        [HttpPost]
        public async Task<HttpResponseMessage> Upload()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string tempFolder = HttpContext.Current.Server.MapPath("~/"+System.Configuration.ConfigurationManager.AppSettings["TempUrl"]);
            string userFolder = AuthHelper.GetCurrentUserFolder(ControllerContext);
            var provider = new MultipartFormDataStreamProvider(tempFolder);

            try
            {
                // Read the form data, put file to the tempFolder - the file is now has the temp name which is unreadable
                await Request.Content.ReadAsMultipartAsync(provider);

                // Move the file to the user folder and rename it to the original name
                foreach (MultipartFileData file in provider.FileData)
                {
                    File.Move(file.LocalFileName, userFolder + @"\" + Helper.CommonHelper.GetUniqueFileName(file.Headers.ContentDisposition.FileName.Replace("\"", ""), userFolder));
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [Route("browse")]
        public IHttpActionResult Browse()
        {
            // authentication
            BLL.Framework bll = new BLL.Framework();
            Library.DTO.Notification notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int totalRows = 0;
            string userFolder = AuthHelper.GetCurrentUserFolder(ControllerContext);
            List<Models.FileViewModel> result = new List<Models.FileViewModel>();
            try
            {
                foreach (string fileName in Directory.GetFiles(userFolder))
                {
                    FileInfo info = new FileInfo(fileName);

                    // delete old file < 1 day
                    if (DateTime.Compare(info.CreationTime.AddDays(1), DateTime.Now) < 0)
                    {
                        try
                        {
                            File.Delete(info.FullName);
                        }
                        catch { }
                    }
                    else
                    {
                        result.Add(new Models.FileViewModel()
                        {
                            FileName = info.Name,
                            FileExtension = info.Extension,
                            FriendlyName = info.Name,
                            URL = System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"] + System.Configuration.ConfigurationManager.AppSettings["TempUrl"] + Helper.AuthHelper.GetAuthUserId(ControllerContext).ToString() + "/" + info.Name
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return Ok(new Library.DTO.ReturnData<IEnumerable<Models.FileViewModel>>() { Data = result, Message = notification, TotalRows = totalRows });
        }

        [Route("clear")]
        public IHttpActionResult Clear()
        {
            // authentication
            BLL.Framework bll = new BLL.Framework();
            Library.DTO.Notification notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int totalRows = 0;
            string userFolder = AuthHelper.GetCurrentUserFolder(ControllerContext);
            List<Models.FileViewModel> result = new List<Models.FileViewModel>();
            try
            {
                foreach (string fileName in Directory.GetFiles(userFolder))
                {
                    FileInfo info = new FileInfo(fileName);

                    // delete all file
                    try
                    {
                        File.Delete(info.FullName);
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return Ok(new Library.DTO.ReturnData<IEnumerable<Models.FileViewModel>>() { Data = result, Message = notification, TotalRows = totalRows });
        }
    }
}
