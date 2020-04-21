using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.Models.ReportDTO
{
    public class PurchaseOrderPrintOutDTO
    {
        public int PurchaseOrderID { get; set; }
        public string PurchaseOrderUD { get; set; }
        public string PurchaseOrderDate { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
        public string Address { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string RequiredDocuments { get; set; }
        public string PaymentDocuments { get; set; }
        public int? SupplierPaymentTermID { get; set; }
        public string SupplierPaymentTermNM { get; set; }
        public string Currency { get; set; }
        public decimal VAT { get; set; }
        public List<Models.ReportDTO.PurchaseOrderDetailPrintOutDTO> PurchaseOrderDetailPrintOutDtos { get; set; }
    }
}