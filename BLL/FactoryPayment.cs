using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class FactoryPaymentMng : Lib.BLLBase2<DTO.FactoryPaymentMng.SearchData, DTO.FactoryPaymentMng.EditData, DTO.FactoryPaymentMng.FactoryPayment>
    {
        private DAL.FactoryPaymentMng.DataFactoryPayment factory = new DAL.FactoryPaymentMng.DataFactoryPayment();
        private BLL.Framework fwBLL = new Framework();

        public override DTO.FactoryPaymentMng.SearchData GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.FactoryPaymentMng.EditData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.FactoryPaymentMng.FactoryPayment dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public override bool Approve(int id, ref DTO.FactoryPaymentMng.FactoryPayment dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.FactoryPaymentMng.FactoryPayment dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public string GetReportOutStanding(string season,int supplierID, out Library.DTO.Notification notification)
        {
            return factory.GetGetOutStandingBalance(season,supplierID,out notification );
        }
    }
}
