using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportInvoice.DTO
{
    public class TransportInvoiceDTO
    {
        public int? TransportInvoiceID { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public int? BookingID { get; set; }
        public string Currency { get; set; }
        public string RefNo { get; set; }
        public string Remark { get; set; }
        public int? InvoiceStatusID { get; set; }
        public int? StatusUpdatedBy { get; set; }
        public string StatusUpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int? ForwarderID { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public string StatustorName { get; set; }
        public string BLNo { get; set; }
        public string ForwarderNM { get; set; }
        public string PoLname { get; set; }
        public string PoDName { get; set; }
        public string ETD { get; set; }
        public string ETA { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string InvoiceStatusText { get; set; }
        public List<TransportInvoiceDetailDTO> TransportInvoiceDetailDTOs { get; set; }
    }
}
