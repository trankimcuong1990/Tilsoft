using Module.OfferToClientMng.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferToClientMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {            
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {           
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return factory.Approve(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool Reset(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return factory.Reset(iRequesterID, id, ref dtoItem, out notification);
        }

        public DTO.SupportFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {            
            return factory.GetInitData(out notification);
        }
        public DTO.EditFormData GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            return factory.GetData(id, out notification);
        }
        public DTO.EditFormData GetDataByClient(int clientID, string season, out Library.DTO.Notification notification)
        {
            return factory.GetDataByClient(clientID, season, out notification);
        }
      
        public OfferSeasonDetailSearchFormData QuickSearchOfferLine(string articleCode, string description, int clientID, string season, string currency, int offerLineType, int pageSize, int pageIndex, string orderBy, string orderDirection, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchOfferLine(articleCode, description, clientID, season, currency, offerLineType, pageSize, pageIndex, orderBy, orderDirection, out notification);
        }      
        public bool DeleteOffer(int deletedBy, int id, out Library.DTO.Notification notification)
        {
            return factory.DeleteOffer(deletedBy, id, out notification);
        }
        public string GetPrintDataOffer5(int offerID, bool IsSendEmail, bool IsGetFullSizeImage, int imageOption, int iRequesterID, out Library.DTO.Notification notification)
        { 
            return factory.GetPrintDataOffer5(offerID, IsSendEmail, IsGetFullSizeImage, imageOption, iRequesterID, out notification);
        }

        public string GetPrintDataOfferPP2013(int offerID, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetPrintDataOfferPP2013(offerID, iRequesterID, out notification);
        }
        public string GetExcelFOBItemOnlyReportData(int id, out Library.DTO.Notification notification)
        {
            return factory.GetExcelFOBItemOnlyReportData(id, out notification);
        }
        public string GetExportNewVersion(int offerID, out Library.DTO.Notification notification)
        {
            return factory.GetExportNewVersion(offerID, out notification);
        }
        public bool UpdateClientInfomation(int offerID, out Library.DTO.Notification notification)
        {
            return factory.UpdateClientInfomation(offerID, out notification);
        }
    }
}
