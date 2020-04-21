using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ShowroomReceiptMng : Lib.BLLBase2<DTO.ShowroomReceiptMng.SearchFormData,DTO.ShowroomReceiptMng.EditFormData,DTO.ShowroomReceiptMng.ShowroomReceipt>
    {
        private DAL.ShowroomReceiptMng.DataFactory factory = new DAL.ShowroomReceiptMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();
        public override bool Approve(int id, ref DTO.ShowroomReceiptMng.ShowroomReceipt dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.DeleteData(id, out notification);
        }
        public override DTO.ShowroomReceiptMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetData(id, out notification);
        }
        public override DTO.ShowroomReceiptMng.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public override bool Reset(int id, ref DTO.ShowroomReceiptMng.ShowroomReceipt dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public override bool UpdateData(int id, ref DTO.ShowroomReceiptMng.ShowroomReceipt dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }
    }
}
