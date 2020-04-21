using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMng2.DTO
{
    public class FactoryExpectedCapacity
    {
        public int? FactoryExpectedCapacityID { get; set; }
        public int? FactoryID { get; set; }
        public string Season { get; set; }
        public int? Month { get; set; }
        public decimal? Qnt40HC { get; set; }
    }
}
