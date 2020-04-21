using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Library.DTO;

namespace Module.PlanningReportWorkcenter.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private PlanningReportWorkCenterEntities CreateContext()
        {
            return new PlanningReportWorkCenterEntities(Library.Helper.CreateEntityConnectionString("DAL.PlanningReportWorkCenterModel"));
        }
        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.EditFormData GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.WorkOrderDTO>();
            data.WeekInfoDTOs = new List<DTO.WeekInfoDTO>();
            data.ReceivingDetailDTOs = new List<DTO.ReceivingDetailDTO>();
            data.ReceivingSetDetailDTOs = new List<DTO.ReceivingSetDetailDTO>();
            data.WorkcenterStatus = new List<DTO.WorkcenterStatus>();
            data.MaterialStatus = new List<DTO.MaterialStatus>();
            totalRows = 0;

            string StartDateFrom = null;
            string StartDateTo = null;
            string FinishedDateFrom = null;
            string FinishedDateTo = null;
            string WorkOrderUD = null;
            string ClientUD = null;
            string ProformaInvoiceNo = null;
            string WorkOrderStatus = null;
            int? WorkCenterID = null;
            int? UserID = null;
            int CompanyID = 0;
            if (filters.ContainsKey("StartDateFrom") && filters["StartDateFrom"] != null && !string.IsNullOrEmpty(filters["StartDateFrom"].ToString()))
            {
                StartDateFrom = filters["StartDateFrom"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("StartDateTo") && filters["StartDateTo"] != null && !string.IsNullOrEmpty(filters["StartDateTo"].ToString()))
            {
                StartDateTo = filters["StartDateTo"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FinishedDateFrom") && filters["FinishedDateFrom"] != null && !string.IsNullOrEmpty(filters["FinishedDateFrom"].ToString()))
            {
                FinishedDateFrom = filters["FinishedDateFrom"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FinishedDateTo") && filters["FinishedDateTo"] != null && !string.IsNullOrEmpty(filters["FinishedDateTo"].ToString()))
            {
                FinishedDateTo = filters["FinishedDateTo"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("WorkOrderUD") && filters["WorkOrderUD"] != null && !string.IsNullOrEmpty(filters["WorkOrderUD"].ToString()))
            {
                WorkOrderUD = filters["WorkOrderUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ClientUD") && filters["ClientUD"] != null && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
            {
                ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ProformaInvoiceNo") && filters["ProformaInvoiceNo"] != null && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
            {
                ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("WorkOrderStatus") && filters["WorkOrderStatus"] != null && !string.IsNullOrEmpty(filters["WorkOrderStatus"].ToString()))
            {
                WorkOrderStatus = filters["WorkOrderStatus"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("WorkCenterID") && filters["WorkCenterID"] != null && !string.IsNullOrEmpty(filters["WorkCenterID"].ToString()))
            {
                WorkCenterID = Convert.ToInt32(filters["WorkCenterID"]);
            }
            else
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = "Work Center invalid";
                return data;
            }
            if (filters.ContainsKey("UserID") && filters["UserID"] != null && !string.IsNullOrEmpty(filters["UserID"].ToString()))
            {
                UserID = Convert.ToInt32(filters["UserID"]);
            }

            //try to get data
            try
            {
                try
                {
                    CompanyID = fwFactory.GetCompanyID(UserID.Value).Value;
                }
                catch
                {
                    CompanyID = 0;
                }

                using (PlanningReportWorkCenterEntities context = CreateContext())
                {
                    var dbWorkOrders = context.PlanningReportWorkCenter_function_SearchWorkOrder(StartDateFrom, StartDateTo, FinishedDateFrom, FinishedDateTo, WorkOrderUD, ClientUD, ProformaInvoiceNo, WorkOrderStatus, WorkCenterID, CompanyID).OrderBy(o => o.StartDate).ToList();
                    data.Data = converter.DB2DTO_WorkOrderList(dbWorkOrders);

                    //get real
                    List<int?> workOrderIDs = data.Data.Select(s => s.WorkOrderID).ToList();
                    List<int?> workCenterIDs = data.Data.Select(s => s.WorkCenterID).ToList();
                    var realData = context.PlanningReportWorkCenter_ReceivingDetail_View.Where(o => workOrderIDs.Contains(o.WorkOrderID.Value) && workCenterIDs.Contains(o.WorkCenterID.Value)).ToList();
                    var detailData = context.PlanningReportWorkCenter_SetDetail_View.Where(o => workOrderIDs.Contains(o.WorkOrderID.Value) && workCenterIDs.Contains(o.WorkCenterID.Value)).ToList();
                    data.ReceivingDetailDTOs = converter.DB2DTO_ReceivingDetail(realData);
                    //data.ReceivingSetDetailDTOs = converter.DB2DTO_ReceivingSetDetail(context.PlanningReportWorkCenter_SetDetail_View.Where(o => workOrderIDs.Contains(o.WorkOrderID.Value) && workCenterIDs.Contains(o.WorkCenterID.Value)).ToList());
                    data.ReceivingSetDetailDTOs = converter.DB2DTO_ReceivingSetDetail(detailData);
                    data.WorkcenterStatus = converter.DB2DTO_GetWorkCenterStatus(context.PlanningReportWorkCenter_GetWorkCenterStatus_View.Where(o => o.WorkCenterID != null && o.WorkCenterID == WorkCenterID).ToList());
                    data.MaterialStatus = converter.DB2DTO_GetMaterialStatus(context.PlanningReportWorkCenter_GetMaterialStatus_View.ToList());
                    int? FromDate = dbWorkOrders.Min(o => o.WeekInfoID);
                    //int? ToDate = dbWorkOrders.Max(o => o.WeekInfoID);
                    int? ToDate;
                    if (realData.Max(o => o.WeekInfoID) > dbWorkOrders.Max(o => o.WeekInfoID))
                    {
                        ToDate = realData.Max(o => o.WeekInfoID);
                    }
                    else
                    {
                        ToDate = dbWorkOrders.Max(o => o.WeekInfoID);
                    }

                    data.WeekInfoDTOs = converter.DB2DTO_WeekInfoList(context.WeekInfo.Where(o => o.WeekInfoID >= FromDate && o.WeekInfoID <= ToDate).OrderBy(o => o.WeekStart).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.SupportFormData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.SupportFormData data = new DTO.SupportFormData();
            data.WorkCenterDTOs = new List<DTO.WorkCenterDTO>();
            try
            {
                using (PlanningReportWorkCenterEntities context = CreateContext())
                {
                    data.WorkCenterDTOs = converter.DB2DTO_WorkCenterList(context.WorkCenter.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }
    }
}
