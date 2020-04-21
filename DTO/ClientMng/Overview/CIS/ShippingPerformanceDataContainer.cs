using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview.CIS
{
    public class ShippingPerformanceDataContainer
    {
        public string Season { get; set; }

        public int Total40HC { get; set; }
        public int Total40DC { get; set; }
        public int Total20DC { get; set; }
        public int TotalUnknowContainerType { get; set; }

        public int TotalOnTimeContainer { get; set; }
        public int TotalLateContainer { get; set; }
        public int TotalNotYetShipped { get; set; }

        public decimal TotalOrdered40HC { get; set; }
        public decimal TotalShipped40HC { get; set; }
        public decimal TotalToBeShipped { get; set; }

        public decimal AvgLateInDays { get; set; }
        public string FinalPerformanceConclusion { get; set; }

        public List<ShippingPerformanceDTO> ShippingPerformanceDTOs { get; set; }
        public List<ShippingPerformanceChartDTO> ShippingPerformanceChartDTOs { get; set; }
        public List<ShippingPerformanceChartDTO2> ShippingPerformanceChart2DTOs { get; set; }
    }
}
