using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [Lib.LogFilterAtrribute]
    [RoutePrefix("api/model")]
    public class ModelController : ApiController
    {
        private string moduleCode = "ModelMng";

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

            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.ModelMng.SearchFormData data = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ModelMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            DTO.ModelMng.EditFormData data = bll.GetData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ModelMng.EditFormData>() { Data = data, Message = notification });
        }
        [HttpPost]
        [Route("query-check")]
        public IHttpActionResult QuyeryCheckList(string param)
        {
            Hashtable input = new Hashtable();
            input["query"] = param;
            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification = new Library.DTO.Notification();
            List<DTO.ModelMng.CheckListGroupDTO> data = bll.QuyeryCheckList(ControllerContext.GetAuthUserId(), "query-check", input, out notification);
            return Ok(data);
        }
        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.ModelMng.Model dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.ModelMng.Model>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.ModelMng.Model>() { Data = dtoItem, Message = notification });
            }

            // continue processing
            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ModelMng.Model>() { Data = dtoItem, Message = notification });
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

            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("restore")]
        public IHttpActionResult Restore(int id)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            bll.Restore(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchSupportData()
        {
            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            DTO.ModelMng.SearchFilterData data = bll.GetFilterData(out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ModelMng.SearchFilterData>() { Data = data, Message = notification, TotalRows = 0 });
        }

        [HttpPost]
        [Route("inittracking")]
        public IHttpActionResult InitTracking(int id, int mdlId)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            DTO.ModelMng.EditFormData2 data = bll.GetData2(id, mdlId, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<DTO.ModelMng.EditFormData2>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updatedetailtracking")]
        public IHttpActionResult UpdateDetailTracking(int id, int idIssue, DTO.ModelMng.ModelIssueTrack dtoItem)
        {
            Library.DTO.Notification notification;
            Module.Framework.BLL fwBll = new Module.Framework.BLL();

            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            if (!Helper.CommonHelper.ValidateDTO<DTO.ModelMng.ModelIssueTrack>(dtoItem, out notification))
                return Ok(new Library.DTO.ReturnData<DTO.ModelMng.ModelIssueTrack>() { Data = dtoItem, Message = notification });

            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.UpdateDetailTracking(id, idIssue, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<DTO.ModelMng.ModelIssueTrack>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("deletedetailtracking")]
        public IHttpActionResult DeleteDetailTracking(int id)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();

            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;

            bll.DeleteDetailTracking(id, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("getModelDefaultFactory")]
        public IHttpActionResult GetModelDefaultFactoryDetail(int modelID, int factoryID)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();

            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;

            List<DTO.ModelMng.ModelDefaultFactoryDetail> data = bll.GetModelDefaultFactoryDetail(modelID, factoryID, out notification);

            return Ok(new Library.DTO.ReturnData<List<DTO.ModelMng.ModelDefaultFactoryDetail>>() { Data = data, Message = notification });
        }

        //[HttpPost]
        //[Route("exportmodel")]
        //public IHttpActionResult ExportModel(string season)

        //{
        //    Module.Framework.BLL fwBLL = new Module.Framework.BLL();
        //    if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
        //    {
        //        return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
        //    }

        //    System.Collections.Hashtable filters = new System.Collections.Hashtable();
        //    filters["Season"] = season;
        //    //filters["endDate"] = endDate;
        //    BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
        //    Library.DTO.Notification notification;

        //    object data = bll.ExportModel(ControllerContext.GetAuthUserId(), season, out notification);
        //    return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        //    //object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "exportcostinvoice2", filters, out Library.DTO.Notification notification);
        //    //return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        //}

        [HttpPost]
        [Route("exportexcelmodel")]
        public IHttpActionResult ExportExcelModel(DTO.Search searchInput)
        {
            Library.DTO.Notification notification;
            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            string dataFileName = bll.ExportExcelModel(searchInput.Filters, out notification);
            //string dataFileName = string.Empty;
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }


        [HttpPost]
        [Route("getsampleproductdata")]
        public IHttpActionResult GetSampleProductData(int id)
        {
            // authencation
            Module.Framework.BLL fwBll = new Module.Framework.BLL();

            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            DTO.ModelMng.EditFormData data = bll.GetSampleProductData(id, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ModelMng.EditFormData>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getproductdata")]

        public IHttpActionResult GetProductData(int id)
        {
            //authencation 
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;

            DTO.ModelMng.EditFormData data = bll.GetProductData(id, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ModelMng.EditFormData>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getinitdatacreatemodel")]
        public IHttpActionResult GetInitDataCreateModel()
        {
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            object data = bll.GetInitDataCreateModel(ControllerContext.GetAuthUserId(), out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getdatacreatemodel")]
        public IHttpActionResult GetDataCreateModel(int id, int sampleProductID)
        {
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            object data = bll.GetDataCreateModel(ControllerContext.GetAuthUserId(), id, sampleProductID, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getproductbreakdown")]

        public IHttpActionResult GetProductBreakDown(int id)
        {
            //authencation 
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;

            DTO.ModelMng.EditFormData data = bll.GetProductBreakDown(ControllerContext.GetAuthUserId(), id, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ModelMng.EditFormData>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getmodeldefaultoptionbyseason")]

        public IHttpActionResult GetModelDefaultoptionBySeason(int id, string season)
        {
            //authencation 
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;

            List<DTO.ModelMng.ModelDefaultOptionDTO> data = bll.GetDefaultOptionBySeason(ControllerContext.GetAuthUserId(), id, season, out notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.ModelMng.ModelDefaultOptionDTO>>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getmodeldefaultoptionbypreviousseason")]

        public IHttpActionResult GetModelDefaultoptionByPreviousSeason(int id, string season)
        {
            //authencation 
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;

            List<DTO.ModelMng.ModelDefaultOptionDTO> data = bll.GetDefaultOptionByPreviousSeason(ControllerContext.GetAuthUserId(), id, season, out notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.ModelMng.ModelDefaultOptionDTO>>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("approveData")]
        public IHttpActionResult ApproveData(int id,int packagingID, object dtoItem)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();

            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));

            Notification notification;

            object data = bll.ApproveData(ControllerContext.GetAuthUserId(),id, packagingID, ref dtoItem, out notification);

            return Ok(new ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("resetData")]
        public IHttpActionResult ResetData(int id, object dtoItem)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();

            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanReset))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));

            Notification notification;

            object data = bll.ResetData(ControllerContext.GetAuthUserId(),id, ref dtoItem, out notification);

            return Ok(new ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getclientdata")]
        public IHttpActionResult GetClientData(string season, string clientID, int modelID)
        {
            //authencation 
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;

            int id = 0;

            if (season == "null")
            {
                season = "";
            }
            if(clientID != "null")
            {
                id = Convert.ToInt32(clientID);
            }

            List<DTO.ModelMng.ClientSaleData> data = bll.GetClientSaleData(season, id, modelID, out notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.ModelMng.ClientSaleData>>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("selfcalculationprice")]
        public IHttpActionResult SelfCalculationPrice(int id, string season, List<DTO.ModelMng.ModelPurchasingPriceConfigurationDetailDTO> dtoItem)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();

            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));

            Notification notification;

            object data = bll.SelfCalculationPrice(ControllerContext.GetAuthUserId(), id, season, ref dtoItem, out notification);

            return Ok(new ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("get-admin-dashboard")]
        public IHttpActionResult GetAdminDashboardData()
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();
            BLL.ModelMng bll = new BLL.ModelMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            object data = bll.GetModelNoFactory(ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}