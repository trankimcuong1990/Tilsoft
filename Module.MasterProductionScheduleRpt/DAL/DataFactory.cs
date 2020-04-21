using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Library.DTO;

namespace Module.MasterProductionScheduleRpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.ReportData, DTO.ReportData>
    {
        private DataConverter converter = new DataConverter();
        public DataFactory() { }

        private MasterProductionScheduleRptEntities CreateContext()
        {
            return new MasterProductionScheduleRptEntities(Library.Helper.CreateEntityConnectionString("DAL.MasterProductionScheduleRptModel"));
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

        public DTO.ReportData GetMasterProductionScheduleRpt(int userId, Hashtable filters, out Notification notification)
        {
            DTO.ReportData data = new DTO.ReportData();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                string workOrderUD = null;
                int? workCenterID = null; ;
                string proformaInvoiceNo = null;

                if (filters["workOrderUD"] != null) workOrderUD = filters["workOrderUD"].ToString();
                if (filters["workCenterID"] != null) workCenterID = Convert.ToInt32(filters["workCenterID"]);
                if (filters["proformaInvoiceNo"] != null) proformaInvoiceNo = filters["proformaInvoiceNo"].ToString();

                Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
                data.WorkCenters = support_factory.GetWorkCenter();
                using (MasterProductionScheduleRptEntities context = CreateContext())
                {
                    var x = context.MasterProductionScheduleRpt_MasterProductionSchedule_View.Where(o => o.WorkOrderUD.Contains(workOrderUD == null || workOrderUD == "" ? o.WorkOrderUD : workOrderUD)
                                                                                                && o.WorkCenterID == (workCenterID.HasValue ? workCenterID.Value : o.WorkCenterID)
                                                                                                && o.WorkOrderUD.Contains(workOrderUD == null || workOrderUD == "" ? o.WorkOrderUD : workOrderUD));
                    data.MasterProductionSchedules = converter.DB2DTO_MasterProductionSchedule(x.ToList()); 
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

        

    }
}
