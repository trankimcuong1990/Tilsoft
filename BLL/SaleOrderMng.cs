using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class SaleOrderMng : Lib.BLLBase<DTO.SaleOrderMng.OfferSearch, DTO.SaleOrderMng.SaleOrder>
    {
        private DAL.SaleOrderMng.DataFactory factory = new DAL.SaleOrderMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();
        private string tempFolder;

        public SaleOrderMng() { }
        public SaleOrderMng(string tempFolder) { this.tempFolder = tempFolder; }

        public override IEnumerable<DTO.SaleOrderMng.OfferSearch> GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of offer");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.SaleOrderMng.SaleOrder GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get PI " + id.ToString());
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete eurofar PI " + id.ToString());
            return factory.DeletePI(id, iRequesterID, out notification);
        }

        public override bool UpdateData(int id, ref DTO.SaleOrderMng.SaleOrder dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update proforma invoice " + id.ToString());
            dtoItem.UpdatedBy = iRequesterID;
            dtoItem.UpdatedDate = DateTime.Now;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public DTO.SaleOrderMng.DataContainer GetDataContainer(int id,int offerID, string orderType, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get proforma invoice " + id.ToString());

            // query data
            return factory.GetDataContainer(id, offerID, orderType, out notification);
        }

        public DTO.SaleOrderMng.PIFormContainerDTO SearchSaleOrders(int offerID, int iRequesterID, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list saleorder by offerid");
            return factory.SearchSaleOrders(offerID, out totalRows, out notification);
        }

        public IEnumerable<DTO.SaleOrderMng.SaleOrderDetailSearch> SearchSaleOrderDetails(int saleOrderID, int iRequesterID, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list saleorderdetail by saleorderid");
            return factory.SearchSaleOrderDetails(saleOrderID,out totalRows, out notification);
        }

        public bool Confirm(int id, ref DTO.SaleOrderMng.SaleOrder dtoItem, int iRequesterID, bool isConfirmWithoutSigned, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "confirm order " + id.ToString());
            return factory.Confirm(id, ref dtoItem, iRequesterID, isConfirmWithoutSigned, out notification);
        }

        public bool Revise(int id, ref DTO.SaleOrderMng.SaleOrder dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "revise order " + id.ToString());
            return factory.Revise(id, ref dtoItem, iRequesterID, out notification);
        }

        public DTO.SaleOrderMng.DataSearchContainer SearchDataContainer(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of PI");
            return factory.SearchDataContainer(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.SaleOrderMng.PIContainerPrintout GetPrintoutData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get invoice printout");
            return factory.GetPrintoutData(id, out  notification);
        }

        public string GetReportOrderOverview (string Season, string OrderType, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get report order overview");

            // query data
            return factory.GetReportOrderOverview(Season, OrderType, out notification);
        }

        public bool ValidateStockQuantity(int saleOrderID,DTO.SaleOrderMng.SaleOrder dtoItem,int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "validate quantity in stock");

            // query data
            return factory.ValidateStockQuantity(saleOrderID,dtoItem, out notification);
        }

        public IEnumerable<int> MultiConfirm(List<int> ids, int iRequesterID, bool isConfirmWithoutSigned, out Library.DTO.Notification notification)
        {
            return factory.MultiConfirm(ids, iRequesterID, isConfirmWithoutSigned, out notification);
        }

        public IEnumerable<int> MultiRevise(List<int> ids, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.MultiRevise(ids, iRequesterID, out notification);
        }

        public IEnumerable<int> MultiDelete(List<int> ids, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.MultiDelete(ids, iRequesterID, out notification);
        }

        public IEnumerable<DTO.SaleOrderMng.LoadingPlan> GetLoadingPlan(int saleOrderID, out Library.DTO.Notification notification)
        {
            DAL.SaleOrderMng.DataFactorySaleOrderReturn factory = new DAL.SaleOrderMng.DataFactorySaleOrderReturn();
            return factory.GetLoadingPlan(saleOrderID, out notification);
        }

        public bool CreateReturnData(DTO.SaleOrderMng.SaleOrder dtoItem, out Library.DTO.Notification notification)
        {
            DAL.SaleOrderMng.DataFactorySaleOrderReturn factory = new DAL.SaleOrderMng.DataFactorySaleOrderReturn();
            return factory.CreateReturnData(dtoItem, out notification);
        }

        public int? GetOfferTrackingStatus(int? offerID, out Library.DTO.Notification notification)
        {
            return factory.GetOfferTrackingStatus(offerID, out notification);
        }

        public IEnumerable<DTO.SaleOrderMng.LoadingPlan2> GetLoadingPlan2(int saleOrderID, out Library.DTO.Notification notification)
        {
            DAL.SaleOrderMng.DataFactoryReturnGoods factory = new DAL.SaleOrderMng.DataFactoryReturnGoods();
            return factory.GetLoadingPlan(saleOrderID, out notification);
        }

        public bool CreateReturnData2(List<DTO.SaleOrderMng.LoadingPlan2> dtoReturns, out Library.DTO.Notification notification)
        {
            DAL.SaleOrderMng.DataFactoryReturnGoods factory = new DAL.SaleOrderMng.DataFactoryReturnGoods();
            return factory.CreateReturnData(dtoReturns, out notification);
        }

        public bool UploadSignedPIFile(int saleOrderID, string newFile, string oldPointer, out DTO.SaleOrderMng.SaleOrder dtoSaleOrder, out Library.DTO.Notification notification)
        {
            DAL.SaleOrderMng.DataFactory file_factory = new DAL.SaleOrderMng.DataFactory(this.tempFolder);
            return file_factory.UploadSignedPIFile(saleOrderID, newFile, oldPointer, out dtoSaleOrder, out notification);
        }

        public bool UploadClientPOFile(int saleOrderID, string newFile, string oldPointer, out DTO.SaleOrderMng.SaleOrder dtoSaleOrder, out Library.DTO.Notification notification)
        {
            DAL.SaleOrderMng.DataFactory file_factory = new DAL.SaleOrderMng.DataFactory(this.tempFolder);
            return file_factory.UploadClientPOFile(saleOrderID, newFile, oldPointer, out dtoSaleOrder, out notification);
        }
        public bool UploadLCFile(int saleOrderID, string newFile, string oldPointer, out DTO.SaleOrderMng.SaleOrder dtoSaleOrder, out Library.DTO.Notification notification)
        {
            DAL.SaleOrderMng.DataFactory file_factory = new DAL.SaleOrderMng.DataFactory(this.tempFolder);
            return file_factory.UploadLCFile(saleOrderID, newFile, oldPointer, out dtoSaleOrder, out notification);
        }
        public DTO.SaleOrderMng.DataContainerOverview GetViewData(int id, int offerID, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get proforma invoice " + id.ToString());

            // query data
            return factory.GetViewData(id, offerID, out notification);
        }


        public List<DTO.SaleOrderMng.SaleOrderDetail> GetOfferLine(int offerId, string orderType, string searchArt, out Library.DTO.Notification notification)
        {
            return factory.GetOfferLine(offerId, orderType, searchArt, out notification);
        }

        public List<DTO.SaleOrderMng.SaleOrderDetailSparepart> GetOfferLineSparepart(int offerId, string orderType, string searchArtSparepart, out Library.DTO.Notification notification)
        {
            return factory.GetOfferLineSparepart(offerId, orderType, searchArtSparepart, out notification);
        }
        
        public List<DTO.SaleOrderMng.SaleOrderDetailSample> GetOfferLineSample(int offerId, string orderType, string searchArtSample, out Library.DTO.Notification notification)
        {
            return factory.GetOfferLineSample(offerId, orderType, searchArtSample, out notification);
        }

        public List<DTO.SaleOrderMng.BizBloqsInvoice> BizBloqsImportData(List<DTO.SaleOrderMng.BizBloqsInvoice> bizBloqsInvoice, int? userId, out Library.DTO.Notification notification)
        {
            return factory.BizBloqsImportData(bizBloqsInvoice, userId, out notification);
        }

        public bool SaveLDSClient(int id, string ldsDate, string remark, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.SaveLDSClient(id, ldsDate, iRequesterID, remark, out notification);
        }
    }
}
