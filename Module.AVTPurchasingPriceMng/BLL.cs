﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AVTPurchasingPriceMng
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
            throw new NotImplementedException();
            //return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
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
            throw new NotImplementedException();
            //return factory.GetInitData(out notification);
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.SupportFormData GetSearchFilter(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetSearchFilter(out notification);
        }

        public bool UpdateData(int iRequesterID, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateData(iRequesterID, dtoItem, out notification);
        }

        public bool ConfirmPrice(int iRequesterID, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.ConfirmPrice(iRequesterID, dtoItem, out notification);
        }

        public bool UnConfirmPrice(int iRequesterID, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UnConfirmPrice(iRequesterID, dtoItem, out notification);
        }

        public List<DTO.OfferHistoryDTO> GetHistoryData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetHistoryData(id, out notification);
        }

        public List<DTO.PurchasingPriceHistoryDTO> GetPurchasingPriceHistoryData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetPurchasingPriceHistoryData(id, out notification);
        }

        public List<DTO.GeneralBreakDownDTO> GetGeneralBreakDownData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetGeneralBreakDownData(id, out notification);
        }

        public List<DTO.PALBreakDownDTO> GetPALBreakDownData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetPALBreakDownData(id, out notification);
        }

        public string GetExcelReport(Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetExcelReport(filters, out notification);
        }
        public List<DTO.FactoryQuotationSearchResultDTO> GetDataWithFilterAdditional(string season, string ids, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilterAdditional(season, ids, out notification);
        }
    }
}
