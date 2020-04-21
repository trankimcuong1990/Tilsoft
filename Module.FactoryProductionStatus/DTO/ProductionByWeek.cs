using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryProductionStatus.DTO
{
    public class ProductionByWeek
    {
        public object KeyID { get; set; }
        public int? FactoryOrderID { get; set; }
        public int? FactoryID { get; set; }
        public string Season { get; set; }
        public int? WeekNo { get; set; }
        public decimal? TotalProducedContainerQnt { get; set; }
    }
}
