using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CostInvoice2Mng.DTO
{
    public class CostInvoice2Factory
    {
        public int CostInvoice2FactoryID { get; set; }
        public int? CostInvoice2ID { get; set; }
        public int? FactoryID { get; set; }
        public decimal? AmountFactory { get; set; }
        public string FactoryUD { get; set; }
    }
}
