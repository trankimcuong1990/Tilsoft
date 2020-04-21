using Library.DTO;
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
    [RoutePrefix("api/offertoclientmng")]
    public class OfferToClientMngController : ApiController
    {
        private string moduleCode = "OfferToClientMng";      
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.OfferToClientMng.Executor, Module.OfferToClientMng");

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

           
            Library.DTO.Notification notification;
            int totalRows = 0;

            object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("excel-searchoffer")]
        public IHttpActionResult GetExcelFOBItemOnlyReportData(DTO.Search searchInput)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.OfferMng bll = new BLL.OfferMng();
            string reportFileName = bll.ExportExcelSearchOffer(searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            //Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            //if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            //{
            //    return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            //}
        
            object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("quicksearchclient")]
        public IHttpActionResult QuickSearchClient(string clientUD, string sortedBy, string sortedDirection, int pageSize, int pageIndex)
        {
            Library.DTO.Notification notification = new Notification();
            notification.Type = NotificationType.Success;
           
            //// authentication
            //if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            //{
            //    return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            //}

            Hashtable filters = new Hashtable();
            filters["clientUD"] = clientUD;
            filters["sortedBy"] = sortedBy;
            filters["sortedDirection"] = sortedDirection;
            filters["pageSize"] = pageSize;
            filters["pageIndex"] = pageIndex;        
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchclient", filters, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification});
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            Library.DTO.Notification notification;
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;          
           
            object data = executor.GetData(ControllerContext.GetAuthUserId(), id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        //
        //customize function
        //
        [HttpPost]
        [Route("get-purchaseorder-data")]
        public IHttpActionResult GetPlanningData(DTO.Search searchInput)
        {
            Hashtable input = searchInput.Filters;
            input["pageSize"] = searchInput.PageSize;
            input["pageIndex"] = searchInput.PageIndex;
            input["sortedBy"] = searchInput.SortedBy;
            input["sortedDirection"] = searchInput.SortedDirection;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-purchaseorder-data", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });

        }

        [HttpPost]
        [Route("get-offerSeason")]
        public IHttpActionResult GetPlanningData(int clientID, string season)
        {
            Hashtable input = new Hashtable();
            input["clientID"] = clientID;
            input["season"] = season;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getofferseason", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });

        }
        [HttpPost]
        [Route("getsupport")]
        public IHttpActionResult GetSupportData()
        {
            BLL.OfferMng bll = new BLL.OfferMng();
            Library.DTO.Notification notification;
            DTO.OfferMng.SupportDataContainer result = bll.GetSupportData(out notification);

            return Ok(new Library.DTO.ReturnData<DTO.OfferMng.SupportDataContainer>() { Data = result, Message = notification, TotalRows = 0 });
        }
        [HttpPost]
        [Route("quicksearchOfferLine")]
        public IHttpActionResult QuickSearchOfferLine(DTO.Search searchInput)
        {
            Library.DTO.Notification notification = new Notification();
            notification.Type = NotificationType.Success;
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = searchInput.Filters;         
            filters["pageSize"] = searchInput.PageSize;
            filters["pageIndex"] = searchInput.PageIndex;
            filters["sortedBy"] = searchInput.SortedBy;
            filters["sortedDirection"] = searchInput.SortedDirection;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchofferline", filters, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Notification();
            notification.Type = NotificationType.Success;

            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }
        
        [HttpPost]
        [Route("update-clientinfo")]
        public IHttpActionResult UpdateClientInfo(int offerID)
        {
            Notification notification;
          
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable();
            filters["id"] = offerID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "update-clientinfo", filters, out notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
          
            Library.DTO.Notification notification;
            executor.DeleteData(ControllerContext.GetAuthUserId(), id, out notification);

            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });           
        }

        [HttpPost]
        [Route("getprintoffer5")]
        public IHttpActionResult GetPrintDataOffer5(int offerID, bool IsSendEmail, bool IsGetFullSizeImage, int imageOption)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable();
            filters["offerID"] = offerID;
            filters["IsSendEmail"] = IsSendEmail;
            filters["IsGetFullSizeImage"] = IsGetFullSizeImage;
            filters["imageOption"] = imageOption;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getprintoffer5", filters, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getprintofferpp2013")]
        public IHttpActionResult GetPrintDataOfferPP2013(int id)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable();
            filters["id"] = id;         
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getprintofferpp2013", filters, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getexportexcel")]
        public IHttpActionResult GetExportNewVersion(int offerID)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable();
            filters["id"] = offerID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getexportexcel", filters, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-fobitemonly")]
        public IHttpActionResult GetExcelFOBItemOnlyReportData(int offerID)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable filters = new Hashtable();
            filters["id"] = offerID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-fobitemonly", filters, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}