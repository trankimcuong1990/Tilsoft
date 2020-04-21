using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TilsoftService.Helper;
using System.Collections;

namespace TilsoftService.Controllers
{
    [Authorize]
    [RoutePrefix("api/profile")]
    public class ProfileController : ApiController
    {
        private string moduleCode = "Profile";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.ProfileMng.Executor, Module.ProfileMng");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            Library.DTO.Notification notification;
            object data = executor.GetData(ControllerContext.GetAuthUserId(), ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("changepassword")]
        public IHttpActionResult ChangePassword(Models.PasswordChangeViewModel dtoItem)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            // validation
            if (!Helper.CommonHelper.ValidateDTO<Models.PasswordChangeViewModel>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<Models.PasswordChangeViewModel>() { Data = dtoItem, Message = notification });
            }

            // change password
            try
            {
                using (AuthRepository _repo = new AuthRepository())
                {
                    if (dtoItem.NewPassword.Length < 7)
                    {
                        throw new Exception("Password length must be at least 7 chars");
                    }
                    if (dtoItem.NewPassword != dtoItem.Confirmation)
                    {
                        throw new Exception("Password and confirmation do not match!");
                    }

                    _repo.ResetPassword(ControllerContext.GetAuthUserName(), dtoItem.NewPassword);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return Ok(new Library.DTO.ReturnData<DTO.UserMng.UserProfile>() { Data = null, Message = notification });
            }

            return Ok(new Library.DTO.ReturnData<bool>() { Data = true, Message = notification });
        }

        [HttpPost]
        [Route("updateemployee")]
        public IHttpActionResult UpdateEmployee(object dtoItem)
        {
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["id"] = ControllerContext.GetAuthUserId();
            param["data"] = dtoItem;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateemployee", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("get-api-key")]
        public IHttpActionResult GetAPIKey()
        {
            Library.DTO.Notification notification;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-api-key", new Hashtable(), out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }
    }
}
