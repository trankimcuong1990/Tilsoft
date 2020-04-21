using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TilsoftService.Helper;
using System.IO;
using System.Collections;
using Library.DTO;

namespace TilsoftService.Controllers
{
    [Authorize]
    [RoutePrefix("api/support")]
    public class SupportController : ApiController
    {
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.Support.Executor, Module.Support");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getClient2")]
        public IHttpActionResult GetClient2(DTO.Search searchInput)
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetClient2", searchInput.Filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getClient2")]
        public IHttpActionResult GetClient2(string param)
        {
            Hashtable filters = new Hashtable();
            filters["searchQuery"] = param;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetClient2", filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getFactory2")]
        public IHttpActionResult GetFactory2(DTO.Search searchInput)
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetFactory2", searchInput.Filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getFactory2")]
        public IHttpActionResult GetFactory2(string param)
        {
            Hashtable filters = new Hashtable();
            filters["searchQuery"] = param;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetFactory2", filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getProductionItem")]
        public IHttpActionResult GetProductionItem(DTO.Search searchInput)
        {
            //get support instant
            Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.Support.Executor, Module.Support");
            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetProductionItem", searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getClient")]
        public IHttpActionResult GetClient(DTO.Search searchInput)
        {
            //get support instant
            Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.Support.Executor, Module.Support");
            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetClient", searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getFactoryOrderDetail")]
        public IHttpActionResult GetFactoryOrderDetail(DTO.Search searchInput)
        {
            //get support instant
            Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.Support.Executor, Module.Support");
            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetFactoryOrderDetail", searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getSampleProduct")]
        public IHttpActionResult GetSampleProduct(DTO.Search searchInput)
        {
            //get support instant
            Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.Support.Executor, Module.Support");
            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetSampleProduct", searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getModel")]
        public IHttpActionResult GetModel(DTO.Search searchInput)
        {
            //get support instant
            Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.Support.Executor, Module.Support");
            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetModel", searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getProductionItemToDeliveryFromTeamToTeam")]
        public IHttpActionResult GetProductionItemToDeliveryFromTeamToTeam(DTO.Search searchInput)
        {
            //get support instant
            Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.Support.Executor, Module.Support");
            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetProductionItemToDeliveryFromTeamToTeam", searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getProductionItemToDeliveryFromWarehouseToTeam")]
        public IHttpActionResult GetProductionItemToDeliveryFromWarehouseToTeam(DTO.Search searchInput)
        {
            //get support instant
            Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.Support.Executor, Module.Support");
            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetProductionItemToDeliveryFromWarehouseToTeam", searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getProductionItemToDeliveryFromTeamToTeamToAmend")]
        public IHttpActionResult GetProductionItemToDeliveryFromTeamToTeamToAmend(DTO.Search searchInput)
        {
            //get support instant
            Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.Support.Executor, Module.Support");
            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetProductionItemToDeliveryFromTeamToTeamToAmend", searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getProductionItemToDeliveryFromWarehouseToTeamToAmend")]
        public IHttpActionResult GetProductionItemToDeliveryFromWarehouseToTeamToAmend(DTO.Search searchInput)
        {
            //get support instant
            Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.Support.Executor, Module.Support");
            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetProductionItemToDeliveryFromWarehouseToTeamToAmend", searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("quicksearchmodel")]
        public IHttpActionResult QuickSearchModel(DTO.Search searchInput)
        {
            BLL.Support bll = new BLL.Support();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.Support.Model> Models = bll.QuickSearchModel(searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.Model>>() { Data = Models, Message = notification, TotalRows = totalRows });
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("select2model")]
        public IHttpActionResult Select2Model(string query)
        {
            return Ok((new BLL.Support()).GetModels().Where(o => o.ModelNM.ToUpper().Contains(query.ToUpper())).ToList());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("select2client")]
        public IHttpActionResult Select2Client(string query)
        {
            return Ok((new BLL.Support()).GetClients().Where(o => o.ClientUD.ToUpper().Contains(query.ToUpper()) || o.ClientNM.ToUpper().Contains(query.ToUpper())).ToList());
        }

        [HttpPost]
        [Route("quicksearchwarehouse")]
        public IHttpActionResult QuickSearchWareHouse()
        {
            BLL.Support bll = new BLL.Support();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.Support.WareHouse> WareHouses = bll.QuickSearchWareHouse(out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.WareHouse>>() { Data = WareHouses, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearchconfirmedproduct")]
        public IHttpActionResult QuickSearchConfirmedProduct(DTO.Search searchInput)
        {
            BLL.Support bll = new BLL.Support();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.Support.ConfirmedProduct> data = bll.QuickSearchConfirmedProduct(searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.ConfirmedProduct>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearchsparepart")]
        public IHttpActionResult QuickSearchSparepart(DTO.Search searchInput)
        {
            BLL.Support bll = new BLL.Support();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.Support.Sparepart> data = bll.QuickSearchSparepart(searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.Sparepart>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearchframematerialoption")]
        public IHttpActionResult QuickSearchFrameOption(DTO.Search searchInput)
        {
            BLL.Support bll = new BLL.Support();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.Support.FrameMaterialOption> data = bll.QuickSearchFrameMaterialOption(searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.FrameMaterialOption>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearchmaterialoption")]
        public IHttpActionResult QuickSearchMaterialOption(DTO.Search searchInput)
        {
            BLL.Support bll = new BLL.Support();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.Support.MaterialOption> data = bll.QuickSearchMaterialOption(searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.MaterialOption>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearchsubmaterialoption")]
        public IHttpActionResult QuickSearchSubMaterialOption(DTO.Search searchInput)
        {
            BLL.Support bll = new BLL.Support();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.Support.SubMaterialOption> data = bll.QuickSearchSubMaterialOption(searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.SubMaterialOption>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearchcushionoption")]
        public IHttpActionResult QuickSearchCushionOption(DTO.Search searchInput)
        {
            BLL.Support bll = new BLL.Support();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.Support.CushionOption> data = bll.QuickSearchCushionOption(searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.CushionOption>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearchcushioncoloroption")]
        public IHttpActionResult QuickSearchCushionColorOption(DTO.Search searchInput)
        {
            BLL.Support bll = new BLL.Support();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.Support.CushionColorOption> data = bll.QuickSearchCushionColorOption(searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.CushionColorOption>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearchpiecemodel")]
        public IHttpActionResult QuickSearchPieceModel(DTO.Search searchInput)
        {
            BLL.Support bll = new BLL.Support();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.Support.Model> data = bll.QuickSearchPieceModel(searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.Model>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearchclient")]
        public IHttpActionResult QuickSearchClient(DTO.Search searchInput)
        {
            var bll = new BLL.Support();
            Notification notification;
            int totalRows;
            var data = bll.QuickSearchClient(searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            if (notification.Type != NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new ReturnData<IEnumerable<DTO.Support.Client>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearchbooking")]
        public IHttpActionResult QuickSearchBooking(DTO.Search searchInput)
        {
            BLL.Support bll = new BLL.Support();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.Support.Booking> data = bll.QuickSearchBooking(searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.Booking>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearchmodelseason")]
        public IHttpActionResult QuickSearchModelSeason(DTO.Search searchInput)
        {
            BLL.Support bll = new BLL.Support();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.Support.ModelSeason> data = bll.QuickSearchModelSeason(searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.ModelSeason>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("getproductwizarddata")]
        public IHttpActionResult GetProductWizardData(int modelID, int? clientID = null, string season = "")
        {
            BLL.Support bll = new BLL.Support();
            Library.DTO.Notification notification;
            DTO.Controls.ProductWizardData data = bll.GetProductWizardData(modelID, clientID, season, out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<DTO.Controls.ProductWizardData>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("quicksearchbackcushionoption")]
        public IHttpActionResult QuickSearchBackCushionOption(DTO.Search searchInput)
        {
            BLL.Support bll = new BLL.Support();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.Support.BackCushionOption> data = bll.QuickSearchBackCushionOption(searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.BackCushionOption>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearchseatcushionoption")]
        public IHttpActionResult QuickSearchSeatCushionOption(DTO.Search searchInput)
        {
            BLL.Support bll = new BLL.Support();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.Support.SeatCushionOption> data = bll.QuickSearchSeatCushionOption(searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.SeatCushionOption>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("getxml")]
        public IHttpActionResult GetXML(string filename)
        {
            string filepath = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext) + @"\" + filename;
            Library.DTO.Notification notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            string data = string.Empty;
            try
            {
                // validation
                if (!File.Exists(filepath))
                {
                    throw new Exception("File not found !");
                }

                if (Path.GetExtension(filepath) != ".xml")
                {
                    throw new Exception("File type not supported !");
                }

                StreamReader sr = new StreamReader(filepath);
                data = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return Ok(new Library.DTO.ReturnData<string>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getseasons")]
        public IHttpActionResult GetSeason()
        {
            BLL.Support bll = new BLL.Support();
            IEnumerable<DTO.Support.Season> data = bll.GetSeason();
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.Season>>() { Data = data });
        }

        [HttpPost]
        [Route("getshowroomreceipttype")]
        public IHttpActionResult GetShowroomReceiptType()
        {
            BLL.Support bll = new BLL.Support();
            IEnumerable<DTO.Support.ShowroomReceiptType> data = bll.GetShowroomReceiptType();
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.ShowroomReceiptType>>() { Data = data });
        }

        [HttpPost]
        [Route("quicksearchshowroomitem")]
        public IHttpActionResult QuickSearchShowroomItem(DTO.Search searchInput)
        {
            BLL.Support bll = new BLL.Support();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.Support.ShowroomItem> data = bll.QuickSearchShowroomItem(searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.ShowroomItem>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearchwarehouseitem")]
        public IHttpActionResult QuickSearchWarehouseItem(DTO.Search searchInput)
        {
            BLL.Support bll = new BLL.Support();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.Support.WarehouseItem> data = bll.QuickSearchWarehouseItem(searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.WarehouseItem>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearchshowroomarea")]
        public IHttpActionResult QuickSearchShowroomArea(DTO.Search searchInput)
        {
            BLL.Support bll = new BLL.Support();
            Library.DTO.Notification notification;
            IEnumerable<DTO.Support.ShowroomArea> data = bll.QuickSearchShowroomArea(searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.ShowroomArea>>() { Data = data, Message = notification, TotalRows = data.Count() });
        }

        [HttpPost]
        [Route("quicksearchshowroomareabyphysicalqnt")]
        public IHttpActionResult QuickSearchShowroomAreaByPhysicalQnt(DTO.Search searchInput)
        {
            BLL.Support bll = new BLL.Support();
            Library.DTO.Notification notification;
            IEnumerable<DTO.Support.ShowroomAreaByPhysicalQnt> data = bll.QuickSearchShowroomAreaByPhysicalQnt(searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.ShowroomAreaByPhysicalQnt>>() { Data = data, Message = notification, TotalRows = data.Count() });
        }

        [HttpPost]
        [Route("quicksearchfactorymaterial")]
        public IHttpActionResult QuickSearchFactoryMaterial(DTO.Search searchInput)
        {
            //get support instant
            Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.Support.Executor, Module.Support");

            //get input
            Hashtable filters = searchInput.Filters;
            filters["pageSize"] = searchInput.PageSize;
            filters["pageIndex"] = searchInput.PageIndex;
            filters["sortedBy"] = searchInput.SortedBy;
            filters["sortedDirection"] = searchInput.SortedDirection;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "QuickSearchFactoryMaterial", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("quicksearchfactoryorder")]
        public IHttpActionResult QuickSearchFactoryOrder(DTO.Search searchInput)
        {
            //get support instant
            Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.Support.Executor, Module.Support");

            //get input
            Hashtable filters = searchInput.Filters;
            filters["pageSize"] = searchInput.PageSize;
            filters["pageIndex"] = searchInput.PageIndex;
            filters["sortedBy"] = searchInput.SortedBy;
            filters["sortedDirection"] = searchInput.SortedDirection;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "QuickSearchFactoryOrder", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("quickSearchfactoryfinishedproduct")]
        public IHttpActionResult QuickSearchFactoryFinishedProduct(DTO.Search searchInput)
        {
            //get support instant
            Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.Support.Executor, Module.Support");

            //get input
            Hashtable filters = searchInput.Filters;
            filters["pageSize"] = searchInput.PageSize;
            filters["pageIndex"] = searchInput.PageIndex;
            filters["sortedBy"] = searchInput.SortedBy;
            filters["sortedDirection"] = searchInput.SortedDirection;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "QuickSearchFactoryFinishedProduct", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getfactory")]
        public IHttpActionResult GetFactory()
        {
            //get support instant
            Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.Support.Executor, Module.Support");

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetFactory", null, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getfactorybyuser")]
        public IHttpActionResult GetFactoryByUser()
        {
            //get support instant
            Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.Support.Executor, Module.Support");

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetFactoryByUser", null, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpGet]
        [Route("searchclient")]
        public IHttpActionResult QuickSearchClient(string query)
        {
            Notification notification;
            var input = new Hashtable();
            input["query"] = query;
            var data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "searchclient", input, out notification);
            return Ok(data);
        }


        [HttpPost]
        [Route("quicksearchavfsupplier")]
        public IHttpActionResult QuickSearchAVFSupplier(DTO.Search searchInput)
        {
            //get support instant
            Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.Support.Executor, Module.Support");

            //get input
            Hashtable filters = searchInput.Filters;
            filters["pageSize"] = searchInput.PageSize;
            filters["pageIndex"] = searchInput.PageIndex;
            filters["sortedBy"] = searchInput.SortedBy;
            filters["sortedDirection"] = searchInput.SortedDirection;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "QuickSearchAVFSupplier", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("quicksearchemployee")]
        public IHttpActionResult QuickSearchEmployee(DTO.Search searchInput)
        {
            //get support instant
            Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.Support.Executor, Module.Support");

            //get input
            Hashtable filters = searchInput.Filters;
            filters["pageSize"] = searchInput.PageSize;
            filters["pageIndex"] = searchInput.PageIndex;
            filters["sortedBy"] = searchInput.SortedBy;
            filters["sortedDirection"] = searchInput.SortedDirection;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "QuickSearchEmployee", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        //
        // QUICK SEARCH
        //
        // client
        [HttpGet]
        [Route("quicksearchclient2")]
        public IHttpActionResult QuickSearchClient2(string query)
        {
            //get input
            Hashtable filters = new Hashtable();
            filters["query"] = query;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchclient2", filters, out notification);
            return Ok(data);
        }

        // model
        [HttpGet]
        [Route("quicksearchmodel2")]
        public IHttpActionResult QuickSearchModel2(string query)
        {
            //get input
            Hashtable filters = new Hashtable();
            filters["query"] = query;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchmodel2", filters, out notification);
            return Ok(data);
        }

        // material
        [HttpGet]
        [Route("quicksearchmaterial2")]
        public IHttpActionResult QuickSearchMaterial2(string query)
        {
            //get input
            Hashtable filters = new Hashtable();
            filters["query"] = query;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchmaterial2", filters, out notification);
            return Ok(data);
        }

        // material type
        [HttpGet]
        [Route("quicksearchmaterialtype2")]
        public IHttpActionResult QuickSearchMaterialType2(string query)
        {
            //get input
            Hashtable filters = new Hashtable();
            filters["query"] = query;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchmaterialtype2", filters, out notification);
            return Ok(data);
        }

        // material color
        [HttpGet]
        [Route("quicksearchmaterialcolor2")]
        public IHttpActionResult QuickSearchMaterialColor2(string query)
        {
            //get input
            Hashtable filters = new Hashtable();
            filters["query"] = query;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchmaterialcolor2", filters, out notification);
            return Ok(data);
        }

        // frame material
        [HttpGet]
        [Route("quicksearchframematerial2")]
        public IHttpActionResult QuickSearchFrameMaterial2(string query)
        {
            //get input
            Hashtable filters = new Hashtable();
            filters["query"] = query;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchframematerial2", filters, out notification);
            return Ok(data);
        }

        // frame material color
        [HttpGet]
        [Route("quicksearchframematerialcolor2")]
        public IHttpActionResult QuickSearchFrameMaterialColor2(string query)
        {
            //get input
            Hashtable filters = new Hashtable();
            filters["query"] = query;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchframematerialcolor2", filters, out notification);
            return Ok(data);
        }

        // sub material
        [HttpGet]
        [Route("quicksearchsubmaterial2")]
        public IHttpActionResult QuickSearchSubMaterial2(string query)
        {
            //get input
            Hashtable filters = new Hashtable();
            filters["query"] = query;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchsubmaterial2", filters, out notification);
            return Ok(data);
        }

        // sub material color
        [HttpGet]
        [Route("quicksearchsubmaterialcolor2")]
        public IHttpActionResult QuickSearchSubMaterialColor2(string query)
        {
            //get input
            Hashtable filters = new Hashtable();
            filters["query"] = query;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchsubmaterialcolor2", filters, out notification);
            return Ok(data);
        }

        // back cushion
        [HttpGet]
        [Route("quicksearchbackcushion2")]
        public IHttpActionResult QuickSearchBackCushion2(string query)
        {
            //get input
            Hashtable filters = new Hashtable();
            filters["query"] = query;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchbackcushion2", filters, out notification);
            return Ok(data);
        }

        // seat cushion
        [HttpGet]
        [Route("quicksearchseatcushion2")]
        public IHttpActionResult QuickSearchSeatCushion2(string query)
        {
            //get input
            Hashtable filters = new Hashtable();
            filters["query"] = query;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchseatcushion2", filters, out notification);
            return Ok(data);
        }

        // cushion color
        [HttpGet]
        [Route("quicksearchcushioncolor2")]
        public IHttpActionResult QuickSearchCushionColor2(string query)
        {
            //get input
            Hashtable filters = new Hashtable();
            filters["query"] = query;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchcushioncolor2", filters, out notification);
            return Ok(data);
        }

        // model image
        [HttpGet]
        [Route("getmodelimage")]
        public IHttpActionResult GetModelImage(int id)
        {
            //get input
            Hashtable filters = new Hashtable();
            filters["id"] = id;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getmodelimage", filters, out notification);
            return Ok(data);
        }

        // product info
        [HttpGet]
        [Route("getproductinfo")]
        public IHttpActionResult GetProductInfo(string c)
        {
            //get input
            Hashtable filters = new Hashtable();
            filters["articleCode"] = c;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getproductinfo", filters, out notification);
            return Ok(data);
        }

        // client 3 - client with name
        [HttpGet]
        [Route("quicksearchclient3")]
        public IHttpActionResult QuickSearchClient3(string query)
        {
            //get input
            Hashtable filters = new Hashtable();
            filters["query"] = query;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchclient3", filters, out notification);
            return Ok(data);
        }

        [HttpPost]
        [Route("quicksearchforwarder")]
        public IHttpActionResult QuickSearchForwarder(DTO.Search searchInput)
        {

            var bll = new BLL.Support();
            Notification notification;
            var totalRows = 0;

            var data = bll.QuickSearchForwarder(searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            if (notification.Type != NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new ReturnData<IEnumerable<DTO.Support.Forwarder>>() { Data = data, Message = notification, TotalRows = totalRows });

        }

        [HttpPost]
        [Route("quickSearchProductionItem")]
        public IHttpActionResult GetProductionItem2(string param)
        {
            Hashtable filters = new Hashtable();
            filters["searchQuery"] = param;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchproductionitem2", filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getProductionItemWithFilters")]
        public IHttpActionResult GetProductionItemWithFilters(string param, string param2)
        {
            Hashtable filters = new Hashtable();
            filters["searchQuery"] = param;
            filters["branchID"] = param2;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getProductionItemWithFilters", filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getWorkOrderApproved")]
        public IHttpActionResult GetWorkOrderApproved(string param)
        {
            Hashtable filters = new Hashtable();
            filters["searchQuery"] = param;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getWorkOrderApproved", filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getPurchaseOrderApprove")]
        public IHttpActionResult GetPurchaseOrderApprove(string param)
        {
            Hashtable filters = new Hashtable();
            filters["searchQuery"] = param;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getPurchaseOrderApprove", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getModel2")]
        public IHttpActionResult GetModel2(string param)
        {
            Hashtable filters = new Hashtable();
            filters["searchQuery"] = param;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getModel2", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getWorkOrder")]
        public IHttpActionResult GetWorkOrder(string param)
        {
            Hashtable filters = new Hashtable();
            filters["searchQuery"] = param;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getWorkOrder", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getuserprofiles")]
        public IHttpActionResult GetUserProfiles(string param)
        {
            Hashtable filters = new Hashtable();
            filters["searchString"] = param;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetUserProfiles", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
