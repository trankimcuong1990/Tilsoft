using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TransferShowroomAreaMng : Lib.BLLBase2<List<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch>, DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch, DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch>
    {
        private DAL.TransferShowroomAreaMng.DataFactory factory;
        private BLL.Framework fwBLL = new Framework();

        public TransferShowroomAreaMng() {
            factory = new DAL.TransferShowroomAreaMng.DataFactory();
        }
       
        public override bool Approve(int id, ref DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.DeleteData(id, out notification);
        }
        public override DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetData(id, out notification);
        }
        public override List<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch> GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public override bool Reset(int id, ref DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public override bool UpdateData(int id, ref DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            dtoItem.TransferBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public bool TransferMultiItem(List<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch> dtoItem, int iRequesterID, out List<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch> errorItems, out Library.DTO.Notification notification)
        {
            foreach (var item in dtoItem)
            {
                item.TransferBy = iRequesterID;
            }
            return factory.TransferMultiItem(dtoItem, out errorItems, out notification);
        }
    }
}
