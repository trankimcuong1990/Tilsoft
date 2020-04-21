using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.OrderQuantityCheckingRpt
{
    public class SaleOrder
    {
        public string ClientUD { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public string ProformaInvoiceNo { get; set; }
        public string Season { get; set; }

        public int OrderQnt { get; set; }

        public List<FactoryOrder> FactoryOrderDetails { get; set; }
    }
}