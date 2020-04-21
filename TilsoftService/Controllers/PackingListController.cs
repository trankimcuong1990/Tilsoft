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
    [RoutePrefix("api/PackingList")]
    public class PackingListController : ApiController
    {

        private string moduleCode = "PackingListMng";

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

            BLL.PackingListMng bll = new BLL.PackingListMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.PackingListMng.PackingListSearchResult> searchResult = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.PackingListMng.PackingListSearchResult>>() { Data = searchResult, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id, int purchasingInvoiceID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.PackingListMng bll = new BLL.PackingListMng();
            Library.DTO.Notification notification;
            DTO.PackingListMng.DataContainer PackingList = bll.GetDataContainer(id,purchasingInvoiceID, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.PackingListMng.DataContainer>() { Data = PackingList, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.PackingListMng.PackingList dtoItem)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
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

            // validation            
            if (!Helper.CommonHelper.ValidateDTO<DTO.PackingListMng.PackingList>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.PackingListMng.PackingList>() { Data = dtoItem, Message = notification });
            }

            // continue processing
            BLL.PackingListMng bll = new BLL.PackingListMng();
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);

            
            return Ok(new Library.DTO.ReturnData<DTO.PackingListMng.PackingList>() { Data = dtoItem, Message = notification });
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

            BLL.PackingListMng bll = new BLL.PackingListMng();
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("initinfos")]
        public IHttpActionResult GetInitInfos(DTO.Search searchInput)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.PackingListMng bll = new BLL.PackingListMng();
            Library.DTO.Notification notification;

            int totalRows = 0;
            IEnumerable<DTO.PackingListMng.InitInfo> initInfos = bll.GetInitInfos(searchInput.Filters, ControllerContext.GetAuthUserId(), out totalRows, out notification);
            
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.PackingListMng.InitInfo>>() { Data = initInfos, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("getreport")]
        public IHttpActionResult GetReportData(int packingListID, int template)
        {
            Library.DTO.Notification notification;
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.PackingListMng bll = new BLL.PackingListMng();
            //int? companyID = fwBll.GetCompanyID(ControllerContext.GetAuthUserId());

            string data = "";
            if (template == 1)
            {
                data = bll.GetOrangePiePrintout(packingListID, out notification);
            }
            else {
                data = bll.GetReportData(packingListID, out notification);                
            }            
            return Ok(new Library.DTO.ReturnData<string>() { Data = data, Message = notification });
        }


        [HttpPost]
        [Route("getreportbycontainer")]
        public IHttpActionResult GetReportDataByContainer(int packingListID, int template)
        {
            Library.DTO.Notification notification;
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.PackingListMng bll = new BLL.PackingListMng();
            //int? companyID = fwBll.GetCompanyID(ControllerContext.GetAuthUserId());

            string data = "";
            if (template == 1)
            {
                data = bll.GetOrangePineReportDataByContainer(packingListID, out notification);
            }
            else
            {
                data = bll.GetReportDataByContainer(packingListID, out notification);
            }
            return Ok(new Library.DTO.ReturnData<string>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("printpackinglistpdf")]
        public IHttpActionResult PrintPackingListPDF(int id, int template)
        {
            Library.DTO.Notification notification;
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.PackingListMng bll = new BLL.PackingListMng();
            string dataFileName = bll.PrintPackingListPDF(id, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

    }
}
