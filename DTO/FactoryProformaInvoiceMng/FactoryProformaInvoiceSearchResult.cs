using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.FactoryProformaInvoiceMng
{
    public class FactoryProformaInvoiceSearchResult
    {
        public int FactoryProformaInvoiceID { get; set; }
        public string Season { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string FactoryOrderUD { get; set; }
        public string FactoryUD { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public bool IsConfirmed { get; set; }
        public string ConfirmerName { get; set; }
        public string ConfirmedDate { get; set; }
    }
}