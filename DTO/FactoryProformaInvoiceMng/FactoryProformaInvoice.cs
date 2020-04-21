using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.FactoryProformaInvoiceMng
{
    public class FactoryProformaInvoice
    {
        public int FactoryProformaInvoiceID { get; set; }
        public string Season { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Factory P/I No must be <= 30 characters length")]
        public string ProformaInvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public int? FactoryID { get; set; }
        public string AttachedFile { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ConcurrencyFlag_String { get; set; }
        public string FactoryUD { get; set; }
        public string UpdatorName { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public int? FactoryOrderID { get; set; }
        public string FactoryOrderUD { get; set; }

        public bool? IsConfirmed { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmerName { get; set; }
        public string ConfirmedDate { get; set; }

        public bool AttachedFile_HasChange { get; set; }
        public string AttachedFile_NewFile { get; set; }

        public List<FactoryProformaInvoiceDetail> Details { get; set; }
    }
}