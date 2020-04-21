using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class ECommercialInvoiceMng : Lib.BLLBase<DTO.ECommercialInvoiceMng.ECommercialInvoiceSearchResult, DTO.ECommercialInvoiceMng.ECommercialInvoice>
    {
        private DAL.ECommercialInvoiceMng.DataFactory factory = new DAL.ECommercialInvoiceMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();

        public override IEnumerable<DTO.ECommercialInvoiceMng.ECommercialInvoiceSearchResult> GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of eurofar commercial invoice");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.ECommercialInvoiceMng.ECommercialInvoice GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get eurofar commercial invoice " + id.ToString());
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete eurofar commercial invoice " + id.ToString());
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.ECommercialInvoiceMng.ECommercialInvoice dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update eurofar commercial invoice " + id.ToString());
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public DTO.ECommercialInvoiceMng.DataContainer GetDataContainer(int id, int? typeOfInvoice, int? internalCompanyID, int? clientID, int? parentID, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get eurofar commercial invoice " + id.ToString());

            // query data
            return factory.GetDataContainer(id, typeOfInvoice, internalCompanyID, clientID, parentID, out notification);
        }

        public IEnumerable<DTO.ECommercialInvoiceMng.PurchasingInvoice> GetPurchasingInvoice(int clientID, int iRequesterID, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list purchasing invoice");
            return factory.GetPurchasingInvoice(clientID, out totalRows, out notification);
        }

        public DTO.ECommercialInvoiceMng.InvoiceContainerPrintout GetInvoicePrintoutData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get invoice printout");
            return factory.GetInvoicePrintoutData(id, out  notification);
        }



        public DTO.ECommercialInvoiceMng.PackingListContainerPrintout GetPackingListPrintoutData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get packinglist printout");
            return factory.GetPackingListPrintoutData(id, out  notification);
        }

        public List<DTO.ECommercialInvoiceMng.PackingListContainerPrintout> GetPackingListPrintoutData_PerCont(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get packinglist printout per container");
            return factory.GetPackingListPrintoutData_PerCont(id, out notification);
        }

        //public bool GenerateWarehouseInvoice(int WarehousePickingPlanID, out Library.DTO.Notification notification)
        //{
        //    return factory.GenerateWarehouseInvoice(WarehousePickingPlanID, out notification);
        //}

        public IEnumerable<DTO.ECommercialInvoiceMng.WarehousePickingList> GetWarehousePickingList(int clientID, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list picking list that is confirmed");
            return factory.GetWarehousePickingList(clientID, out notification);
        }

        //public IEnumerable<DTO.ECommercialInvoiceMng.InitWarehouseInfo> GetInitWarehouseInfos(int iRequesterID, out int totalRows, out Library.DTO.Notification notification)
        //{
        //    // keep log entry
        //    fwBLL.WriteLog(iRequesterID, 0, "get list init warehouse info");
        //    return factory.GetInitWarehouseInfos(out totalRows, out notification);
        //}

        //public IEnumerable<DTO.ECommercialInvoiceMng.InitFobInvoice> GetInitFobInvoices(int iRequesterID, out int totalRows, out Library.DTO.Notification notification)
        //{
        //    // keep log entry
        //    fwBLL.WriteLog(iRequesterID, 0, "get list init fob info");
        //    return factory.GetInitFobInvoices(out totalRows, out notification);
        //}
        public DTO.ECommercialInvoiceMng.InitData GetInitData()
        {
            return factory.GetInitData();
        }

        public bool ConfirmInvoice(int id,bool isConfirmed,int iRequesterID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, isConfirmed ? "confirm invoice" : "un-confirm invoice");
            return factory.ConfirmInvoice(id, isConfirmed, iRequesterID, out notification);
        }

        public IEnumerable<DTO.ECommercialInvoiceMng.InitFobInvoice> GetInitFobInvoice(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetInitFobInvoice(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public IEnumerable<DTO.ECommercialInvoiceMng.InitWarehouseInfo> GetInitWarehouseInvoice(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetInitWarehouseInvoice(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public IEnumerable<DTO.ECommercialInvoiceMng.Client> GetClient(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetClient(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public string GetReportExportExcelInvoice(string season, out Library.DTO.Notification notification)
        {
            return factory.GetReportExportExcelInvoice(season, out notification);
        }

        public DTO.ECommercialInvoiceMng.ReturnGoods GetReturnGoods(int eCommercialInvoiceID, out Library.DTO.Notification notification)
        {
            DAL.ECommercialInvoiceMng.DataFactoryReturnGoods factory_returnGoods = new DAL.ECommercialInvoiceMng.DataFactoryReturnGoods();
            return factory_returnGoods.GetReturnGoods(eCommercialInvoiceID, out notification);
        }
        public int CreateReturGoods(DTO.ECommercialInvoiceMng.ReturnGoods dtoReturn, out Library.DTO.Notification notification)
        {
            DAL.ECommercialInvoiceMng.DataFactoryReturnGoods factory_returnGoods = new DAL.ECommercialInvoiceMng.DataFactoryReturnGoods();
            return factory_returnGoods.CreateReturGoods(dtoReturn, out notification);
        }

        public bool SetDonePayment(int eCommercialInvoiceID, bool isDonePayment, out DTO.ECommercialInvoiceMng.ECommercialInvoice dtoItem, out Library.DTO.Notification notification)
        {
            return factory.SetDonePayment(eCommercialInvoiceID, isDonePayment, out dtoItem, out notification);
        }

        public string ExportExactOnlineSoftware(string ecommercialInvoiceIds, out Library.DTO.Notification notification)
        {
            return factory.ExportExactOnlineSoftware(ecommercialInvoiceIds, out notification);
        }

        public DTO.ECommercialInvoiceMng.SearchFormData SearchECommercialInvoice(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of eurofar commercial invoice");
            return factory.SearchECommercialInvoice(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public string GetInvoicePrintoutInExcel(int eCommercialInvoiceID, string reportName, int invoiceType, out Library.DTO.Notification notification)
        {
            return factory.GetInvoicePrintoutInExcel(eCommercialInvoiceID, reportName, invoiceType, out notification);
        }

        public string GetPEUackingListPrintoutData(int eCommercialInvoiceID, string reportName, out Library.DTO.Notification notification)
        {
            return factory.GetPEUackingListPrintoutData(eCommercialInvoiceID, reportName, out notification);
        }

        public string GetEUPackingListPrintoutData_PerCont(int eCommercialInvoiceID, string reportName, out Library.DTO.Notification notification)
        {
            return factory.GetEUPackingListPrintoutData_PerCont(eCommercialInvoiceID, reportName, out notification);
        }

        public string GetOrangePiePrintout(int eCommercialInvoiceID, out Library.DTO.Notification notification)
        {
            return factory.GetOrangePiePrintout(eCommercialInvoiceID, out notification);
        }
        public List<DTO.ECommercialInvoiceMng.BillTransport> GetBillTransport(Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetBillTransport(filters, out notification);
        }

        public DTO.ECommercialInvoiceMng.DataOverview GetDataOverview(int id, int? typeOfInvoice, int? internalCompanyID, int? clientID, int? parentID, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get eurofar commercial invoice " + id.ToString());

            // query data
            return factory.GetDataOverview(id, typeOfInvoice, internalCompanyID, clientID, parentID, out notification);
        }

        public List<DTO.ECommercialInvoiceMng.ECommercialInvoicePurchasing> GetPurchasingQnt(int id, out Library.DTO.Notification notification)
        {
            return factory.GetPurchasingQnt(id, out notification);
        }

        public List<DTO.ECommercialInvoiceMng.BizBloqsInvoice> BizBloqsImportData(List<DTO.ECommercialInvoiceMng.BizBloqsInvoice> bizBloqsInvoice, int userId, out Library.DTO.Notification notification)
        {
            return factory.BizBloqsImportData(bizBloqsInvoice, userId, out notification);
        }
    }
}
   