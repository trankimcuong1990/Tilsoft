using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReportMng
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();
        public DTO.SearchForm GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get qc report list");
            return factory.GetDataWithFilter(iRequesterID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public DTO.InitDataDTO GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get as InitData");

            // query data
            return factory.GetInitData(out notification);

        }
        public object GetInspection(int iRequesterID, string param, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get as InitData");

            // query data
            return factory.GetInspection(param, out notification);

        }
        public DTO.InitDataDTO SearchPI(int userId, Hashtable filters, out Notification notification)
        {
            return factory.SearchPI(filters, out notification);
        }

        public DTO.EditForm GetData(int iRequesterID, int id, int? saleOrderDetailID, int? factoryID, string itemFactoryOrderLink, int? clientID, string articleCode, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get as GetData" + id.ToString());

            // query data
            return factory.GetData(id, saleOrderDetailID, factoryID, itemFactoryOrderLink, clientID, articleCode, out notification);

        }
        public bool UpdateData(int userId, int id, ref object dto, out Notification notification)
        {
            return factory.Update(userId, id, ref dto, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, id, "Delete data width " + id.ToString());
            return factory.DeleteData(id, out notification);
        }

        public string GetExcelReportData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get QCReport data to excel");

            // query data
            return factory.GetExcelReportData(id, out notification);
        }

        public string GetExportPDF(int iRequesterID, int id, out Notification notification)
        {
            return factory.GetExportPDF(id, out notification);
        }

        public object GetListPIFromFactoryOrderDTO(string articleCode, int? client, int? factoryID, out Library.DTO.Notification notification)
        {
            // query data
            return factory.GetListPIFromFactoryOrderDTO(articleCode, client, factoryID, out notification);

        }
    }
}
