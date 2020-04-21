using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SaleOrderMng
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();
        private string tempFolder;
        public BLL() { }
        public BLL(string tempFolder) { this.tempFolder = tempFolder; }

        public DTO.SearchFormData GetDataWithFilter(int iReiRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderedBy, string orderedDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilters(filters, pageSize, pageIndex, orderedBy, orderedDirection, out totalRows, out notification);
        }
        public DTO.DataContainer GetDataContainer(int id, int offerID, string orderType, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get proforma invoice " + id.ToString());

            // query data
            return factory.GetDataContainer(id, offerID, orderType, out notification);
        }

        public bool UpdateData(int id, ref object dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update proforma invoice " + id.ToString());

            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete eurofar PI " + id.ToString());
            return factory.DeletePI(id, iRequesterID, out notification);
        }

        public List<DTO.SaleOrderDetailOTC> GetOfferLine(int offerId, string orderType, out Library.DTO.Notification notification)
        {
            return factory.GetOfferLine(offerId, orderType, out notification);
        }

        public List<DTO.SaleOrderDetailOTCSparepart> GetOfferLineSparepart(int offerId, string orderType, out Library.DTO.Notification notification)
        {
            return factory.GetOfferLineSparepart(offerId, orderType, out notification);
        }

        public bool Confirm(int SaleOrderID, ref object dtoItem, int iRequesterID, bool withoutSigned, out Library.DTO.Notification notification)
        {
            return factory.Confirm(SaleOrderID, ref dtoItem, iRequesterID, withoutSigned, out notification);
        }

        public bool Revise(int SaleOrderID, ref object dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.Revise(SaleOrderID, ref dtoItem, iRequesterID, out notification);
        }

        public DTO.SaleOrder UploadSignedPIFile(int saleOrderID, string newFile, string oldPointer, string tempFolder, out Library.DTO.Notification notification)
        {
            DAL.DataFactory file_factory = new DAL.DataFactory(this.tempFolder);
            return file_factory.UploadSignedPIFile(saleOrderID, newFile, oldPointer, tempFolder, out notification);
        }

        public DTO.SaleOrder UploadClientPOFile(int saleOrderID, string newFile, string oldPointer, string tempFolder, out Library.DTO.Notification notification)
        {
            DAL.DataFactory file_factory = new DAL.DataFactory(this.tempFolder);
            return file_factory.UploadClientPOFile(saleOrderID, newFile, oldPointer, tempFolder, out notification);
        }

        public string GetPrintoutData(int saleOrderID, string reportName, out Library.DTO.Notification notification)
        {
            return factory.GetPrintoutData(saleOrderID, reportName, out notification);
        }

        public List<DTO.LoadingPlan2> GetLoadingPlan2(int saleOrderID, out Library.DTO.Notification notification)
        {
            DAL.DataFactoryReturnGoods factory = new DAL.DataFactoryReturnGoods();
            return factory.GetLoadingPlan(saleOrderID, out notification);
        }

        public bool CreateReturnData(object dtoItem, out Library.DTO.Notification notification)
        {
            DAL.DataFactoryReturnGoods factory = new DAL.DataFactoryReturnGoods();
            return factory.CreateReturnData(dtoItem, out notification);
        }
    }
}
