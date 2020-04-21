using Library.DTO;
using System.Collections;
using System.Collections.Generic;

namespace Module.ProductionFrameWeightMng
{
    internal class BLL
    {
        private readonly DAL.DataFactory factory = new DAL.DataFactory();

        public DTO.ProductionFrameWeightData GetData(int userID, int id, out Notification notification)
        {
            return factory.GetData(userID, id, out notification);
        }

        public DTO.ProductionFrameWeightData UpdateData(int userID, int id, Hashtable filters, out Notification notification)
        {
            return factory.UpdateData(userID, id, filters, out notification);
        }

        public bool DeleteData(int userID, int id, out Notification notification)
        {
            return factory.DeleteData(userID, id, out notification);
        }

        public List<DTO.ProductionFrameWeightSearchResultData> GetDataWithFilter(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return factory.GetDataWithFilter(userID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public string GetExcelReportData(string workOrderUD, string clientUD, string proformaInvoiceNo, string productionItem, out Library.DTO.Notification notification)
        {
            return factory.GetExcelReportData(workOrderUD,clientUD,proformaInvoiceNo,productionItem, out notification);
        }
    }
}
