using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using Library.DTO;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [Lib.LogFilterAtrribute]
    [RoutePrefix("api/showroomRpt")]
    public class ShowroomRptController : ApiController
    {

        const string ModuleCode = "ShowroomRpt";
        readonly Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.ShowroomRpt.Executor, Module.ShowroomRpt");
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
        public IHttpActionResult ExportExcel(string factorywarehouseID, DTO.Search input)
        {
            //authentication
            if (!this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanPrint))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            this.SetModuleIdenfitier(this.executor);

            Library.DTO.Notification notification = new Library.DTO.Notification();

            //System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            //inputs["listFactoryWarehouse"] = factorywarehouseID;

            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "exportexcel", input.Filters, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            //authentication
            if (!this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanPrint))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            this.SetModuleIdenfitier(this.executor);

            object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out Notification notification);

            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("exportexcelwithoutimage")]
        public IHttpActionResult ExportExcelWithoutImage(string factorywarehouseID, DTO.Search input)
        {
            //authentication
            if (!this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanPrint))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            this.SetModuleIdenfitier(this.executor);

            Library.DTO.Notification notification = new Library.DTO.Notification();

            //System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            //inputs["listFactoryWarehouse"] = factorywarehouseID;

            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "exportexcelwithoutimage", input.Filters, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
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