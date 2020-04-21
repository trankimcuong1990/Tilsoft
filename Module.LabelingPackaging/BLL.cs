using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Library.DTO;

namespace Module.LabelingPackaging
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public BLL() { }

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of LP");

            // query data
            return factory.GetDataWithFilter(iRequesterID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get as LP " + id.ToString());

            // query data
            return factory.GetData(iRequesterID, id, out notification);

        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete as LP " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);

        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update LP " + id.ToString());

            // query data
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);

        }

        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.SearchFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            return factory.GetSearchFilter(out notification);
        }
        public string GetExcelData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get LP Overview to excel");

            // query data
            return factory.GetExcelData(id, out notification);
        }

        // Get excel report data
        public string GetExcelReportData(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            // query data
            return factory.GetExcelReportData(iRequesterID, filters, out notification);
        }

        public bool ClearExistingFile(out Library.DTO.Notification notification)
        {
            // query data
            return factory.ClearExistingFile(out notification);
        }

        public bool SendNotificationNL2VN(int iRequesterID, Hashtable filters, out Notification notification)
        {
            return factory.SendNotificationNL2VN(iRequesterID, filters, out notification);
        }     
        public DTO.EditFormData AutoFile(int iRequesterID, int id, Hashtable dtoItem, out Library.DTO.Notification notification)
        {
            // query data
            return factory.AutoFile(iRequesterID, id, dtoItem, out notification);
        }

        public bool AutoUpdate(int iRequesterID, Hashtable dtoItem, out Library.DTO.Notification notification)
        {

            // query data
            return factory.AutoUpdate(iRequesterID, ref dtoItem, out notification);

        }
    }
}
