using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMng2.DTO
{
    public class FactoryCapacityByWeek
    {
        public int? FactoryID { get; set; }
        public int? FactoryCapacityID { get; set; }
        public string Season { get; set; }
        public int? WeekIndex { get; set; }
        public decimal? Capacity { get; set; }
        public int? WeekInfoID { get; set; }
    }
}
