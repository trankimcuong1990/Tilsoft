using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrderRpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();
        private Module.Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        private WorkOrderRptEntities CreateContext()
        {
            return new WorkOrderRptEntities(Library.Helper.CreateEntityConnectionString("DAL.WorkOrderRptModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTIONS
        //
        public DTO.ReportFormData GetReportData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ReportFormData data = new DTO.ReportFormData();
            data.Data = new DTO.WorkOrder();
            data.OPSequenceDetails = new List<DTO.OPSequenceDetail>();
            data.ProductionTeams = new List<DTO.ProductionTeam>();
            data.FactoryWarehouses = new List<DTO.FactoryWarehouse>();
            data.Details = new List<DTO.Detail>();
            data.Receipts = new List<DTO.Receipt>();

            data.ReceiptOverviews = new List<DTO.ReceiptOverview>();
            data.ReceiptOverviewDetails = new List<DTO.ReceiptOverviewDetail>();

            //try to get data
            try
            {
                using (WorkOrderRptEntities context = CreateContext())
                {
                    int companyId = fwFactory.GetCompanyID(userId).Value;
                    data.Data = converter.DB2DTO_WorkOrder(context.WorkOrderRpt_WorkOrder_View.SingleOrDefault(o => o.WorkOrderID == id && o.CompanyID == companyId));
                    data.OPSequenceDetails = converter.DB2DTO_OPSequenceDetail(context.WorkOrderRpt_OPSequenceDetail_View.Where(o => o.WorkOrderID == id && o.CompanyID == companyId).ToList());
                    data.ProductionTeams = converter.DB2DTO_ProductionTeam(context.WorkOrderRpt_ProductionTeamBySequenceDetail_View.Where(o => o.WorkOrderID == id && o.CompanyID == companyId).ToList());
                    data.FactoryWarehouses = converter.DB2DTO_FactoryWarehouse(context.WorkOrderRpt_FactoryWarehouseBySequenceDetail_View.Where(o => o.WorkOrderID == id && o.CompanyID == companyId).ToList());
                    data.Details = converter.DB2DTO_Detail(context.WorkOrderRpt_InOutDetail_View.Where(o => o.WorkOrderID == id && o.CompanyID == companyId).ToList());
                    data.Receipts = converter.DB2DTO_Receipt(context.WorkOrderRpt_InOutReceipt_View.Where(o => o.WorkOrderID == id && o.CompanyID == companyId).ToList());

                    data.ReceiptOverviews = converter.DB2DTO_ReceiptOverview(context.WorkOrderRpt_ReceiptOverview_View.Where(o => o.WorkOrderID == id && o.CompanyID == companyId).OrderBy(o=>o.ReceiptDate).ToList());
                    data.ReceiptOverviewDetails = converter.DB2DTO_ReceiptOverviewDetail(context.WorkOrderRpt_ReceiptOverviewDetail_View.Where(o => o.WorkOrderID == id && o.CompanyID == companyId).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
    }
}
