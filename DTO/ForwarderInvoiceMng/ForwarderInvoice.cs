using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ForwarderInvoiceMng
{
    public class ForwarderInvoice
    {
        public int? ForwarderInvoiceID { get; set; }
        public int? BookingID { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string AccountNo { get; set; }
        public string DueDate { get; set; }
        public string Remark { get; set; }
        public string PDFFileScan { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string FullName { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string BookingUD { get; set; }
        public string BLNo { get; set; }
        public string ETD { get; set; }
        public string ETA { get; set; }
        public string ForwarderInfo { get; set; }
        public string PoDName { get; set; }
        public string PoLname { get; set; }
        public bool PDFFileScan_HasChange { get; set; }
        public string PDFFileScan_NewFile { get; set; }
        public List<ForwarderInvoiceDetail> ForwarderInvoiceDetails { get; set; }
    }
}