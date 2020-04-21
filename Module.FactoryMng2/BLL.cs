using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMng2
{
    internal class BLL
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
            fwBLL.WriteLog(iRequesterID, 0, "get list of Factory");

            // query data
            filters["UserID"] = iRequesterID;
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get as factory " + id.ToString());

            // query data
            return factory.GetData(iRequesterID, id, out notification);

        }

        //
        // Get factory quick view
        //
        public DTO.EditFormData GetDetail(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get as employee " + id.ToString());

            // query data
            return factory.GetDetail(id, out notification);

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

        public DTO.SearchFilterData GetSearchFilter(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetSearchFilter(iRequesterID, out notification);
        }

        public List<DTO.FactoryOrderTurnover> GetFactoryOrderTurnover(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            fwBLL.WriteLog(userID, 0, "Get list Factory Order with Turnover");
            return factory.GetFactoryOrderTurnover(userID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public string ExportExcelFactory(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            filters["UserID"] = iRequesterID;
            return factory.ExportExcelFactory(filters, out notification);
        }

        public List<DTO.PersonInChargeDTO> GetPersonInCharge(int userID, Hashtable filters, out int totalRows, out Notification notification)
        {
            return factory.GetPersonInCharge(userID, filters, out totalRows, out notification);
        }
    }
}
