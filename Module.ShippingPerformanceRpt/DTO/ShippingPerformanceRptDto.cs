using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ShippingPerformanceRpt.DTO
{
    public class ShippingPerformanceRptDto
    {
        public int? FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
        public string Season { get; set; }
        public decimal? NotYet { get; set; }
        public decimal? OnTime { get; set; }
        public decimal? Week1 { get; set; }
        public decimal? Week2 { get; set; }
        public decimal? Week3 { get; set; }
        public decimal? Week4 { get; set; }
        public decimal? Week5 { get; set; }
        public decimal? Week6 { get; set; }
        public decimal? Week7 { get; set; }
        public decimal? Over7 { get; set; }
    }
}
