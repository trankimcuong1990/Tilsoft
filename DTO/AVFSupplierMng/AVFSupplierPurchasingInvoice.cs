using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.AVFSupplierMng
{
    public class AVFSupplierPurchasingInvoice
    {
        public string AVFSupplierNM { get; set; }
        public string AVFSupplierUD { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string DocumentNo { get; set; }
        public string ContainerTransportation { get; set; }
        public string DocumentDate { get; set; }
        public string PDFFileScan { get; set; }
        public int? AVFPurchasingInvoiceID { get; set; }
        public List<AVFSupplierPurchasingInvoiceDetail> AVFSupplierPurchasingInvoiceDetails { get; set; }
    }
}
