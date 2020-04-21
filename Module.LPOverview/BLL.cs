using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Module.LPOverview
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public BLL() { }

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of confirmed LP");

            // query data
            return factory.GetDataWithFilter(iRequesterID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get as LP overview " + id.ToString());

            // query data
            return factory.GetData(iRequesterID, id, out notification);

        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            throw new NotImplementedException();

        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

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

        public bool ReviseData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "resive labeling and packaging");
            return factory.ReviseData(id, out notification);
        }

        public string GetExcelData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get LP Overview to excel");

            // query data
            return factory.GetExcelData(id, out notification);
        }

        public string GetExcelDataNL(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get LP Overview to excel");

            // query data
            return factory.GetExcelDataNL(id, out notification);
        }

        public string GetExcelDataV2(int iRequesterID, int id, string ListLPDetails, string ListLPSparepartDetails, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get LP Overview to excel");

            // query data
            return factory.GetExcelDataV2(id, ListLPDetails, ListLPSparepartDetails, out notification);
        }

        // Get excel report data
        public string GetExcelReportData(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            // query data
            return factory.GetExcelReportData(iRequesterID, filters, out notification);
        }
    }
}
