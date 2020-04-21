using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/quotation")]
    public class QuotationController : ApiController
    {
        private string moduleCode = "QuotationMng";

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

            BLL.QuotationMng bll = new BLL.QuotationMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.QuotationMng.SearchFormData data = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.QuotationMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("getsasync")]
        public async Task<DTO.QuotationMng.SearchFormData> SearchDataAsync()
        {
            // authentication
            //Module.Framework.BLL fwBll = new Module.Framework.BLL();
            //if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            //{
            //    return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            //}

            BLL.QuotationMng bll = new BLL.QuotationMng();
            DTO.QuotationMng.SearchFormData data = await bll.GetDataWithFilterAsync(ControllerContext.GetAuthUserId(), new Hashtable(), 50, 1, "UpdatedDate", "DESC");
            return data;
        }

        [HttpPost]
        [Route("getsnormal")]
        public IHttpActionResult SearchData()
        {
            // authentication
            //Module.Framework.BLL fwBll = new Module.Framework.BLL();
            //if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            //{
            //    return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            //}

            BLL.QuotationMng bll = new BLL.QuotationMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.QuotationMng.SearchFormData data = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(),new Hashtable(), 50,1,"UpdatedDate", "DESC", out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.QuotationMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.QuotationMng.Quotation dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.QuotationMng.Quotation>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.QuotationMng.Quotation>() { Data = dtoItem, Message = notification });
            }

            // continue processing
            BLL.QuotationMng bll = new BLL.QuotationMng();
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.QuotationMng.Quotation>() { Data = dtoItem, Message = notification });
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

            BLL.QuotationMng bll = new BLL.QuotationMng();
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchFilterData()
        {
            BLL.QuotationMng bll = new BLL.QuotationMng();
            Library.DTO.Notification notification;
            DTO.QuotationMng.SearchFilterData data = bll.GetFilterData(out notification);
            return Ok(new Library.DTO.ReturnData<DTO.QuotationMng.SearchFilterData>() { Data = data, Message = notification, TotalRows = 0 });
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            BLL.QuotationMng bll = new BLL.QuotationMng();
            Library.DTO.Notification notification;
            DTO.QuotationMng.InitFormData data = bll.GetInitData(ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.QuotationMng.InitFormData>() { Data = data, Message = notification, TotalRows = 0 });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id, int factoryID, string season, List<int> orders)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.QuotationMng bll = new BLL.QuotationMng();
            Library.DTO.Notification notification;
            DTO.QuotationMng.EditFormData data = bll.GetData(id, factoryID, season, orders, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.QuotationMng.EditFormData>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getfactoryquotation")]
        public IHttpActionResult GetFactoryQuotation(int id)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.QuotationMng bll = new BLL.QuotationMng();
            string dataFileName = bll.GetFactoryQuotationReportData(id, out notification);
            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("geteurofarquotation")]
        public IHttpActionResult GetEurofarQuotation(int id)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.QuotationMng bll = new BLL.QuotationMng();
            string dataFileName = bll.GetEurofarQuotationReportData(id, out notification);
            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }
    }
}
