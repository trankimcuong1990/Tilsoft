using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class FactoryMng : Lib.BLLBase2<DTO.FactoryMng.SearchFormData, DTO.FactoryMng.EditFormData, DTO.FactoryMng.Factory>
    {
        private DAL.FactoryMng.DataFactory factory = new DAL.FactoryMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();

        public override DTO.FactoryMng.SearchFormData GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of factories");
            return factory.GetDataWithFilter(new Hashtable(), pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.FactoryMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get factory " + id.ToString());
            //return factory.GetData(id, out notification);
            return factory.GetData(id, iRequesterID, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete factory " + id.ToString());
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.FactoryMng.Factory dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update factory " + id.ToString());
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public override bool Approve(int id, ref DTO.FactoryMng.Factory dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.FactoryMng.Factory dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
