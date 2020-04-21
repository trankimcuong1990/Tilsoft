using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OfferMng : Lib.BLLBase<DTO.OfferMng.OfferSearchResult, DTO.OfferMng.Offer>
    {
        private DAL.OfferMng.DataFactory factory;
        private BLL.Framework fwBLL = new Framework();

        public OfferMng() {
            factory = new DAL.OfferMng.DataFactory();
        }
        public OfferMng(string tempFolder) {
            factory = new DAL.OfferMng.DataFactory(tempFolder);
        }

        public override IEnumerable<DTO.OfferMng.OfferSearchResult> GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of offer");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.OfferMng.Offer GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete offer" + id.ToString());
            return factory.DeleteOffer(id, iRequesterID, out notification);
        }

        public override bool UpdateData(int id, ref DTO.OfferMng.Offer dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();           
        }

        public DTO.OfferMng.SupportDataContainer GetSupportData(out Library.DTO.Notification notification)
        {
            return factory.GetSupportData(out notification);
        }

        public DTO.OfferMng.DataContainer GetDataContainer(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // query data
            return factory.GetDataContainer(id, iRequesterID, out notification);
        }

        public IEnumerable<DTO.OfferMng.OfferLine> SearchOfferLines(int offerID, int iRequesterID, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list offerline by offerid");
            return factory.SearchOfferLines(offerID, out totalRows, out notification);
        }

        public bool Confirm(int id, int actionType, ref DTO.OfferMng.Offer dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "confirm offer " + id.ToString());
            return factory.Confirm(id, actionType, ref dtoItem, iRequesterID, out notification);
            
        }

        public bool Revise(int id, int actionType, ref DTO.OfferMng.Offer dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "revise offer " + id.ToString());
            return factory.Revise(id, actionType, ref dtoItem, iRequesterID, out notification);
        }

        public bool UpdateData(int id,int actionType, ref DTO.OfferMng.Offer dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, actionType, iRequesterID, ref dtoItem, out notification);
        }

        public DTO.OfferMng.OfferLineEdit GetOfferLineEdit(int offerLineID, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get info about product to edit offerline " + offerLineID.ToString());

            // query data
            return factory.GetOfferLineEdit(offerLineID,  out notification);
        }

        public string GetPrintDataOffer(int offerID, int iRequesterID, out Library.DTO.Notification notification)
        {
            //DAL.OfferMng.PrintFactory print_factory = new DAL.OfferMng.PrintFactory();
            //// keep log entry
            //fwBLL.WriteLog(iRequesterID, 0, "get report offer");

            //// query data
            //return print_factory.GetPrintDataOffer(offerID, out notification);
            throw new NotImplementedException();
        }

        public string GetPrintDataOffer2(int offerID, int iRequesterID, out Library.DTO.Notification notification)
        {
            DAL.OfferMng.Print2Factory print2_factory = new DAL.OfferMng.Print2Factory();
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get report offer");

            // query data
            return print2_factory.GetPrintDataOffer2(offerID, out notification);
        }

        public string GetPrintDataOffer5(int offerID, bool IsSendEmail,bool IsGetFullSizeImage, int imageOption, int iRequesterID, out Library.DTO.Notification notification)
        {
            DAL.OfferMng.PrintFactory print_factory = new DAL.OfferMng.PrintFactory();
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get report offer 5");

            // query data
            return print_factory.GetPrintDataOffer5(offerID, IsSendEmail, IsGetFullSizeImage, imageOption, iRequesterID, out notification);
        }

        public string GetPrintDataOfferPP2013(int offerID, int iRequesterID, out Library.DTO.Notification notification)
        {
            return (new DAL.OfferMng.PrintFactory()).GetPrintDataOfferPP2013(offerID, iRequesterID, out notification);
        }

        public IEnumerable<DTO.Support.ModelSparepart> GetModelSparepart(int modelID)
        {
            return factory.GetModelSparepart(modelID);
        }

        public bool UploadOfferScanFile(int offerID, bool offerScanFileHasChange, string newOfferScanFile, string offerScanFile,out DTO.OfferMng.Offer dtoOffer, out Library.DTO.Notification notification)
        {
            return factory.UploadOfferScanFile(offerID, offerScanFileHasChange, newOfferScanFile, offerScanFile, out dtoOffer, out notification);
        }

        public DTO.OfferMng.Model GetModelInfo(int modelID, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetModelInfo(modelID, out notification);
        }

        public DTO.OfferMng.DataContainerOverView GetViewData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get offer " + id.ToString());

            // query data
            return factory.GetViewData(id, out notification);
        }

        public string GetExportNewVersion(int offerID, int iRequesterID, out Library.DTO.Notification notification)
        {
            DAL.OfferMng.Print2Factory print2Factory = new DAL.OfferMng.Print2Factory();
            return print2Factory.GetExportNewVersion(offerID, out notification);
        }

        public IEnumerable<DTO.OfferMng.SampleProductSearchResultDTO> QuickSearchSampleProduct(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchSampleProduct(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public List<DTO.OfferMng.PlaningPurchasingPriceDTO> GetPlaningPurchasingPrice(int userId, int offerLineID, out Library.DTO.Notification notification)
        {
            return factory.GetPlaningPurchasingPrice(userId, offerLineID, out notification);
        }

        public List<DTO.OfferMng.PlaningPurchasingPriceDTO> GetPlaningPurchasingPrice(int userId, object param, out Library.DTO.Notification notification)
        {
            return factory.GetPlaningPurchasingPrice(userId, param, out notification);
        }

        public DTO.OfferMng.PriceHistory GetOfferLineSalePriceHistory(int userId, string season, int offerLineID, out Library.DTO.Notification notification)
        {
            return factory.GetOfferLineSalePriceHistory(userId, season, offerLineID, out notification);
        }

        public bool ManagerApprove(int id, int userId, out Library.DTO.Notification notification)
        {
            return factory.ManagerApprove(id, userId, out notification);
        }


        public bool ApproveItem(int id, int userId, out Library.DTO.Notification notification)
        {
            return factory.ApproveItem(id, userId, out notification);
        }

        public bool UnApproveItem(int id, int userId, out Library.DTO.Notification notification)
        {
            return factory.UnApproveItem(id, userId, out notification);
        }

        public bool ApproveAllItem(int id, int userId, out Library.DTO.Notification notification)
        {
            return factory.ApproveAllItem(id, userId, out notification);
        }
        public string GetExcelFOBItemOnlyReportData(int offerID, int iRequesterID, out Library.DTO.Notification notification)
        {
            DAL.OfferMng.FOBItemOnlyFactory print2Factory = new DAL.OfferMng.FOBItemOnlyFactory();
            return print2Factory.GetExcelFOBItemOnlyReportData(offerID, out notification);
        }
        public string ExportExcelSearchOffer(System.Collections.Hashtable filters,  out Library.DTO.Notification notification)
        {
            DAL.OfferMng.FOBItemOnlyFactory print2Factory = new DAL.OfferMng.FOBItemOnlyFactory();
            return print2Factory.ExportExcelSearchOffer(filters, out notification);
        }

        public bool SendEmailNotificationByApproveOfferPermission(int id, int userId, out Library.DTO.Notification notification)
        {
            return factory.SendEmailNotificationByApproveOfferPermission(id, userId, out notification);
        }

        public string ExportOfferDetailToExcel(int id, out Library.DTO.Notification notification)
        {
            DAL.OfferMng.DataFactory factory = new DAL.OfferMng.DataFactory();
            return factory.ExportOfferDetailToExcel(id, out notification);
        }
    }
}
