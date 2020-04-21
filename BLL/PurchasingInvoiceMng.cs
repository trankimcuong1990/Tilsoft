using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class PurchasingInvoiceMng : Lib.BLLBase<DTO.PurchasingInvoiceMng.PurchasingInvoiceSearch, DTO.PurchasingInvoiceMng.PurchasingInvoice>
    {
        private DAL.PurchasingInvoiceMng2.DataFactory factory;
        private BLL.Framework fwBLL = new Framework();

        public PurchasingInvoiceMng()
        {
            factory = new DAL.PurchasingInvoiceMng2.DataFactory();
        }
        
        public override IEnumerable<DTO.PurchasingInvoiceMng.PurchasingInvoiceSearch> GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of PurchasingInvoice");

            //assign userID
            filters["iRequesterID"] = iRequesterID;

            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.PurchasingInvoiceMng.PurchasingInvoice GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get PurchasingInvoice " + id.ToString());
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete PurchasingInvoice " + id.ToString());
            return factory.DeleteData(iRequesterID, id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.PurchasingInvoiceMng.PurchasingInvoice dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update PurchasingInvoice " + id.ToString());
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public DTO.PurchasingInvoiceMng.SearchSupportList GetSearchSupportData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetSearchSupportData(iRequesterID, out notification);
        }
        
        public DTO.PurchasingInvoiceMng.EditSupportList GetEditSupportData()
        {
            return factory.GetEditSupportData();
        }
        
        public IEnumerable<DTO.PurchasingInvoiceMng.Booking> GetBookings(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of booking");

            return factory.GetBookings(iRequesterID,filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public IEnumerable<DTO.PurchasingInvoiceMng.LoadingPlanDetail> GetLoadingPlanDetails(int iRequesterID, int bookingID, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetLoadingPlanDetails(iRequesterID, bookingID, out totalRows, out notification);
        }

        public IEnumerable<DTO.PurchasingInvoiceMng.LoadingPlanSparepartDetail> GetLoadingPlanSparepartDetails(int iRequesterID, int bookingID, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetLoadingPlanSparepartDetails(iRequesterID, bookingID, out totalRows, out notification);
        }

        public DTO.PurchasingInvoiceMng.PurchasingInvoice GetEditData(int iRequesterID, int id, int invoiceType, int bookingID, int supplierID, int parentID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "get PurchasingInvoice " + id.ToString());
            return factory.GetEditData(iRequesterID, id, invoiceType, bookingID, supplierID, parentID, out notification);
        }
        public bool SetStatus(int id, int iRequesterID, bool status, int invoiceType, out Library.DTO.Notification notification)
        {
            return factory.SetStatus(id, iRequesterID,status, invoiceType, out notification);
        }

        public string GetReportData(int iRequesterID, int purchasingInvoiceID, out Library.DTO.Notification notification)
        {
            DAL.PurchasingInvoiceMng2.ReportFactory reportfactory = new DAL.PurchasingInvoiceMng2.ReportFactory();
            return reportfactory.GetReportData(iRequesterID, purchasingInvoiceID, out notification);
        }

        public DTO.PurchasingInvoiceMng.InitData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(iRequesterID, out notification);
        }

        public DTO.PurchasingInvoiceMng.SearchFormData GetSearchData(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of PurchasingInvoice");

            return factory.GetSearchData(iRequesterID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public string ExportExactOnlineSoftware(int iRequesterID, string purchasingInvoiceIds, out Library.DTO.Notification notification)
        {
            DAL.PurchasingInvoiceMng2.ReportFactory reportfactory = new DAL.PurchasingInvoiceMng2.ReportFactory();
            return reportfactory.ExportExactOnlineSoftware(iRequesterID, purchasingInvoiceIds, out notification);
        }

        public bool MarkAsExportedToExact(int iRequesterID, List<int> purchasingInvoiceIDs, out Library.DTO.Notification notification)
        {
            return factory.MarkAsExportedToExact(purchasingInvoiceIDs, out notification);
        }

        public bool MarkAsNotYetExportedToExact(int iRequesterID, List<int> purchasingInvoiceIDs, out Library.DTO.Notification notification)
        {
            return factory.MarkAsNotYetExportedToExact(iRequesterID, purchasingInvoiceIDs, out notification);
        }

        public  bool SetConfirmPrice(int iRequesterID, int purchasingInvoiceID, DTO.PurchasingInvoiceMng.PurchasingInvoice dtoItem, bool isConfirmedPrice, out Library.DTO.Notification notification)
        {
            return factory.SetConfirmPrice(iRequesterID, purchasingInvoiceID, dtoItem, isConfirmedPrice, out notification);
        }

        public object GetPrintoutData(int iRequesterID, int purchasingInvoiceID, int optionID, out Library.DTO.Notification notification)
        {
            DAL.PurchasingInvoiceMng2.ReportFactory reportFactory = new DAL.PurchasingInvoiceMng2.ReportFactory();
            return reportFactory.GetPrintoutData(iRequesterID, purchasingInvoiceID, optionID, out notification);
        }
    }
}
