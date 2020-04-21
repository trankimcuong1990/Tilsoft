using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WAYNMng
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public BLL() { }

        public BLL(string tempFolder)
        {
            factory = new DAL.DataFactory(tempFolder);
        }

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of Purchasing Invoice");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get as invoice " + id.ToString());

            // query data
            return factory.GetData(id, out notification);

        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete as invoice " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);

        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update profile " + id.ToString());

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
        //
        // CUSTOM FUNCTION
        //
        public IEnumerable<DTO.EmployeeList> GetDetail(int userId, string date, out Library.DTO.Notification notification)
        {
            return factory.GetDetail(userId, date, out notification);
        }
        public bool UpdateList(int userId, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateList(userId, dtoItem, out notification);
        }
        public bool UpdateNewList(int userId, object dtoItem, string date, out Library.DTO.Notification notification)
        {
            return factory.UpdateNewList(userId, dtoItem, date, out notification);
        }
        public bool DeleteList(int userId, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.DeleteList(userId, dtoItem, out notification);
        }
        public string GetExcelReportData(int iRequesterID, string date, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get Attendance Time  Overview to excel");

            // query data
            return factory.GetExcelReportData(date, out notification);
        }
    }
}
