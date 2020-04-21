using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using Library.DTO;
using TilsoftService.Helper;
using System.Collections;

namespace TilsoftService.Controllers
{
    [Authorize]
    [Lib.LogFilterAtrribute]
    [RoutePrefix("api/SpecificationOfProductMng")]
    public class SpecificationOfProductMngController : ApiController
    {
        private readonly string moduleCode = "SpecificationOfProductMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.SpecificationOfProductMng.Executor, Module.SpecificationOfProductMng");
        private readonly Module.Framework.BLL fwBll = new Module.Framework.BLL();


        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            Notification notification;
            var data = executor.GetInitData(ControllerContext.GetAuthUserId(), out notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getdataspec")]
        public IHttpActionResult GetDataSpecification(int id, int? sampleProductID, int? productID)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable();
            filters["id"] = id;
            filters["sampleProductID"] = sampleProductID;
            filters["productID"] = productID;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdataspecification", filters, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            searchInput.Filters.Add("userId", ControllerContext.GetAuthUserId());
            var data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out int totalRows, out Notification notification);
            return Ok(new ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });

        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
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
        [Route("getreportdata")]
        public IHttpActionResult ExportExcel(int productSpecificationID)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["productSpecificationID"] = productSpecificationID;

            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getreportdata", filters, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("quicksearchsample")]
        public IHttpActionResult GetMaterialOption(string param, int factoryID)
        {
            Hashtable filters = new Hashtable();
            filters["searchQuery"] = param;
            filters["factoryID"] = factoryID;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "searchsample", filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getspecofproductlist")]

        public IHttpActionResult GetsListSpec(int? modelID)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Notification();
            System.Collections.Hashtable input = new Hashtable()
            {
                ["modelID"] = modelID
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getspecofproductlist", input, out notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("copyspecofproduct")]

        public IHttpActionResult CoppySpecOfProduct(int? productSpecificationID)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Notification();
            System.Collections.Hashtable input = new Hashtable()
            {
                ["productSpecificationID"] = productSpecificationID
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "copyspecofproduct", input, out notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }
    }
}