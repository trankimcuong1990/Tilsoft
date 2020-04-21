using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMng2.DTO
{
    public class FactoryOrderTurnover
    {
        public int FactoryOrderID { get; set; }
        public string FactoryOrderUD { get; set; }
        public int? FactoryID { get; set; }
        public string ClientUD { get; set; }
        public string ProductionStatus { get; set; }
        public string TrackingStatusNM { get; set; }
        public decimal? Turnover { get; set; }
        public string Season { get; set; }
    }
}
