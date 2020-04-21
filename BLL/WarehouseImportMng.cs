using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class WarehouseImportMng : Lib.BLLBase2<DTO.WarehouseImportMng.SearchFormData, DTO.WarehouseImportMng.EditFormData, DTO.WarehouseImportMng.WarehouseImport>
    {
        private DAL.WarehouseImportMng.DataFactory factory = new DAL.WarehouseImportMng.DataFactory();
        private Framework fwBLL = new Framework();

        public override DTO.WarehouseImportMng.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of warehouse import receipt");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.WarehouseImportMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get warehouse import receipt: " + id.ToString());

            // query data
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete warehouse import receipt: " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.WarehouseImportMng.WarehouseImport dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update warehouse import receipt: " + id.ToString());

            // query data
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public override bool Approve(int id, ref DTO.WarehouseImportMng.WarehouseImport dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "approve warehouse import receipt: " + id.ToString());

            // query data
            dtoItem.UpdatedBy = iRequesterID;
            return factory.Approve(id, ref dtoItem, out notification);
        }

        public override bool Reset(int id, ref DTO.WarehouseImportMng.WarehouseImport dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "approve warehouse import receipt: " + id.ToString());

            // query data
            dtoItem.UpdatedBy = iRequesterID;
            return factory.Reset(id, ref dtoItem, out notification);
        }

        // 
        // CUSTOM FUNCTIONS
        //
        public List<DTO.WarehouseImportMng.OnSeaProduct> QuickSearchOnSeaProduct(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of on sea product");

            // query data
            return factory.QuickSearchOnSeaProduct(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public List<DTO.WarehouseImportMng.OnSeaSparepart> QuickSearchOnSeaSparepart(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of on sea sparepart");

            // query data
            return factory.QuickSearchOnSeaSparepart(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.WarehouseImportMng.SearchFormData GetSearchSupport(out Library.DTO.Notification notification)
        {
            // query data
            return factory.GetSearchSupport(out notification);
        }

        public List<DTO.WarehouseImportMng.CreditNoteProduct> QuickSearchCreditNoteProduct(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchCreditNoteProduct(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public List<DTO.WarehouseImportMng.CreditNoteSparepart> QuickSearchCreditNoteSparepart(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchCreditNoteSparepart(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public string GetImportPrintout(int iRequesterID, int werehouseImportID, int version, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "warehouse import printout");

            // query data
            return factory.GetImportPrintout(werehouseImportID, version, out notification);
        }

        public List<DTO.WarehouseImportMng.ExportToWexData> GetExportWexData(int iRequesterID, int warehouseImportID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "warehouse import wex data");

            // query data
            return factory.GetExportWexData(warehouseImportID, out notification);
        }

        public string CreateWexCSVFile(int iRequesterID, int werehouseImportID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "create csv");

            // query data
            return factory.CreateWexCSVFile(werehouseImportID, out notification);
        }

        public List<DTO.Support.SaleOrder> QuickSearchSaleOrder(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DAL.Support.DataFactory support_Factory = new DAL.Support.DataFactory();
            return support_Factory.QuickSearchSaleOrder(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public List<DTO.Support.LoadingPlan> QuickSearchLoadingPlan(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DAL.Support.DataFactory support_Factory = new DAL.Support.DataFactory();
            return support_Factory.QuickSearchLoadingPlan(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public int? ImportCSVFileToWarehouseImport(List<DTO.WarehouseImportMng.WexInputData> wexData, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.ImportCSVFileToWarehouseImport(wexData, iRequesterID, out notification);
        }
    }
}
