using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [RoutePrefix("api/bifaCompanyMng")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class BifaCompanyMngController : ApiController
    {
        private string moduleCode = "BifaCompanyMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.BifaCompanyMng.Executor, Module.BifaCompanyMng");
        private Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchFilterData()
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsearchfilter", null, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            int totalRows = 0;
            System.Object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<System.Object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.GetData(ControllerContext.GetAuthUserId(), id, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
        {
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

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
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = executor.DeleteData(ControllerContext.GetAuthUserId(), id, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("approve")]
        public IHttpActionResult Approve(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            executor.Approve(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("reset")]
        public IHttpActionResult Reset(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            executor.Reset(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("updateBifaCity")]
        public IHttpActionResult UpdateBifaCity(int id, object dtoItem)
        {
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaCityMng", Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaCityMng", Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;
            filters["view"] = dtoItem;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateBifaCity", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deleteBifaCity")]
        public IHttpActionResult DeleteBifaCity(int id)
        {
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaCityMng", Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "deleteBifaCity", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updateBifaIndustry")]
        public IHttpActionResult UpdateBifaIndustry(int id, object dtoItem)
        {
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaIndustryMng", Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaIndustryMng", Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;
            filters["view"] = dtoItem;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateBifaIndustry", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deleteBifaIndustry")]
        public IHttpActionResult DeleteBifaIndustry(int id)
        {
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaIndustryMng", Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "deleteBifaIndustry", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getBifaAddress")]
        public IHttpActionResult GetBifaAddress(int id)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaAddressMng", Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getBifaAddress", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updateBifaAddress")]
        public IHttpActionResult UpdateBifaAddress(int id, object dtoItem)
        {
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaAddressMng", Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaAddressMng", Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;
            filters["view"] = dtoItem;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateBifaAddress", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deleteBifaAddress")]
        public IHttpActionResult DeleteBifaAddress(int id)
        {
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaAddressMng", Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "deleteBifaAddress", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updateBifaClub")]
        public IHttpActionResult UpdateBifaClub(int id, object dtoItem)
        {
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaClubMng", Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaClubMng", Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;
            filters["view"] = dtoItem;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateBifaClub", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deleteBifaClub")]
        public IHttpActionResult DeleteBifaClub(int id)
        {
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaClubMng", Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "deleteBifaClub", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updateBifaClubMember")]
        public IHttpActionResult UpdateBifaClubMember(int id, object dtoItem)
        {
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaClubMemberMng", Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaClubMemberMng", Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;
            filters["view"] = dtoItem;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateBifaClubMember", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deleteBifaClubMember")]
        public IHttpActionResult DeleteBifaClubMember(int id)
        {
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaClubMemberMng", Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "deleteBifaClubMember", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updateBifaEmailAddress")]
        public IHttpActionResult UpdateBifaEmailAddress(int id, object dtoItem)
        {
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaEmailAddressMng", Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaEmailAddressMng", Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;
            filters["view"] = dtoItem;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateBifaEmailAddress", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deleteBifaEmailAddress")]
        public IHttpActionResult DeleteBifaEmailAddress(int id)
        {
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaEmailAddressMng", Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "deleteBifaEmailAddress", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updateTelephone")]
        public IHttpActionResult UpdateTelephone(int id, object dtoItem)
        {
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaTelephoneMng", Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaTelephoneMng", Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;
            filters["view"] = dtoItem;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateTelephone", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deleteTelephone")]
        public IHttpActionResult DeleteTelephone(int id)
        {
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaTelephoneMng", Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "deleteTelephone", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updatePerson")]
        public IHttpActionResult UpdatePerson(int id, object dtoItem)
        {
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaPersonMng", Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaPersonMng", Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;
            filters["view"] = dtoItem;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updatePerson", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deletePerson")]
        public IHttpActionResult DeletePerson(int id)
        {
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaPersonMng", Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "deletePerson", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updateEvent")]
        public IHttpActionResult UpdateEvent(int id, object dtoItem)
        {
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaEventMng", Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaEventMng", Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;
            filters["view"] = dtoItem;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateEvent", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deleteEvent")]
        public IHttpActionResult DeleteEvent(int id)
        {
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "BifaEventMng", Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new System.Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "deleteEvent", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
