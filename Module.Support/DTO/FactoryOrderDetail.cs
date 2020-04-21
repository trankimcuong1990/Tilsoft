using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class FactoryOrderDetail
    {
        public int FactoryOrderDetailID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public int? ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public int? Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
    }
}
