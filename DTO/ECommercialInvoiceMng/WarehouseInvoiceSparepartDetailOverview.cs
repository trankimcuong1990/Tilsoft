using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ECommercialInvoiceMng
{
    public class WarehouseInvoiceSparepartDetailOverview
    {
        public int? WarehouseInvoiceSparepartDetailID { get; set; }

        public int? ECommercialInvoiceID { get; set; }

        public int? SparepartID { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public int? WarehousePickingListSparepartDetailID { get; set; }

        public string ProformaInvoiceNo { get; set; }

        public string CMRNo { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public string Currency { get; set; }

        public string VAT { get; set; }

        public string Conditions { get; set; }

        public int? PaymentTermID { get; set; }

        public string PaymentTermNM { get; set; }

        public int? DeliveryTermID { get; set; }

        public string DeliveryTermNM { get; set; }
    }
}
