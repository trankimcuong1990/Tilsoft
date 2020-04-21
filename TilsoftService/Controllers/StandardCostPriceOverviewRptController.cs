using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;
using Library.DTO;
using System.Collections;

namespace TilsoftService.Controllers
{
    [Authorize]
    [Lib.LogFilterAtrribute]
    [RoutePrefix("api/standardCostPriceOverviewRpt")]
    public class StandardCostPriceOverviewRptController : ApiController
    {
        const string ModuleCode = "StandardCostPriceOverviewRpt";
        readonly Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.StandardCostPriceOverviewRpt.Executor, Module.StandardCostPriceOverviewRpt");
        readonly Module.Framework.BLL framework = new Module.Framework.BLL();


        [HttpPost]
        [Route("gets")]
        public IHttpActionResult GetDataWithFilters(DTO.Search input)
        {
            if (!this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanRead))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            this.SetModuleIdenfitier(this.executor);

            input.Filters.Add("UserID", ControllerContext.GetAuthUserId());
            var data = this.executor.GetDataWithFilter(this.ControllerContext.GetAuthUserId(), input.Filters, input.PageSize, input.PageIndex, input.SortedBy, input.SortedDirection, out int totalRows, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("exportexcel")]
        public IHttpActionResult ExportExcel(DTO.Search input)
        {
            //authentication
            if (!this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanPrint))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            this.SetModuleIdenfitier(this.executor);

            Library.DTO.Notification notification = new Library.DTO.Notification();
            //System.Collections.Hashtable inputs = input.Filters;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "exportexcel", input.Filters, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("getDataForPopupWithFilters")]
        public IHttpActionResult GetDataForPopupWithFilters(DTO.Search filters)
        {
            //authentication
            if (!this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanUpdate))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            this.SetModuleIdenfitier(this.executor);

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdataforpopupưithfilters", filters.Filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("setDefaultFactory")]
        public IHttpActionResult SetDefaultFactory(DTO.Search filters)
        {
            //authentication
            if (!this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanUpdate))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            this.SetModuleIdenfitier(this.executor);

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "setdefaultfactory", filters.Filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updateProductPrice")]
        public IHttpActionResult UpdateProductPrice(object param)
        {
            //authentication
            if (!this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanUpdate))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            this.SetModuleIdenfitier(this.executor);
            Hashtable filters = new Hashtable();
            filters["data"] = param;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateproductprice", filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        private void SetModuleIdenfitier(Library.Base.IExecutor executor)
        {
            if (executor != null)
                executor.identifier = ControllerContext.GetCurrentUserFolder();
        }

        private bool HasNotUpdatePermission(int id, out IHttpActionResult httpActionResult)
        {
            httpActionResult = null;

            if (id == 0 && !this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanCreate))
            {
                httpActionResult = InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                return true;
            }

            if (id > 0 && !this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanUpdate))
            {
                httpActionResult = InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                return true;
            }

            return false;

        }
    }
}