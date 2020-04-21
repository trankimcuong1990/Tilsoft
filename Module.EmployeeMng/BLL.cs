using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Library.DTO;

namespace Module.EmployeeMng
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
            fwBLL.WriteLog(iRequesterID, 0, "get list of Employee");

            // query data
            filters["UserID"] = iRequesterID;
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get as employee " + id.ToString());

            // query data
            return factory.GetData(iRequesterID, id, out notification);

        }
        public DTO.EditFormData GetSensitiveData(int userId, int id, out Library.DTO.Notification notification)
        {
            return factory.GetSensitiveData(userId, id, out notification);
        }

        //
        // Get employee quick view
        //
        public DTO.EditFormData GetDetail(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get as employee " + id.ToString());

            // query data
            return factory.GetDetail(iRequesterID, id, out notification);

        }

        //
        // Get employee quick view
        //
        public DTO.EditFormData GetEmpDetail(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get as employee " + id.ToString());

            // query data
            return factory.GetEmpDetail(id, iRequesterID, out notification);

        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete as employee " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);

        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update employee " + id.ToString());

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

        //
        // overview
        //
        public DTO.Overview.FormData GetOverviewData(int userId, int id, out Library.DTO.Notification notification)
        {
            return factory.GetOverviewData(userId, id, out notification);
        }

        public DTO.Overview.FormData GetOverviewSensitiveData(int userId, int id, out Library.DTO.Notification notification)
        {
            return factory.GetOverviewSensitiveData(userId, id, out notification);
        }

        public bool Restore (int id, out Library.DTO.Notification notification)
        {
            return factory.Restore(id, out notification);
        }

        public object DeactiveAccount(int id, string hasLeftDate, out Notification notification)
        {
            // query data
            return factory.DeactiveAccount(id, hasLeftDate, out notification);
        }

        public string ExportExcelEmployee(Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.ExportExcelEmployee(filters, out notification);
        }

        //public string ExportEmployeeReport(int iRequesterID, string studentUD, string studentNM, string email, string birthday, out Library.DTO.Notification notification)
        //{
        //    // keep log entry
        //    fwBLL.WriteLog(iRequesterID, 0, "Export file Employee report");
        //    return factory.ExportEmployeeReport(studentUD, studentNM, email, birthday, out notification);
        //}
    }
}
