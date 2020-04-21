using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview.CIS
{
    public class CISDataContainer
    {
        public DTO.ClientMng.Overview.CIS.ShippingPerformanceDataContainer ShippingPerformanceData { get; set; }
        public DTO.ClientMng.Overview.CIS.ItemDataContainer ItemData { get; set; }
        public DTO.ClientMng.Overview.CIS.PriceComparisonDataContainer PriceComparisonData { get; set; }
    }
}
