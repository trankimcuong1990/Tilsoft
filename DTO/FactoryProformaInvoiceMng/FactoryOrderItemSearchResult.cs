using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.FactoryProformaInvoiceMng
{
    public class FactoryOrderItemSearchResult
    {
        public string ClientUD { get; set; }

        public int? FactoryOrderDetailID { get; set; }

        public int? FactoryOrderSparepartDetailID { get; set; }

        public string FactoryOrderUD { get; set; }

        public string FactoryUD { get; set; }

        public string ProductionStatus { get; set; }

        public string LDS { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public int? OrderQnt { get; set; }
        public bool IsSelected { get; set; }
        public decimal? PurchasingPrice { get; set; }
    }
}