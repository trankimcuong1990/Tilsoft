using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class WarehouseExportMng : Lib.BLLBase2<DTO.WarehouseExportMng.SearchFormData, DTO.WarehouseExportMng.EditFormData , DTO.WarehouseExportMng.WarehouseExport>
    {
        private DAL.WarehouseExportMng.DataFactory factory = new DAL.WarehouseExportMng.DataFactory();
        private Framework fwBLL = new Framework();

        public override DTO.WarehouseExportMng.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of warehouse Export receipt");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.WarehouseExportMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get warehouse Export receipt: " + id.ToString());

            // query data
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete warehouse Export receipt: " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.WarehouseExportMng.WarehouseExport dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update warehouse Export receipt: " + id.ToString());

            // query data
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public override bool Approve(int id, ref DTO.WarehouseExportMng.WarehouseExport dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.WarehouseExportMng.WarehouseExport dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool ChangeStatus(int id, int statusId, ref DTO.WarehouseExportMng.WarehouseExport dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "change status of warehouse export: " + id.ToString() + " to " + statusId.ToString());

            // query data
            return factory.ChangeStatus(id, statusId, ref dtoItem, out notification);
        }
    }
}
