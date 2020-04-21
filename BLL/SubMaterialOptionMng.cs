using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class SubMaterialOptionMng : Lib.BLLBase2<DTO.SubMaterialOptionMng.SearchFormData, DTO.SubMaterialOptionMng.EditFormData, DTO.SubMaterialOptionMng.SubMaterialOption>
    {
        private DAL.SubMaterialOptionMng.DataFactory factory;
        private BLL.Framework fwBLL = new Framework();
        public SubMaterialOptionMng(string tempFolder)
        {
            factory = new DAL.SubMaterialOptionMng.DataFactory(tempFolder);
        }

        public override DTO.SubMaterialOptionMng.SearchFormData GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of frame material option");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.SubMaterialOptionMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get frame material option " + id.ToString());
            return factory.GetData(id, out notification);
            //return factory.GetData(id, iRequesterID, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete frame material option " + id.ToString());
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.SubMaterialOptionMng.SubMaterialOption dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update frame material option " + id.ToString());
            dtoItem.UpdatedBy = iRequesterID;
            dtoItem.CreatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public override bool Approve(int id, ref DTO.SubMaterialOptionMng.SubMaterialOption dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.SubMaterialOptionMng.SubMaterialOption dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.SubMaterialOptionMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            return factory.GetFilterData(out notification);
        }
    }
}
