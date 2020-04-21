using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class QuotationMng : Lib.BLLBase2<DTO.QuotationMng.SearchFormData, DTO.QuotationMng.EditFormData, DTO.QuotationMng.Quotation>
    {
        private DAL.QuotationMng.DataFactory factory = new DAL.QuotationMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();

        public override DTO.QuotationMng.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of quotation");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public async Task<DTO.QuotationMng.SearchFormData> GetDataWithFilterAsync(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection)
        {
            // query data
            return await factory.GetDataWithFilterAsync(filters, pageSize, pageIndex, orderBy, orderDirection);
        }

        public override DTO.QuotationMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete quotation" + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.QuotationMng.Quotation dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update Quotation " + id.ToString());

            // query data
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public override bool Approve(int id, ref DTO.QuotationMng.Quotation dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.QuotationMng.Quotation dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.QuotationMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            return factory.GetFilterData(out notification);
        }

        public DTO.QuotationMng.InitFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(iRequesterID, out notification);
        }

        public DTO.QuotationMng.EditFormData GetData(int id, int factoryID, string season, List<int> orders, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get Quotation " + id.ToString());

            // query data
            return factory.GetData(id, factoryID, season, orders, iRequesterID, out notification);
        }

        //
        // REPORT FUNCTION
        //
        public string GetFactoryQuotationReportData(int QuotationID, out Library.DTO.Notification notification)
        {
            return factory.GetFactoryQuotationReportData(QuotationID, out notification);
        }

        public string GetEurofarQuotationReportData(int QuotationID, out Library.DTO.Notification notification)
        {
            return factory.GetEurofarQuotationReportData(QuotationID, out notification);
        }
    }
}
