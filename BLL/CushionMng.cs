using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class CushionMng : Lib.BLLBase2<DTO.CushionMng.SearchFormData, DTO.CushionMng.EditFormData, DTO.CushionMng.Cushion>
    {
        private DAL.CushionMng.DataFactory factory;
        private BLL.Framework fwBLL = new Framework();

        public CushionMng(string tempFolder)
        {
            factory = new DAL.CushionMng.DataFactory(tempFolder);
        }
        public override DTO.CushionMng.SearchFormData GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of cushion");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.CushionMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get cushion " + id.ToString());

            // query data
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete cushion " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.CushionMng.Cushion dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update cushoin " + id.ToString());

            // query data
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public override bool Approve(int id, ref DTO.CushionMng.Cushion dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.CushionMng.Cushion dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.CushionMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            return factory.GetFilterData(out notification);
        }
    }
}
