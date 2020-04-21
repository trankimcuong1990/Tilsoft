using Microsoft.AspNet.Identity.EntityFramework;
using System;
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
    [RoutePrefix("api/employeemng")]
    public class EmployeeMngController : ApiController
    {
        private string moduleCode = "EmployeeMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.EmployeeMng.Executor, Module.EmployeeMng");
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
            Object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out int totalRows, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });
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

            //executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            System.Collections.Hashtable input = new System.Collections.Hashtable
            {
                ["ID"] = id
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdata", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getsensitivedata")]
        public IHttpActionResult GetSensitiveData(int id, string u, string p)
        {
            // authentication 1st level (user have to logged in)
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            // authentication 2nd level (verify account again)
            try
            {
                IdentityUser user = null;
                using (AuthRepository _repo = new AuthRepository())
                {
                    //_repo.RetrieveHash();
                    user = _repo.FindUser(u, p);
                }
                if (user == null)
                {
                    throw new Exception("Not authorized!");
                }
                else
                {
                    if (ControllerContext.GetAuthUserId() != fwBll.GetUserID(user.UserName))
                    {
                        throw new Exception("Not authorized!");
                    }
                }
            }
            catch
            {
                return Ok(new Library.DTO.ReturnData<object>() { Data = null, Message = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Not authorized!" } });
            }

            // everything is ok, go ahead and return sensitive data
            System.Collections.Hashtable input = new System.Collections.Hashtable
            {
                ["id"] = id
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsensitivedata", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
        {
            // authentication
            BLL.Framework fwBll = new BLL.Framework();
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

            //executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
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
            executor.DeleteData(ControllerContext.GetAuthUserId(), id, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        //
        // CUSTOM FUNCTION
        //
        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchFilterData()
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsearchfilter", null, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("detail")]
        public IHttpActionResult GetDetail(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            //executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            System.Collections.Hashtable input = new System.Collections.Hashtable
            {
                ["ID"] = id
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdetail", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("empdetail")]
        public IHttpActionResult GetEmpDetail(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            //executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            System.Collections.Hashtable input = new System.Collections.Hashtable
            {
                ["ID"] = id
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getempdetail", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getoverviewdata")]
        public IHttpActionResult GetOverviewData(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable input = new System.Collections.Hashtable
            {
                ["id"] = id
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getoverviewdata", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getoverviewsensitivedata")]
        public IHttpActionResult GetOverviewSensitiveData(int id, string u, string p)
        {
            // authentication 1st level (user have to logged in)
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            // authentication 2nd level (verify account again)
            try
            {
                IdentityUser user = null;
                using (AuthRepository _repo = new AuthRepository())
                {
                    //_repo.RetrieveHash();
                    user = _repo.FindUser(u, p);
                }
                if (user == null)
                {
                    throw new Exception("Not authorized!");
                }
                else
                {
                    if (ControllerContext.GetAuthUserId() != fwBll.GetUserID(user.UserName))
                    {
                        throw new Exception("Not authorized!");
                    }
                }
            }
            catch
            {
                return Ok(new Library.DTO.ReturnData<object>() { Data = null, Message = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Not authorized!" } });
            }

            // everything is ok, go ahead and return sensitive data
            System.Collections.Hashtable input = new System.Collections.Hashtable
            {
                ["id"] = id
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getoverviewsensitivedata", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }


        //[HttpPost]
        //[Route("restore")]
        //public IHttpActionResult Restore(int id)
        //{
        //    //authentication
        //    if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
        //    {
        //        return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
        //    }

        //    System.Collections.Hashtable input = new System.Collections.Hashtable
        //    {
        //        ["id"] = id
        //    };
        //    executor.CustomFunction(ControllerContext.GetAuthUserId(), "restore", input, out Library.DTO.Notification notification);
        //    return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        //}

        [HttpPost]
        [Route("restore")]
        public IHttpActionResult Restore(int id)
        {

            //authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable input = new System.Collections.Hashtable
            {
                ["id"] = id
            };
            executor.CustomFunction(ControllerContext.GetAuthUserId(), "restore", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }
        [HttpPost]
        [Route("deactiveaccount")]
        public IHttpActionResult DeactiveAccount(int id, string hasLeftDate)
        {
            //authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable input = new System.Collections.Hashtable
            {
                ["id"] = id,
                ["hasLeftDate"] = hasLeftDate
            };
            executor.CustomFunction(ControllerContext.GetAuthUserId(), "deactiveaccount", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("exportexcelemployee")]
        public IHttpActionResult ExportExcelEmployee(DTO.Search searchInput)
        {
            //authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "exportexcelemployee", searchInput.Filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}                                                                       