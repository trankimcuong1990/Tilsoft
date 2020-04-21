using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SubMaterialMng: Lib.BLLBase2<DTO.SubMaterialMng.SearchFormData, DTO.SubMaterialMng.EditFormData, DTO.SubMaterialMng.SubMaterial>
    {
        private DAL.SubMaterialMng.DataFactory factory = new DAL.SubMaterialMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();

        public override DTO.SubMaterialMng.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of sub material");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.SubMaterialMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get sub material " + id.ToString());

            // query data
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete sub material " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.SubMaterialMng.SubMaterial dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update sub material " + id.ToString());

            // query data
            return factory.UpdateDataSub(id, ref dtoItem, iRequesterID, out notification);
        }

        public override bool Approve(int id, ref DTO.SubMaterialMng.SubMaterial dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.SubMaterialMng.SubMaterial dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
