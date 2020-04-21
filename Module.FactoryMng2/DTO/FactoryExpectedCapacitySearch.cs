using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMng2.DTO
{
    public class FactoryExpectedCapacitySearch
    {
        public int FactoryExpectedCapacityID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string Season { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<decimal> Qnt40HC { get; set; }
        public string Months { get; set; }
    }
}
