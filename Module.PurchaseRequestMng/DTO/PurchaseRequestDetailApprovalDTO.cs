using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseRequestMng.DTO
{
    public class PurchaseRequestDetailApprovalDTO
    {
        public int PurchaseRequestDetailApprovalID {get;set;}
        public int? PurchaseRequestDetailID { get; set; }
        public int? PurchaseQuotationDetailID { get; set; }
        public decimal? ApprovedQnt { get; set; }
    }
}
