using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng
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
            fwBLL.WriteLog(iRequesterID, 0, "get list of data");

            // query data
            filters["UserID"] = iRequesterID;
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public List<DTO.MaterialPriceProductItemSeach> GetProductionItem(string searchProductionItem, string searchProductionItemUD, int userId, out Library.DTO.Notification notification)
        {
            return factory.GetProductionItem(searchProductionItem, searchProductionItemUD, userId, out notification);
        }
        public string ExportToExcel(int iRequesterID, System.Collections.Hashtable filters, string orderBy, string orderDirection, out Library.DTO.Notification notification)
        {           
            return factory.ExportToExcel(filters, iRequesterID, orderBy, orderDirection, out notification);
        }
        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get as raw material " + id.ToString());

            // query data
            return factory.GetData(iRequesterID, id, out notification);

        }

        //
        // Get factory quick view
        //
        public DTO.EditFormData GetDetail(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get as raw material " + id.ToString());

            // query data
            return factory.GetDetail(id, out notification);

        }
        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete as raw material " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);

        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update raw material " + id.ToString());

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

        //
        // Custom function
        //
        public List<DTO.KeyRawMaterial> UpdateRawList(int userId, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateRawList(userId, dtoItem, out notification);
        }
        public int? RemoveRaw(int userId, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.RemoveRaw(userId, dtoItem, out notification);
        }

        public DTO.EditFormData GetOverview(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get as raw material overview" + id.ToString());

            // query data
            return factory.GetOverview(id, out notification);

        }
    }
}
