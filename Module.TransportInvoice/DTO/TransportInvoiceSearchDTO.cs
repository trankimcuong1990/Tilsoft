using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportInvoice.DTO
{
    public class TransportInvoiceSearchDTO
    {
        public int? TransportInvoiceID { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string BLNo { get; set; }
        public string Currency { get; set; }
        public string RefNo { get; set; }
        public string Remark { get; set; }
        public string InvoiceStatusText { get; set; }
        public string StatustorName { get; set; }
        public string StatusUpdatedDate { get; set; }
        public string CreatorName { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public string ForwarderNM { get; set; }
        public int? UpdatedBy { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
    }
}
