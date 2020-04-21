using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SampleOrderOverviewRpt
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
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

        //
        // CUSTOM FUNCTION
        //
        public string GetExcelReportData(int iRequesterID, string season, string clientId, int? vnFactoryId, int? sampleProductStatusID, int? sampleOrderStatusID, int? sampleOrderID, bool canEdit, bool canRead, int userId, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get sample order overview data to excel");

            // query data
            return factory.GetExcelReportData(season, clientId, vnFactoryId, sampleProductStatusID, sampleOrderStatusID, sampleOrderID, canEdit, canRead, userId, out notification);
        }

        public string GetExcelReportData(int iRequesterID, int SampleOrderID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get sample order data to excel: " + SampleOrderID.ToString());

            // query data
            return factory.GetExcelReportData(SampleOrderID, out notification);
        }

        public DTO.InitFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(out notification);
        }

        public object GetSampleOrder(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetSampleOrder(iRequesterID, filters, out notification);
        }

        public string GetExportBarcode(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetExportBarcode(filters, out notification);
        }

        public object GetInitDataCustomFunction(int iRequesterID, bool canEdit, bool canRead, out Library.DTO.Notification notification)
        {
            return factory.GetInitDataCustomFunction(iRequesterID, canEdit, canEdit, out notification);
        }

        public string GetExcelReportDataSampleProcess(int iRequesterID, string season, string clientId, int? vnFactoryId, int? sampleProductStatusID, int? sampleOrderStatusID, int? sampleOrderID, bool canEdit, bool canRead, int userId, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get sample order data to excel version 2");

            // query data
            return factory.GetExcelReportDataSampleProcess(season, clientId, vnFactoryId, sampleProductStatusID, sampleOrderStatusID, sampleOrderID, canEdit, canRead, userId, out notification);
        }
    }
}
