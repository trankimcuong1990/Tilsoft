using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.PurchasingInvoiceMng
{
    public class PurchasingInvoiceSearch
    {
        public int? PurchasingInvoiceID { get; set; }

        public string PurchasingInvoiceUD { get; set; }

        public string InvoiceNo { get; set; }

        public bool? IsExpotedToExact { get; set; }

        public string InvoiceDate { get; set; }

        public string Season { get; set; }

        public bool? IsConfirmed { get; set; }

        public string FactoryInvoiceNo { get; set; }

        public string CreatorName { get; set; }

        public int CreatorID { get; set; }

        public string CreatedDate { get; set; }

        public string UpdatorName { get; set; }

        public int UpdatorID { get; set; }

        public string UpdatedDate { get; set; }

        public string BLNo { get; set; }

        public string ShipedDate { get; set; }

        public string SupplierNM { get; set; }

        public string ForwarderNM { get; set; }

        public string Feeder { get; set; }

        public string POLName { get; set; }

        public string PODName { get; set; }

        public decimal? TotalAmount { get; set; }

        public decimal? TotalFactoryAmount { get; set; }

        public bool? IsConfirmedPrice { get; set; }
        public int? CompanyID { get; set; }

        public List<PurchasingCreditNote> PurchasingCreditNotes { get; set; }

        public List<PurchasingInvoiceContainer> PurchasingInvoiceContainers { get; set; }
    }
}