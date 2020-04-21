using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ECommercialInvoiceMng
{
    public class WarehouseInvoice
    {
        public string InvoiceNo { get; set; }

        public string InvoiceDate { get; set; }

        public string Currency { get; set; }

        public string ClientUD { get; set; }

        public string ClientNM { get; set; }

        public string SaleNM { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public string SupplierArt { get; set; }

        public int? Quantity { get; set; }

        public decimal? PurchasingPrice { get; set; }

        public decimal? PurchasingAmount { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal? SaleAmount { get; set; }

        public decimal? GrossMargin { get; set; }
        public int? ECommercialInvoiceID { get; set; }
        public bool? IsConfirmed { get; set; }
        public int? TypeOfInvoice { get; set; }
        public string ECInvoiceDate { get; set; }

        public int PrimaryID { get; set; }
    }
}
