using System;
using System.Collections;

namespace Module.ClientComplaint
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

        public DTO.InitFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(iRequesterID, out notification);
        }

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of Client Complaint");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, int clientId, string season, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "get client complaint");
            return factory.GetData(iRequesterID, id, clientId, season, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete as ClientComplaint " + id);

            // query data
            return factory.DeleteData(id, out notification);

        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update ClientComplaint " + id);

            // query data
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);

        }

       
        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "closing entry");
            return factory.Approve(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool Reset(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.Reset(iRequesterID, id, ref dtoItem, out notification);
        }

        public DTO.FactoryOrderDetailItems QuickSearchFactoryOrderDetailItems(int iRequesterID, Hashtable filters, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "quick search factory order detail items by client season");
            return factory.QuickSearchFactoryOrderDetailItems(filters, out notification);
        }

        public DTO.ProformaInvoiceItems QuickSearchPIs(int iRequesterID, Hashtable filters, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "quick search proformaInvoiceNo items by client season");
            return factory.QuickSearchPIs(filters, out notification);
        }

        public DTO.PIShipmentStatusData GetShipmentStatusOfPiSolution(int iRequesterID, Hashtable filters, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "quick search shipment status for created proformaInvoiceNo solution");
            return factory.GetShipmentStatusOfPiSolution(filters, out notification);
        }

        public string ExportExcelClientComplaint(int iRequesterID, Hashtable filters, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "export excel client complaint list");
            return factory.ExportExcelClientComplaint(filters, out notification);
        }

        public string ExportExcelClientComplaintItem(int iRequesterID, Hashtable filters, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "export excel client complaint item detail");
            return factory.ExportExcelClientComplaintItem(filters, out notification);
        }

        
    }
}
