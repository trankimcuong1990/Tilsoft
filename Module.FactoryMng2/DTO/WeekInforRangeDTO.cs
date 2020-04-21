using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMng2.DTO
{
    public class WeekInforRangeDTO
    {
        public string Season { get; set; }
        public string WeekStart { get; set; }
        public int? WeekStartIndex { get; set; }
        public string WeekEnd { get; set; }
        public int? WeekEndIndex { get; set; }
        public bool? HasWeek53 { get; set; }
    }
}
