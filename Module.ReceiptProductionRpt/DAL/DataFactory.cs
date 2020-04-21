using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceiptProductionRpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchForm, DTO.EditForm>
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
            totalRows = 0;

            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.SearchForm data = new DTO.SearchForm();
            data.ReceiptProductions = new List<DTO.ReceiptProduction>();

            string workOrderUD = null;
            int? workCenterID = null;
            int? productionTeamID = null;

            try
            {
                workOrderUD = (filters.ContainsKey("workOrderUD") && filters["workOrderUD"] != null && !string.IsNullOrEmpty(filters["workOrderUD"].ToString().Trim())) ? filters["workOrderUD"].ToString().Trim() : null;
                workCenterID = (filters.ContainsKey("workCenterID") && filters["workCenterID"] != null && !string.IsNullOrEmpty(filters["workCenterID"].ToString().Trim())) ? (int?)Convert.ToInt32(filters["workCenterID"].ToString().Trim()) : null;
                productionTeamID = (filters.ContainsKey("productionTeamID") && filters["productionTeamID"] != null && !string.IsNullOrEmpty(filters["productionTeamID"].ToString().Trim())) ? (int?)Convert.ToInt32(filters["productionTeamID"].ToString().Trim()) : null;

                using (var context = CreatedContext())
                {
                    totalRows = context.ReceiptProductionRpt_function_SearchResult(workOrderUD, workCenterID, orderBy, orderDirection).Count();
                    data.ReceiptProductions = converter.DB2DTO_ReceiptProductions(context.ReceiptProductionRpt_function_SearchResult(workOrderUD, workCenterID, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    data.ReceiptProductionQuantities = converter.DB2DTO_ReceiptProductionQuantities(context.ReceiptProductionRpt_function_SearchResultProductionTeam(productionTeamID, orderBy, orderDirection).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<DTO.ReceivingNote> receivingNotes = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<DTO.ReceivingNote>>();

            Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();

            bool result = true;

            using (var context = CreatedContext())
            {
                using (var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in receivingNotes)
                        {
                            ReceivingNote dbItem;

                            if (item.ReceivingNoteID < 0)
                            {
                                dbItem = new ReceivingNote();
                                context.ReceivingNote.Add(dbItem);
                            }
                            else
                            {
                                dbItem = context.ReceivingNote.FirstOrDefault(o => o.ReceivingNoteID == item.ReceivingNoteID);

                                if (dbItem == null)
                                {
                                    continue;
                                }
                            }

                            converter.DTO2DB_UpdateReceivingNote(item, ref dbItem);

                            dbItem.WorkCenterID = context.SupportMng_OPSequenceDetail_View.FirstOrDefault(o => o.WorkCenterID == item.OPSequenceDetailID).WorkCenterID;
                            dbItem.ReceivingNoteDate = DateTime.Now;
                            dbItem.CompanyID = frameworkFactory.GetCompanyID(userId);
                            dbItem.UpdatedBy = userId;
                            dbItem.UpdatedDate = DateTime.Now;

                            context.ReceivingNoteDetail.Local.Where(o => o.ReceivingNote == null).ToList().ForEach(o => context.ReceivingNoteDetail.Remove(o));

                            context.SaveChanges();

                            context.ReceivingNoteMng_function_GenerateReceivingNoteUD(dbItem.ReceivingNoteID, dbItem.CompanyID, dbItem.ReceivingNoteDate.Value.Year, dbItem.ReceivingNoteDate.Value.Month);
                        }

                        
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();

                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = ex.Message;

                        result = false;
                    }
                }
            }

            return result;
        }

        public DTO.InitForm GetInitData(int userId, out Library.DTO.Notification notification)
        {
            DTO.InitForm initData = new DTO.InitForm();
            Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
            Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();

            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                initData.WorkCenters = supportFactory.GetWorkCenter();
                
                int? companyID = frameworkFactory.GetCompanyID(userId);
                initData.ProductionTeams = supportFactory.GetProductionTeam(companyID);
                initData.FactoryWarehouses = supportFactory.GetFactoryWarehouse(companyID);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return initData;
        }

        public List<DTO.WorkOrder> GetWorkOrder(int userID, string searchQuery, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<DTO.WorkOrder> data = new List<DTO.WorkOrder>();

            Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();
            int? companyID = frameworkFactory.GetCompanyID(userID);

            try
            {
                using (var context = CreatedContext())
                {
                    data = converter.DB2DTO_WorkOrder(context.SupportMng_WorkOrder_View.Where(o => o.Label.Contains(searchQuery) && o.CompanyID == companyID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        private ReceiptProductionRptEntities CreatedContext()
        {
            return new ReceiptProductionRptEntities(Library.Helper.CreateEntityConnectionString("DAL.ReceiptProductionRptModel"));
        }
    }
}
