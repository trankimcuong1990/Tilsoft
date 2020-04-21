using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class WarehousePickingListMng : Lib.BLLBase2<DTO.WarehousePickingListMng.SearchFormData, DTO.WarehousePickingListMng.EditFormData, DTO.WarehousePickingListMng.WarehousePickingList>
    {
        private DAL.WarehousePickingListMng.DataFactory factory = new DAL.WarehousePickingListMng.DataFactory();
        private Framework fwBLL = new Framework();

        public override DTO.WarehousePickingListMng.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of warehouse picking list receipt");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.WarehousePickingListMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get warehouse picking list receipt: " + id.ToString());

            // query data
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete warehouse picking list receipt: " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.WarehousePickingListMng.WarehousePickingList dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update warehouse picking list receipt: " + id.ToString());

            // query data
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public override bool Approve(int id, ref DTO.WarehousePickingListMng.WarehousePickingList dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.WarehousePickingListMng.WarehousePickingList dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTION
        //
        public List<DTO.WarehousePickingListMng.RemainReservation> QuickSearchRemainProduct(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of remain reservation product");

            // query data
            return factory.QuickSearchRemainProduct(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public List<DTO.WarehousePickingListMng.RemainSparepart> QuickSearchRemainSparepart(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of remain reservation sparepart");

            // query data
            return factory.QuickSearchRemainSparepart(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool ChangeStatus(int id, int statusId, ref DTO.WarehousePickingListMng.WarehousePickingList dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "change status of picking list: " + id.ToString() + " to " + statusId.ToString());

            // query data
            return factory.ChangeStatus(id, statusId, ref dtoItem, out notification);
        }

        public DTO.WarehousePickingListMng.PickingListContainerPrintout GetNewPickingListPrintData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get pickinglist printout");
            return factory.GetNewPickingListPrintData(id, out  notification);
        }

        public string GetPickingListPrintoutData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get printout pickinglist");

            // query data
            return factory.GetPickingListPrintoutData(id,out notification);
        }

        public DTO.WarehousePickingListMng.CMRContainer GetCMRPrintout(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get CMR printout");
            return factory.GetCMR(id, out  notification);
        }

        public string ExportXML(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "export xml");

            // query data
            return factory.ExportXML(id, out notification);
        }

        public string GetExportExcelPickingList(int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "export picking list");

            // query data
            return factory.GetExportExcelPickingList(out notification);
        }

        public bool DeletePickingListProductDetail(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete warehouse picking list product: " + id.ToString());

            // query data
            return factory.DeletePickingListProductDetail(id, out notification);
        }

        public bool DeletePickingListSparepartDetail(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete warehouse picking list sparepart: " + id.ToString());

            // query data
            return factory.DeletePickingListSparepartDetail(id, out notification);
        }

        public bool DeletePickingListAreaDetail(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete warehouse picking list area detail: " + id.ToString());

            // query data
            return factory.DeletePickingListAreaDetail(id, out notification);
        }

    }
}
