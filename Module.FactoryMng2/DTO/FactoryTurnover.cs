using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMng2.DTO
{
    public class FactoryTurnover
    {
        public int? FactoryTurnoverID { get; set; }
        public int? FactoryID { get; set; }
        public string Season { get; set; }
        public decimal? Amount { get; set; }
        public decimal? ContainerShipped { get; set; }
    }
}
