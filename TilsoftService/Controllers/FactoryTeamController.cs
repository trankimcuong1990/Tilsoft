using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;
using System.Collections;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/FactoryTeam")]
    public class FactoryTeamController : ApiController
    {
        private string moduleCode = "FactoryTeamMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.FactoryTeam.Executor, Module.FactoryTeam");
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
        [Route("createfactoryteamstep")]
        public IHttpActionResult CreateFactoryTeamStep(int factoryTeamID, object dtoItem)
        {
            Library.DTO.Notification notification;
            Hashtable filters = new System.Collections.Hashtable();
            filters["factoryTeamID"] = factoryTeamID;
            filters["dtoItem"] = dtoItem;
            object factoryTeamStepID = executor.CustomFunction(ControllerContext.GetAuthUserId(), "CreateFactoryTeamStep", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = factoryTeamStepID, Message = notification });
        }

        [HttpPost]
        [Route("deleteteamstep")]
        public IHttpActionResult DeleteTeamStep(int factoryTeamStepID)
        {
            Library.DTO.Notification notification;
            Hashtable filters = new System.Collections.Hashtable();
            filters["factoryTeamStepID"] = factoryTeamStepID;
            executor.CustomFunction(ControllerContext.GetAuthUserId(), "DeleteTeamStep", filters, out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = factoryTeamStepID, Message = notification });
        }

        [HttpPost]
        [Route("createfactorymaterialgroupteam")]
        public IHttpActionResult CreateMaterialGroupTeam(int factoryTeamID, object dtoItem)
        {
            Library.DTO.Notification notification;
            Hashtable filters = new System.Collections.Hashtable();
            filters["factoryTeamID"] = factoryTeamID;
            filters["dtoItem"] = dtoItem;
            object factoryMaterialGroupTeamID = executor.CustomFunction(ControllerContext.GetAuthUserId(), "CreateMaterialGroupTeam", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = factoryMaterialGroupTeamID, Message = notification });
        }

        [HttpPost]
        [Route("deletefactorymaterialgroupteam")]
        public IHttpActionResult DeleteMaterialGroupTeam(int factoryMaterialGroupTeamID)
        {
            Library.DTO.Notification notification;
            Hashtable filters = new System.Collections.Hashtable();
            filters["factoryMaterialGroupTeamID"] = factoryMaterialGroupTeamID;
            executor.CustomFunction(ControllerContext.GetAuthUserId(), "DeleteMaterialGroupTeam", filters, out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = factoryMaterialGroupTeamID, Message = notification });
        }

    }
}
