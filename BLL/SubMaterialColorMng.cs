using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SubMaterialColorMng : Lib.BLLBase2<DTO.SubMaterialColorMng.SearchFormData, DTO.SubMaterialColorMng.EditFormData, DTO.SubMaterialColorMng.SubMaterialColor>
    {
        private DAL.SubMaterialColorMng.DataFactory factory = new DAL.SubMaterialColorMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();

        public override DTO.SubMaterialColorMng.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of sub material");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.SubMaterialColorMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
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

        public override bool UpdateData(int id, ref DTO.SubMaterialColorMng.SubMaterialColor dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update sub material " + id.ToString());

            // query data
            return factory.UpdateDataSub(id, ref dtoItem, iRequesterID, out notification);
        }

        public override bool Approve(int id, ref DTO.SubMaterialColorMng.SubMaterialColor dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.SubMaterialColorMng.SubMaterialColor dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
