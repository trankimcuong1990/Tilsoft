using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Module.ProductionIssue.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchForm, DTO.EditForm>
    {
        private DataConverter converter = new DataConverter();

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.EditForm GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.SearchForm GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new Exception();
        }

        public DTO.InitForm GetInit(int userID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.InitForm data = new DTO.InitForm();
            Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

            Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();
            int? companyID = frameworkFactory.GetCompanyID(userID);

            data.SupportWorkCenter = supportFactory.GetWorkCenter();
            data.SupportProductionTeam = supportFactory.GetProductionTeam(companyID);
            data.SupportFactoryWarehouse = supportFactory.GetFactoryWarehouse(companyID);

            return data;
        }

        public DTO.SearchForm GetDataWithFilter(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            totalRows = 0;

            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.SearchForm data = new DTO.SearchForm();

            try
            {
                int workOrderID = (filters.ContainsKey("workOrderID") && filters["workOrderID"] != null && !string.IsNullOrEmpty(filters["workOrderID"].ToString().Trim())) ? Convert.ToInt32(filters["workOrderID"].ToString().Trim()) : 0;
                int? workCenterID = (filters.ContainsKey("workCenterID") && filters["workCenterID"] != null && !string.IsNullOrEmpty(filters["workCenterID"].ToString().Trim())) ? (int?)Convert.ToInt32(filters["workCenterID"].ToString().Trim()) : null;

                using (var context = CreateContext())
                {
                    totalRows = context.ProductionIssueOverviewRpt_function_ProductionIssueOverviewSearchResult(workOrderID, workCenterID, orderBy, orderDirection).Count();
                    data.ProductionIssue = converter.DB2DTO_ProductionIssueOverview(context.ProductionIssueOverviewRpt_function_ProductionIssueOverviewSearchResult(workOrderID, workCenterID, orderBy, orderDirection)/*.Skip(pageSize * (pageIndex - 1)).Take(pageSize)*/.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public List<DTO.QuickSearchWorkOrder> GetWorkOrder(int userID, string searchQuery, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<DTO.QuickSearchWorkOrder> data = new List<DTO.QuickSearchWorkOrder>();

            Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();
            int? companyID = frameworkFactory.GetCompanyID(userID);

            try
            {
                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_WorkOrder(context.ProductionIssueMng_WorkOrder_View.Where(o => o.Label.Contains(searchQuery) && o.CompanyID == companyID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.HistoryDeliveryNote> GetHistoryDeliveryNote(int userID, int workOrderID, int workCenterID, int productionItemID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<DTO.HistoryDeliveryNote> data = new List<DTO.HistoryDeliveryNote>();

            try
            {
                using (var context = CreateContext())
                {
                    //data = converter.DB2DTO_HistoryDeliveryNote(context.ProductionIssueMng_HistoryTransferProductionTeam_View.Where(o => o.WorkOrderID == workOrderID && o.WorkCenterID == workCenterID && o.ProductionItemID == productionItemID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        private ProductionItemIssueEntities CreateContext()
        {
            return new ProductionItemIssueEntities(Library.Helper.CreateEntityConnectionString("DAL.ProductionItemIssueModel"));
        }

        public object UpdateData(int userID, Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

                int id = (filters.ContainsKey("id") && filters["id"] != null && !string.IsNullOrEmpty(filters["id"].ToString())) ? Convert.ToInt32(filters["id"].ToString()) : 0;
                List<DTO.ProductionIssueData> dataItem = filters.ContainsKey("view") && filters["view"] != null && !string.IsNullOrEmpty(filters["view"].ToString().Trim()) ? ((Newtonsoft.Json.Linq.JArray)filters["view"]).ToObject<List<DTO.ProductionIssueData>>() : null;

                using (var context = CreateContext())
                {
                    if (dataItem == null || dataItem.Count == 0)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not update data with array is null!";

                        return null;
                    }

                    foreach (DTO.ProductionIssueData dtoItem in dataItem)
                    {
                        DeliveryNote dbItem = new DeliveryNote();
                        context.DeliveryNote.Add(dbItem);

                        converter.DTO2DB_DeliveryNote(dtoItem, ref dbItem);

                        dbItem.DeliveryNoteDate = DateTime.Now;
                        dbItem.CompanyID = fwFactory.GetCompanyID(userID);
                        dbItem.UpdatedBy = userID;
                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.ViewName = "Warehouse2Team";
                        dbItem.ToProductionTeamID = dtoItem.ToProductionTeamID;

                        context.SaveChanges();
                        context.DeliveryNoteMng_function_GenerateDeliveryNoteUD(dbItem.DeliveryNoteID, dbItem.CompanyID, dbItem.DeliveryNoteDate.Value.Year, dbItem.DeliveryNoteDate.Value.Month);
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return null;
        }
    }
}
