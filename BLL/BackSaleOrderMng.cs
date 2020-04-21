using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class BackSaleOrderMng : Lib.BLLBase<DTO.BackSaleOrderMng.SaleOrderSearch, DTO.BackSaleOrderMng.EditFormData>
    {
        private DAL.BackSaleOrderMng.DataFactory factory = new DAL.BackSaleOrderMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();

        public override IEnumerable<DTO.BackSaleOrderMng.SaleOrderSearch> GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.BackSaleOrderMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new Exception();
        }

        public override bool UpdateData(int id, ref DTO.BackSaleOrderMng.EditFormData dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new Exception();
        }

        public DTO.BackSaleOrderMng.SearchFilterData GetSearchFilter()
        {
            return factory.GetSearchFilter();
        }

        public IEnumerable<DTO.BackSaleOrderMng.GoodsRemaining> GetGoodsRemaining(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetGoodsRemaining(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool UpdateBackOrder(int id,ref DTO.BackSaleOrderMng.BackOrder dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateBackOrder(id,ref dtoItem, out notification);
        }

    }
}
