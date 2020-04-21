using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseQuotationMng.DTO
{
    public class ProductionItemDefaultPriceDTO
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public string Label { get; set; }

        public int? CompanyID { get; set; }

        public string UnitNM { get; set; }

        public decimal? UnitPrice { get; set; }

        public string ProductionItemUD { get; set; }

        public decimal? OrderQnt { get; set; }

        public decimal? FrameWeight { get; set; }

        public int? ProductionItemTypeID { get; set; }

        public bool? IsHasWeight { get; set; }
    }
}
