using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.AVFPurchasingInvoiceMng.DTO
{
    public class AVFPurchasingInvoice
    {
        public int? AVFPurchasingInvoiceID { get; set; }
        public string AVFPurchasingInvoiceUD { get; set; }
        public int AVFSupplierID { get; set; }
        public int AVFSupplierInternalCodeID { get; set; }
        public string DocumentNo { get; set; }
        public string DocumentDate { get; set; }
        public string ContainerTransportation { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string Season { get; set; }
        public string TaxRate { get; set; }
        public string ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public string remark { get; set; }
        public string PDFFileScan { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public string AVFSupplierNM { get; set; }
        public string AVFSupplierUD { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }

        //
        // PDF
        //
        public bool PDFFileScan_HasChange { get; set; }
        public string PDFFileScan_NewFile { get; set; }

        //
        // concurrency
        //
        public string ConcurrencyFlag_String { get; set; }
        public List<AVFPurchasingInvoiceDetail> AVFPurchasingInvoiceDetails { get; set; }

        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public string CreatorName2 { get; set; }
        public string UpdatorName2 { get; set; }
    }
}
