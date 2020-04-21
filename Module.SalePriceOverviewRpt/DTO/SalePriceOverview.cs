using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SalePriceOverviewRpt.DTO
{
    public class SalePriceOverview
    {
        public string ProformaInvoiceNo { get; set; }
        public string Season { get; set; }
        public string ClientUD { get; set; }
        public string ModelNM { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Currency { get; set; }
        public string TotalOrderQty { get; set; }
    }
}
