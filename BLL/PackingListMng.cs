using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class PackingListMng : Lib.BLLBase<DTO.PackingListMng.PackingListSearchResult, DTO.PackingListMng.PackingList>
    {
        private DAL.PackingListMng.DataFactory factory = new DAL.PackingListMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();

        public override IEnumerable<DTO.PackingListMng.PackingListSearchResult> GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of packing list");
            //assign userID
            filters["iRequesterID"] = iRequesterID;
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.PackingListMng.PackingList GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get packing list " + id.ToString());
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete packing list " + id.ToString());
            return factory.DeleteData(id,iRequesterID, out notification);
        }

        public override bool UpdateData(int id, ref DTO.PackingListMng.PackingList dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update packing list " + id.ToString());
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, iRequesterID, out notification);
        }

        public DTO.PackingListMng.DataContainer GetDataContainer(int id,int purchasingInvoiceID, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get packing list " + id.ToString());

            // query data
            return factory.GetDataContainer(id, purchasingInvoiceID, iRequesterID, out notification);
        }

        public IEnumerable<DTO.PackingListMng.InitInfo> GetInitInfos(Hashtable filters,int iRequesterID, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list init info");
            return factory.GetInitInfos(filters, iRequesterID, out totalRows, out notification);
        }

        public string GetReportData(int packingListID, out Library.DTO.Notification notification)
        {
            DAL.PackingListMng.ReportFactory reportfactory = new DAL.PackingListMng.ReportFactory();
            return reportfactory.GetReportData(packingListID, out notification);
        }
         //Report By Container 
        public string GetReportDataByContainer(int packingListID, out Library.DTO.Notification notification)
        {
            DAL.PackingListMng.ReportFactory reportfactory = new DAL.PackingListMng.ReportFactory();
            return reportfactory.GetReportDataByContainer(packingListID, out notification);
        }

        public string GetOrangePiePrintout(int packingListID, out Library.DTO.Notification notification)
        {
            DAL.PackingListMng.ReportFactory reportfactory = new DAL.PackingListMng.ReportFactory();
            return reportfactory.GetOrangePiePrintout(packingListID, out notification);
        }

        public string PrintPackingListPDF(int packingListID, out Library.DTO.Notification notification)
        {
            DAL.PackingListMng.ReportFactory reportfactory = new DAL.PackingListMng.ReportFactory();
            return reportfactory.PrintPackingListPDF(packingListID, out notification);
        }

        public string GetOrangePineReportDataByContainer(int packingListID, out Library.DTO.Notification notification)
        {
            DAL.PackingListMng.ReportFactory reportfactory = new DAL.PackingListMng.ReportFactory();
            return reportfactory.GetOrangePineReportDataByContainer(packingListID, out notification);
        }
    }
}
