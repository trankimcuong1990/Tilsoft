using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class MaterialOptionMng : Lib.BLLBase2<DTO.MaterialOptionMng.SearchFormData, DTO.MaterialOptionMng.EditFormData, DTO.MaterialOptionMng.MaterialOption>
    {
        private DAL.MaterialOptionMng.DataFactory factory;
        private BLL.Framework fwBLL = new Framework();

        public MaterialOptionMng(string tempFolder)
        {
            factory = new DAL.MaterialOptionMng.DataFactory(tempFolder);
        }

        public override DTO.MaterialOptionMng.SearchFormData GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of materials");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.MaterialOptionMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {

            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get material option " + id.ToString());

            // query data
            //return factory.GetData(id, out notification);
            return factory.GetData(id, iRequesterID, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete material option " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.MaterialOptionMng.MaterialOption dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update material option " + id.ToString());

            // query data
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public override bool Approve(int id, ref DTO.MaterialOptionMng.MaterialOption dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.MaterialOptionMng.MaterialOption dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.MaterialOptionMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            return factory.GetFilterData(out notification);
        }
    }
}
