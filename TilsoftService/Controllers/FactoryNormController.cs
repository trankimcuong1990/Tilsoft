using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/FactoryNorm")]
    public class FactoryNormController : ApiController
    {
        private string moduleCode = "FactoryNormMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.FactoryNorm.Executor, Module.FactoryNorm");
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
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
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
            //set indenfier is tempFolder which to use to save image
            executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            //update data
            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out notification);
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
            Library.DTO.Notification notification;
            executor.DeleteData(ControllerContext.GetAuthUserId(), id, out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("createnewfactoryfinishedproduct")]
        public IHttpActionResult CreateNewFactoryFinishedProduct(string code, string name)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["factoryFinishedProductUD"] = code;
            filters["factoryFinishedProductNM"] = name;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "CreateNewFactoryFinishedProduct", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }


        [HttpPost]
        [Route("CreateComponentNorm")]
        public IHttpActionResult CreateComponentNorm(int factoryNormID, object dtoItem)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["factoryNormID"] = factoryNormID;
            filters["dtoItem"] = dtoItem;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "CreateComponentNorm", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }


        [HttpPost]
        [Route("CreateSubComponentNorm")]
        public IHttpActionResult CreateSubComponentNorm(int parentFactoryFinishedProductNormID, object dtoItem)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["parentFactoryFinishedProductNormID"] = parentFactoryFinishedProductNormID;
            filters["dtoItem"] = dtoItem;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "CreateSubComponentNorm", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("CreateFactoryMaterialNorm")]
        public IHttpActionResult CreateFactoryMaterialNorm(int factoryFinishedProductNormID, object dtoItem)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["factoryFinishedProductNormID"] = factoryFinishedProductNormID;
            filters["dtoItem"] = dtoItem;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "CreateFactoryMaterialNorm", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }


        [HttpPost]
        [Route("DeleteFinishedProductNorm")]
        public IHttpActionResult DeleteFinishedProductNorm(int id, object dtoItem)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "DeleteFinishedProductNorm", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("DeleteMaterialNorm")]
        public IHttpActionResult DeleteMaterialNorm(int id, object dtoItem)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "DeleteMaterialNorm", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }



    }
}
