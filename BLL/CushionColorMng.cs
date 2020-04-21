using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class CushionColorMng : Lib.BLLBase2<DTO.CushionColorMng.SearchFormData, DTO.CushionColorMng.EditFormData, DTO.CushionColorMng.CushionColor>
    {
        private DAL.CushionColorMng.DataFactory factory;
        private BLL.Framework fwBLL = new Framework();

        public CushionColorMng(string tempFolder)
        {
            factory = new DAL.CushionColorMng.DataFactory(tempFolder);
        }

        public override DTO.CushionColorMng.SearchFormData GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of CushionColor");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.CushionColorMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get CushionColor " + id.ToString());
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete CushionColor " + id.ToString());
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.CushionColorMng.CushionColor dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update CushionColor " + id.ToString());
            dtoItem.UpdatedBy = iRequesterID;
            dtoItem.CreatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public override bool Approve(int id, ref DTO.CushionColorMng.CushionColor dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.CushionColorMng.CushionColor dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.CushionColorMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            return factory.GetFilterData(out notification);
        }
    }
}
