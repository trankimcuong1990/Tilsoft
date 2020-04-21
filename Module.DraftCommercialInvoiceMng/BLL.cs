using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftCommercialInvoiceMng
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public BLL() { }

        public DTO.SearchFormData GetDataWithFilter(int iRequestID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequestID, 0, "Get company list.");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.EditFormData GetData(int iRequestID, int id, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequestID, 0, string.Format("Get {0}", id.ToString()));
            return factory.GetData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update");
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public List<DTO.SupportClient> GetSupportClient(int iRequesterID, string Client, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get as Client");

            // query data
            return factory.GetSupportClient(Client, out notification);

        }
        public object GetSaleOrder(int clientID, int checkSelect, string season, string proformaInvoiceNo, string articleCode,  out Library.DTO.Notification notification)
        {
            return factory.GetSaleOrder(clientID, checkSelect, season, proformaInvoiceNo, articleCode, out notification);
        }

        public bool Confirm(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return factory.Confirm(userId, id, ref dtoItem, out notification);
        }

        public DTO.EditFormData GetOverView(int iRequestID, int id, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequestID, 0, string.Format("Get {0}", id.ToString()));
            return factory.GetOverView(id, out notification);
        }

        public string GetDraftInvoicePrintoutData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get invoice printout");
            return factory.GetDraftInvoicePrintoutData(id, out notification);
        }
        //public bool DeleteData(int iRequesterID, int id, out Notification notification)
        //{
        //    fwBLL.WriteLog(iRequesterID, id, "Delete data width " + id.ToString());
        //    return factory.DeleteData(id, out notification);
        //}
    }
}
