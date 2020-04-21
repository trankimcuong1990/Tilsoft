using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseRequestMng.DTO
{
    public class PurchaseRequestSearchDTO
    {
        public int? PurchaseRequestID { get; set; }
        public string PurchaseRequestUD { get; set; }
        public string PurchaseRequestDate { get; set; }
        public int? CompanyID { get; set; }
        public string ETA { get; set; }
        public string Remark { get; set; }
        public int? PurchaseRequestStatusID { get; set; }
        //public int? RequestedBy { get; set; }
        public string RequestedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovedDate { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string FactoryUD { get; set; }
        public string LDS { get; set; }
        public string RequestorName { get; set; }
        public string ApproverName { get; set; }
        public string PurchaseRequestStatusNM { get; set; }
    }
}
