using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Library.DTO;

namespace Module.ProductionScheduleRpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.ReportData, DTO.ReportData>
    {
        private DataConverter converter = new DataConverter();
        public DataFactory() { }

        private ProductionScheduleRptEntities CreateContext()
        {
            return new ProductionScheduleRptEntities(Library.Helper.CreateEntityConnectionString("DAL.ProductionScheduleRptModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new Exception();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new Exception();
        }

        public override DTO.ReportData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new Exception();
        }

        public override DTO.ReportData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new Exception();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.ReportData GetProductionSchedule(int userId, Hashtable filters, out Notification notification)
        {
            DTO.ReportData data = new DTO.ReportData();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                int? workOrderID = null;
                string workOrderUD = null;
                int? workCenterID = null; ;
                int? productionTeamID = null;

                if (filters["workOrderID"] != null) workOrderID = Convert.ToInt32(filters["workOrderID"]);
                if (filters["workOrderUD"] != null) workOrderUD = filters["workOrderUD"].ToString();
                if (filters["workCenterID"] != null) workCenterID = Convert.ToInt32(filters["workCenterID"]);
                if (filters["productionTeamID"] != null) productionTeamID = Convert.ToInt32(filters["productionTeamID"]);

                Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);

                data.WorkCenters = support_factory.GetWorkCenter();
                data.ProductionTeams = support_factory.GetProductionTeam(companyID);
                data.FactoryWarehouseDtos = support_factory.GetFactoryWarehouse(companyID);

                using (ProductionScheduleRptEntities context = CreateContext())
                {
                    var x = context.ProductionScheduleRpt_ProductionSchedule_View.Where(o => o.WorkOrderUD.Contains(workOrderUD == null || workOrderUD == "" ? o.WorkOrderUD : workOrderUD)
                                                                                                && o.WorkCenterID == (workCenterID.HasValue ? workCenterID.Value : o.WorkCenterID)
                                                                                                && o.ProductionTeamID == (productionTeamID.HasValue ? productionTeamID.Value : o.ProductionTeamID)
                                                                                                );
                    data.ProductionSchedules = converter.DB2DTO_ProductionSchedule(x.ToList()); 
                    return data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return new DTO.ReportData();
            }
        }

        private int CreateDeliveryNote(int userId, List<DTO.ProductionSchedule> data, out Notification notification)
        {
            throw new NotImplementedException();
            //notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
            //    int? companyID = fw_factory.GetCompanyID(userId);
            //    using (ProductionScheduleRptEntities context = CreateContext())
            //    {
            //        var x = data.FirstOrDefault();
            //        DeliveryNote dbDeliveryNote = new DeliveryNote();
            //        DeliveryNoteDetail dbDetail = new DeliveryNoteDetail();
            //        dbDeliveryNote.DeliveryNoteUD = Guid.NewGuid().ToString();
            //        dbDeliveryNote.DeliveryNoteDate = DateTime.Now;
            //        dbDeliveryNote.OPSequenceDetailID = x.OPSequenceDetailID;
            //        dbDeliveryNote.FromProductionTeamID = x.ProductionTeamID;
            //        dbDeliveryNote.ToProductionTeamID = x.ToProductionTeamID;
            //        dbDeliveryNote.WorkOrderIDs = x.WorkOrderID.ToString();
            //        dbDeliveryNote.CompanyID = companyID;
            //        dbDeliveryNote.UpdatedBy = userId;
            //        dbDeliveryNote.UpdatedDate = DateTime.Now;
            //        dbDeliveryNote.ViewName = "Team2Team";

            //        foreach (var item in data)
            //        {
            //            dbDetail = new DeliveryNoteDetail();
            //            dbDetail.ProductionItemID = item.ProductionItemID;
            //            dbDetail.Qty = item.PutInProductQnt;
            //            dbDetail.BOMID = item.BOMID;

            //            dbDeliveryNote.DeliveryNoteDetail.Add(dbDetail);
            //        }
            //        context.DeliveryNote.Add(dbDeliveryNote);
            //        context.SaveChanges();
            //        return dbDeliveryNote.DeliveryNoteID;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //    notification.DetailMessage.Add(ex.Message);
            //    if (ex.GetBaseException() != null)
            //    {
            //        notification.DetailMessage.Add(ex.GetBaseException().Message);
            //    }
            //    return -1;
            //}
        }

        private int CreateDeliveryReceivingNote(int userId, List<DTO.ProductionSchedule> data, out Notification notification)
        {
            throw new NotImplementedException();
            //notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
            //    int? companyID = fw_factory.GetCompanyID(userId);
            //    using (ProductionScheduleRptEntities context = CreateContext())
            //    {
            //        var x = data.FirstOrDefault();
            //        ReceivingNote dbReceivingNote = new ReceivingNote();
            //        ReceivingNoteDetail dbDetail = new ReceivingNoteDetail();
            //        dbReceivingNote.ReceivingNoteUD = Guid.NewGuid().ToString();
            //        dbReceivingNote.ReceivingNoteDate = DateTime.Now;
            //        dbReceivingNote.FromProductionTeamID = x.ProductionTeamID;
            //        dbReceivingNote.ToFactoryWarehouseID = x.ToFactoryWarehouseID;
            //        dbReceivingNote.WorkOrderID = x.WorkOrderID;
            //        dbReceivingNote.CompanyID = companyID;
            //        dbReceivingNote.UpdatedBy = userId;
            //        dbReceivingNote.UpdatedDate = DateTime.Now;
            //        dbReceivingNote.ViewName = "Team2Warehouse";
            //        dbReceivingNote.OPSequenceDetailID = x.OPSequenceDetailID;

            //        foreach (var item in data)
            //        {
            //            dbDetail = new ReceivingNoteDetail();
            //            dbDetail.ProductionItemID = item.ProductionItemID;
            //            dbDetail.Quantity = item.PutInProductQnt;
            //            dbDetail.ToFactoryWarehouseID = item.ToFactoryWarehouseID;
            //            dbDetail.BOMID = item.BOMID;

            //            dbReceivingNote.ReceivingNoteDetail.Add(dbDetail);
            //        }
            //        context.ReceivingNote.Add(dbReceivingNote);
            //        context.SaveChanges();
            //        return dbReceivingNote.ReceivingNoteID ;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //    notification.DetailMessage.Add(ex.Message);
            //    if (ex.GetBaseException() != null)
            //    {
            //        notification.DetailMessage.Add(ex.GetBaseException().Message);
            //    }
            //    return -1;
            //}
        }

        public int FinishComponent(int userId, Hashtable filters, out Notification notification)
        {
            int receiptDeptID = Convert.ToInt32(filters["receiptDeptID"]);
            object finishComponentData = filters["finishComponentData"];
            List<DTO.ProductionSchedule> data = ((Newtonsoft.Json.Linq.JArray)finishComponentData).ToObject<List<DTO.ProductionSchedule>>();
            switch (receiptDeptID)
            {
                case 1:
                    return CreateDeliveryNote(userId, data, out notification);
                case 2:
                    return CreateDeliveryReceivingNote(userId, data, out notification);
                default:
                    notification = new Notification() { Type = NotificationType.Error, Message = "can not find receipt type" };
                    return -2;
            }                
        }


    }
}
