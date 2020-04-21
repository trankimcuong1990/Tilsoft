using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ForwarderInvoiceMng
{
    public class ForwarderInvoiceSearchResult
    {
        public int? ForwarderInvoiceID { get; set; }
        public int? BookingID { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string AccountNo { get; set; }
        public string Remark { get; set; }
        public string PDFFileScan { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string FullName { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }
        public string DueDate { get; set; }
    }
}