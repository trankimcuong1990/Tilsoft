using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryProductionStatus.DTO
{
    public class OrderDetail
    {
        public int? FactoryOrderDetailID { get; set; }
        public int? FactoryOrderID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? OrderQnt { get; set; }
        public int? Qnt40HC { get; set; }
        public decimal? OrderInCont { get; set; }
    }
}
