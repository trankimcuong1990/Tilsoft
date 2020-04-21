using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryBreakdownMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, int? sampleProductID, int? modelID, out Library.DTO.Notification notification)
        {
            return factory.GetData(iRequesterID, id, sampleProductID, modelID, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.Approve(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool Reset(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.Reset(iRequesterID, id, ref dtoItem, out notification);
        }
        public string GetFactoryBreakdownExportToExcelList(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetFactoryBreakdownExportToExcelList(filters, out notification);
        }

        public object ExportExcelFactoryBreakdownDetail(int userID, int id, out Library.DTO.Notification notification)
        {
            return factory.ExportExcelFactoryBreakdownDetail(id, out notification);
        }

        /// <summary>
        /// Get data open Breakdown
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="sampleProductID"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public object GetDataForBreakdown(int userID, int sampleProductID, out Library.DTO.Notification notification)
        {
            return factory.GetDataForBreakdown(sampleProductID, out notification);
        }
        public DTO.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            return factory.GetFilterData(out notification);
        }
        public bool ImportExcelData(int iRequesterID, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.ImportExcelData(iRequesterID, ref dtoItem, out notification);
        }
    }
}
