using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPerformanceRpt.DTO
{
    public class HtmlReportData
    {
        public List<WeekInfo> WeekInfoData { get; set; }

        // shipped
        public List<AnnualShipped> AnnualShippedData { get; set; }
        public List<WeeklyShipped> WeeklyShippedData { get; set; }
        public List<FactoryInfo> FactoryInfoData { get; set; }

        // produced
        public List<AnnualProduced> AnnualProducedData { get; set; }
        public List<WeeklyProduced> WeeklyProducedData { get; set; }

        //factory capacity
        public List<TotalCapacityAndOrderData> TotalCapacityAndOrderDatas { get; set; }
        public List<TotalCapacityAndOrderByWeekData> TotalCapacityAndOrderByWeekDatas  { get; set; }
    }
}
