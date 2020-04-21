using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Booking2Mng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get booking list");

            filters["UserID"] = iRequesterID;
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.SearchFilterData GetSearchFilter(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetSearchFilter(iRequesterID, out notification);
        }

        public DTO.InitFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(iRequesterID, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, int supplierID, int clientID, string season, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "get booking");
            return factory.GetData(iRequesterID, id, supplierID, clientID, season, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "delete booking");
            return factory.DeleteData(iRequesterID, id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "update booking");
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "approve");
            return factory.Approve(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool Reset(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "reset");
            return factory.Reset(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool ConfirmETA(int iRequesterID, int id, int ETAType, ref object dtoItem, out Library.DTO.Notification notification)
        { 
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "confirm ETA " + ETAType.ToString());
            return factory.ConfirmETA(iRequesterID, id, ETAType, ref dtoItem, out notification);
        }

        public string GetExcelReportData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // query data
            return factory.GetExcelReportData(id, out notification);
        }

        public string GetLoadingPlanReportData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // query data
            return factory.GetLoadingPlanReportData(id, out notification);
        }

        public string GetLoadingPlanReportData_PL(int iRequesterID, int id,int reportOption, out Library.DTO.Notification notification)
        {
            // query data
            return factory.GetLoadingPlanReportData_PL(id, reportOption, out notification);
        }

        public object GetSetDefault(int iRequesterID, int clientID, out Notification notification)
        {
            return factory.GetSetDefault(clientID, out notification);
        }

        public object GetFactoryOrder(int? clientID, string season, string pi, int? bookingID, out Notification notification)
        {
            return factory.GetFactoryOrder(clientID, season, pi, bookingID, out notification);
        }

        public bool ConfirmAllLoading(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "confirm All Loading  ");
            return factory.ConfirmAllLoading(iRequesterID, id, ref dtoItem, out notification);
        }
    }
}
