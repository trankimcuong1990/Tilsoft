using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryProductionStatus.DTO
{
    public class FactoryProductionStatusOrderDetailDTO
    {
        public int? FactoryProductionStatusOrderDetailID { get; set; }
        public int? FactoryProductionStatusDetailID { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? ProducedQnt { get; set; }
        public decimal? ProducedCont { get; set; }
        public int? OutputProducedQnt { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? OrderQnt { get; set; }
        public int? Qnt40HC { get; set; }
        public decimal? OrderInCont { get; set; }
        public int? TotalProducedLastWeek { get; set; }
        public int? TotalOutputProducedLastWeek { get; set; }
    }
}
