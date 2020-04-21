using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class DocumentMonitoringMng : Lib.BLLBase<DTO.DocumentMonitoringMng.DocumentMonitoringSearch, DTO.DocumentMonitoringMng.DocumentMonitoring>
    {
        private DAL.DocumentMonitoringMng.DataFactory factory = new DAL.DocumentMonitoringMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();

        public override IEnumerable<DTO.DocumentMonitoringMng.DocumentMonitoringSearch> GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "get list of document monitoring");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.DocumentMonitoringMng.DocumentMonitoring GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "get document monitoring " + id.ToString());
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "delete document monitoring " + id.ToString());
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.DocumentMonitoringMng.DocumentMonitoring dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "update document monitoring " + id.ToString());
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public bool QuickUpdateData(ref List<DTO.DocumentMonitoringMng.DocumentMonitoringSearchUpdate> dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "update document monitoring");
            return factory.QuickUpdateData(iRequesterID, ref dtoItem, out notification);
        }

        public DTO.DocumentMonitoringMng.EditSupportList GetEditSupportList()
        {
            return factory.GetEditSupportData();
        }

    }
}
   