using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Library;
using Library.DTO;

namespace Module.WeeklyProductionRpt.DAL
{
    internal partial class DataFactory : Library.Base.DataFactory2<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();
        private WeeklyProductionRptEntities CreateContext()
        {
            return new WeeklyProductionRptEntities(Library.Helper.CreateEntityConnectionString("DAL.WeeklyProductionRptModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string season = null;
                int? factoryID = null;

                if (filters.ContainsKey("season") && !string.IsNullOrEmpty(filters["season"].ToString()))
                {
                    season = filters["season"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("factoryID") && filters["factoryID"] !=null)
                {
                    factoryID = Convert.ToInt32(filters["factoryID"]);
                }
                
                using (WeeklyProductionRptEntities context = CreateContext())
                {
                    totalRows = context.WeeklyProductionRpt_function_GetFrameOverview(orderBy, orderDirection, season, factoryID).Count();
                    var result = context.WeeklyProductionRpt_function_GetFrameOverview(orderBy, orderDirection, season, factoryID);
                    searchFormData.Data = converter.DB2DTO_Search(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    searchFormData.Seasons = new Module.Support.DAL.DataFactory().GetSeason();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return searchFormData;
        }
        public override DTO.EditFormData GetData(int userId, int id,Hashtable param, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //DTO.EditFormData editFormData = new DTO.EditFormData();
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (WeeklyProductionRptEntities context = CreateContext())
            //    {
            //        int? companyID = fw_factory.GetCompanyID(userId);
            //        if (id > 0)
            //        {
            //            var dbItem = context.WeeklyProductionRpt_ProductionStatistics_View.Where(o => o.CompanyID == companyID).FirstOrDefault(o => o.ProductionStatisticsID == id);
            //            editFormData.Data = converter.DB2DTO_ProductionStatistics(dbItem);
            //        }
            //        else
            //        {
            //            //initialize item data base on selected workorder
            //            int workCenterID = Convert.ToInt32(param["workCenterID"]);
            //            string producedDate = param["producedDate"].ToString();

            //            //init value
            //            editFormData.Data.WorkCenterID = workCenterID;
            //            editFormData.Data.ProducedDate = producedDate;
            //            editFormData.Data.WorkCenterNM = new Module.Support.DAL.DataFactory().GetWorkCenter().Where(o => o.WorkCenterID == workCenterID).FirstOrDefault().WorkCenterNM;

            //            //get planning production by workcenter
            //            var planningProductionTeam = context.WeeklyProductionRpt_function_GetPlanningProductionTeam(companyID, workCenterID, producedDate).ToList();
            //            editFormData.Data.ProductionStatisticsDetailDTOs = AutoMapper.Mapper.Map<List<WeeklyProductionRpt_PlanningProductionTeam_View>, List<DTO.ProductionStatisticsDetailDTO>>(planningProductionTeam);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = Library.Helper.GetInnerException(ex).Message;
            //}
            //return editFormData;
        }
        public override bool DeleteData(int userID, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            context.SampleTechnicalDrawing.Remove(dbItem);
            //            // also remove all child item if needed
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }
        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //DTO.ProductionStatisticsDTO dtoProductionStatistics = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.ProductionStatisticsDTO>();
            //try
            //{
            //    int? companyID = fw_factory.GetCompanyID(userId);
            //    using (WeeklyProductionRptEntities context = CreateContext())
            //    {
            //        ProductionStatistics dbItem = null;
            //        if (id == 0)
            //        {
            //            dbItem = new ProductionStatistics();
            //            context.ProductionStatistics.Add(dbItem);
            //        }
            //        else
            //        {
            //            dbItem = context.ProductionStatistics.Where(o => o.ProductionStatisticsID == id).FirstOrDefault();
            //        }
            //        if (dbItem == null)
            //        {
            //            notification.Message = "data not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            //convert dto to db
            //            converter.DTO2DB_ProductionStatistics(dtoProductionStatistics, ref dbItem, userId, companyID);
            //            //remove orphan
            //            context.ProductionStatisticsDetail.Local.Where(o => o.ProductionStatistics == null).ToList().ForEach(o => context.ProductionStatisticsDetail.Remove(o));
            //            //save data
            //            context.SaveChanges();
            //            //get return data
            //            dtoItem = GetData(userId, dbItem.ProductionStatisticsID, null, out notification).Data;
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = Library.Helper.GetInnerException(ex).Message;
            //    return false;
            //}            
        }
        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = true;
            //            dbItem.ConfirmedBy = userId;
            //            dbItem.ConfirmedDate = DateTime.Now;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }
        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = false;
            //            dbItem.ConfirmedBy = null;
            //            dbItem.ConfirmedDate = null;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }
        public override object GetInitData(int userId, out Notification notification)
        {
            throw new NotImplementedException();
            //DTO.InitFormData data = new DTO.InitFormData();
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (WeeklyProductionRptEntities context = CreateContext())
            //    {
            //        var wc = context.WorkCenter.Where(o => o.IsVirtual.HasValue && o.IsVirtual.Value).ToList();
            //        data.WorkCenterDTOs = AutoMapper.Mapper.Map<List<WorkCenter>, List<DTO.WorkCenterDTO>>(wc);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = Library.Helper.GetInnerException(ex).Message;
            //}
            //return data;
        }
        public override object GetSearchFilter(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
