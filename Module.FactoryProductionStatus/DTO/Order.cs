using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryProductionStatus.DTO
{
    public class Order
    {
        public object KeyID { get; set; }
        public int? FactoryOrderID { get; set; }
        public int? FactoryID { get; set; }
        public string LDS { get; set; }
        public int? LDS_Week { get; set; }
        public string Season { get; set; }
        public string ClientUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public decimal? TotalContOrder { get; set; }
        public decimal? TotalProducedCont { get; set; }

    }
}
