using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/user2mng")]
    public class User2MngController : ApiController
    {
        private string moduleCode = "User2Mng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.User2Mng.Executor, Module.User2Mng");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            int totalRows = 0;
            Object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchFilterData()
        {
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsearchfilter", null, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getinitdata", null, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("initaccount")]
        public IHttpActionResult InitAccount(Models.AccountViewModel dtoItem)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                // create new case
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Properties.Resources.NOT_AUTHORIZED;
                return Ok(new Library.DTO.ReturnData<DTO.UserMng.UserProfile>() { Data = null, Message = notification });
            }

            // validation
            if (!Helper.CommonHelper.ValidateDTO<Models.AccountViewModel>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<Models.AccountViewModel>() { Data = dtoItem, Message = notification });
            }

            // create asp.net users
            string aspNetUsersId = string.Empty;
            try
            {
                using (AuthRepository _repo = new AuthRepository())
                {
                    if (dtoItem.Password.Length >= 7)
                    {
                        if (_repo.CheckUsernameOrEmailExists(dtoItem.UserName, dtoItem.Email))
                        {
                            throw new Exception("Username or email address already used by other user!");
                        }

                        aspNetUsersId = _repo.CreateUser(dtoItem.UserName, dtoItem.Password, dtoItem.Email);
                    }
                    else
                    {
                        throw new Exception("Password length must be at least 7 chars");
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return Ok(new Library.DTO.ReturnData<DTO.UserMng.UserProfile>() { Data = null, Message = notification });
            }

            // continue processing
            Hashtable input = new Hashtable();
            input["AspNetUserId"] = aspNetUsersId;
            input["EmployeeID"] = dtoItem.EmployeeID;
            input["IsActive"] = dtoItem.IsActive;
            input["UserGroupID"] = dtoItem.UserGroupID;
            int newID = (int)executor.CustomFunction(ControllerContext.GetAuthUserId(), "initaccount", input, out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = newID, Message = notification });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            object data = executor.GetData(ControllerContext.GetAuthUserId(), id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("changepassword")]
        public IHttpActionResult ChangePassword(Models.PasswordChangeViewModel dtoItem)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

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

                    _repo.ResetPassword(dtoItem.UserName, dtoItem.NewPassword);
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
        [Route("updateaccount")]
        public IHttpActionResult UpdateAccount(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                // create new case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["id"] = id;
            param["data"] = dtoItem;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateaccount", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("updateemployee")]
        public IHttpActionResult UpdateEmployee(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                // create new case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["id"] = id;
            param["data"] = dtoItem;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateemployee", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("updatepermission")]
        public IHttpActionResult UpdatePermission(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                // create new case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["id"] = id;
            param["data"] = dtoItem;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updatepermission", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("updatefactoryaccess")]
        public IHttpActionResult UpdateFactoryAccess(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                // create new case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["id"] = id;
            param["data"] = dtoItem;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updatefactoryaccess", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("forcechangepassword")]
        public IHttpActionResult ForceChangePassword(int id)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                // create new case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["id"] = id;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "forcechangepassword", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            object data = executor.DeleteData(ControllerContext.GetAuthUserId(), id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("restoreuser")]

        public IHttpActionResult RestoreUser(int id)
        {
            //authencation 
            if(!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            Hashtable input = new Hashtable()
            {
                ["id"] = id
            };

            executor.CustomFunction(ControllerContext.GetAuthUserId(), "restoreuser", input, out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("get-api-key")]
        public IHttpActionResult GetAPIKey(int id)
        {
            Library.DTO.Notification notification;
            Hashtable input = new Hashtable()
            {
                ["id"] = id
            };
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-api-key", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }
    }
}
