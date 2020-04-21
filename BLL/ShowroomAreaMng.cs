using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ShowroomAreaMng : Lib.BLLBase2<DTO.ShowroomAreaMng.SearchFormData, DTO.ShowroomAreaMng.EditFormData, DTO.ShowroomAreaMng.ShowroomArea>
    {
        private DAL.ShowroomAreaMng.DataFactory factory;
        private BLL.Framework fwBLL = new Framework();

        public ShowroomAreaMng(string tempFolder)
        {
            factory = new DAL.ShowroomAreaMng.DataFactory(tempFolder);
        }
        public override bool Approve(int id, ref DTO.ShowroomAreaMng.ShowroomArea dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.DeleteData(id, out notification);
        }
        public override DTO.ShowroomAreaMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetData(id, out notification);
        }
        public override DTO.ShowroomAreaMng.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public override bool Reset(int id, ref DTO.ShowroomAreaMng.ShowroomArea dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public override bool UpdateData(int id, ref DTO.ShowroomAreaMng.ShowroomArea dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.UpdateData(id, ref dtoItem, out notification);
        }
        public string GetExportExcelFile(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetExportExcelFile(out notification);
        }
    }
}
