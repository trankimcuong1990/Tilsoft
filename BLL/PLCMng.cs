using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PLCMng : Lib.BLLBase2<DTO.PLCMng.SearchFormData, DTO.PLCMng.EditFormData, DTO.PLCMng.PLC>
    {
        private DAL.PLCMng.DataFactory factory;
        private BLL.Framework fwBLL = new Framework();

        public PLCMng(string tempFolder)
        {
            factory = new DAL.PLCMng.DataFactory(tempFolder);
        }

        public override DTO.PLCMng.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of PLC");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.PLCMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete PLC " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.PLCMng.PLC dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update PLC " + id.ToString());

            // query data
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public override bool Approve(int id, ref DTO.PLCMng.PLC dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "approve PLC " + id.ToString());

            // query data
            return factory.Approve(id, ref dtoItem, out notification);
        }

        public override bool Reset(int id, ref DTO.PLCMng.PLC dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "reset PLC " + id.ToString());

            // query data
            return factory.Reset(id, ref dtoItem, out notification);
        }

        public DTO.PLCMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            return factory.GetFilterData(out notification);
        }

        public DTO.PLCMng.InitFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(iRequesterID, out notification);
        }

        public DTO.PLCMng.EditFormData GetData(int id, int iRequesterID, int bookingID, int factoryID, int offerLineID, int offerLineSparepartID, out Library.DTO.Notification notification)
        { 
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get PLC " + id.ToString());

            return factory.GetData(id, bookingID, factoryID, offerLineID, offerLineSparepartID, out notification);
        }

        public string GetReportData(int iRequesterID, string plcids, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get plc print out report");

            // query data
            return factory.GetReportData(plcids, out notification);
        }
    }
}
