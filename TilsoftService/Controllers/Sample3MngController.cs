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
    [RoutePrefix("api/sample3")]
    public class Sample3MngController : ApiController
    {
        private string moduleCode = "Sample3Mng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.Sample3Mng.Executor, Module.Sample3Mng");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        //
        // ORDER 
        //
        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchFilterData()
        {
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsearchfilter", null, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

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
        [Route("getorderdata")]
        public IHttpActionResult GetOrderData(int id, int c, string s)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable param = new Hashtable
            {
                ["id"] = id,
                ["clientId"] = c,
                ["season"] = s ?? string.Empty
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getorderdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getorderoverviewdata")]
        public IHttpActionResult GetOrderOverviewData(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable param = new Hashtable
            {
                ["id"] = id
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getorderoverviewdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updateorderdata")]
        public IHttpActionResult UpdateOrderData(int id, object dtoItem)
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

            Hashtable param = new Hashtable
            {
                ["id"] = id,
                ["data"] = dtoItem
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateorderdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        //
        // PRODUCT
        //
        [HttpPost]
        [Route("getproductoverviewdata")]
        public IHttpActionResult GetProductOverviewData(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable param = new Hashtable
            {
                ["id"] = id
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getproductoverviewdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        #region FACTORY ASSIGNMENT

        [HttpPost]
        [Route("getfactoryassignmentdata")]
        public IHttpActionResult GetFactoryAssignmentData(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable param = new Hashtable
            {
                ["id"] = id
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getfactoryassignmentdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updatefactoryassignmentdata")]
        public IHttpActionResult UpdateFactoryAssignmentData(int id, object dtoItem)
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

            Hashtable param = new Hashtable
            {
                ["id"] = id,
                ["data"] = dtoItem
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updatefactoryassignmentdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        #endregion

        #region INTERNAL REMARK

        [HttpPost]
        [Route("getinternalremarkdata")]
        public IHttpActionResult GetInternalRemarkData(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable param = new Hashtable
            {
                ["id"] = id
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getinternalremarkdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updateinternalremarkdata")]
        public IHttpActionResult UpdateInternalRemarkData(int id, object dtoItem)
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

            Hashtable param = new Hashtable
            {
                ["id"] = id,
                ["data"] = dtoItem
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateinternalremarkdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deleteinternalremarkdata")]
        public IHttpActionResult DeleteInternalRemarkData(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable param = new Hashtable
            {
                ["id"] = id
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "deleteinternalremarkdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        #endregion

        #region QA REMARK

        [HttpPost]
        [Route("getqaremarkdata")]
        public IHttpActionResult GetQARemarkData(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable param = new Hashtable
            {
                ["id"] = id
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getqaremarkdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updateqaremarkdata")]
        public IHttpActionResult UpdateQARemarkData(int id, object dtoItem)
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

            Hashtable param = new Hashtable
            {
                ["id"] = id,
                ["data"] = dtoItem
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateqaremarkdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deleteqaremarkdata")]
        public IHttpActionResult DeleteQARemarkData(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable param = new Hashtable
            {
                ["id"] = id
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "deleteqaremarkdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        #endregion

        #region BUILDING PROCESS

        [HttpPost]
        [Route("getbuildingprocessdata")]
        public IHttpActionResult GetBuildingProcessData(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable param = new Hashtable
            {
                ["id"] = id
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getbuildingprocessdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updatebuildingprocessdata")]
        public IHttpActionResult UpdateBuildingProcessData(int id, object dtoItem)
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

            Hashtable param = new Hashtable
            {
                ["id"] = id,
                ["data"] = dtoItem
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updatebuildingprocessdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deletebuildingprocessdata")]
        public IHttpActionResult DeleteBuildingProcessData(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable param = new Hashtable
            {
                ["id"] = id
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "deletebuildingprocessdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        #endregion

        #region ITEM DATA

        [HttpPost]
        [Route("getitemdata")]
        public IHttpActionResult GetItemData(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable param = new Hashtable
            {
                ["id"] = id
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getitemdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updateitemdata")]
        public IHttpActionResult UpdateItemData(int id, object dtoItem)
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

            Hashtable param = new Hashtable
            {
                ["id"] = id,
                ["data"] = dtoItem
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateitemdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        #endregion

        #region PRODUCT INFO

        [HttpPost]
        [Route("getproductinfodata")]
        public IHttpActionResult GetProductInfoData(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable param = new Hashtable
            {
                ["id"] = id
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getproductinfodata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updateproductinfodata")]
        public IHttpActionResult UpdateProductInfoData(int id, object dtoItem)
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

            Hashtable param = new Hashtable
            {
                ["id"] = id,
                ["data"] = dtoItem
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateproductinfodata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        #endregion

        //
        // OTHER
        //
        [HttpGet]
        [Route("quicksearchvnmonitor")]
        public IHttpActionResult QuickSearchVNMonitor(string query)
        {
            //get input
            Hashtable filters = new Hashtable();
            filters["query"] = query;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchvnmonitor", filters, out notification);
            return Ok(data);
        }

        [HttpGet]
        [Route("quicksearchnlmonitor")]
        public IHttpActionResult QuickSearchNLMonitor(string query)
        {
            //get input
            Hashtable filters = new Hashtable();
            filters["query"] = query;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchnlmonitor", filters, out notification);
            return Ok(data);
        }
    }
}
