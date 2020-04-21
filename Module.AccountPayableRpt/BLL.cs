using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AccountPayableRpt
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.LiabilitiesDto GetLiabilities(int userId, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetLiabilities(userId, filters, out notification);
        }
        public List<DTO.PurchaseInvoiceDTO> GetPurchaseInvoice(int userId, System.Collections.Hashtable filters,string sortedBy, string sortedDirection, out Library.DTO.Notification notification)
        {
            return factory.GetPurchaseInvoice(userId, filters, sortedBy, sortedDirection, out notification);
        }
        //public DTO.CongNoPhaiThuDto GetCongNoPhaiThu(int userId, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        //{
        //    return factory.GetCongNoPhaiThu(userId, filters, out notification);
        //}
        public List<DTO.SupplierSupport> QuerySupplier(System.Collections.Hashtable param, out Library.DTO.Notification notification)
        {
            return factory.QuerySupplier(param, out notification);
        }
        public List<DTO.ClientSupport> QueryCustomer(System.Collections.Hashtable param, out Library.DTO.Notification notification)
        {
            return factory.QueryCustomer(param, out notification);
        }
        public object GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(iRequesterID, out notification);
        }
        public bool Process(int iRequesterID, Hashtable dtoItem, out Library.DTO.Notification notification)
        {
            return factory.Process(iRequesterID, dtoItem, out notification);
        }
        //public object GetPurchaseInvoiceDTO(int iRequesterID, out Library.DTO.Notification notification)
        //{
        //    return factory.GetPurchaseInvoiceDTO(iRequesterID, out notification);
        //}
    }
}
