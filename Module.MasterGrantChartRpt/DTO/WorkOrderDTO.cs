using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MasterGrantChartRpt.DTO
{
    public class WorkOrderDTO
    {
        public int WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string ClientUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public int? Duration { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string WorkOrderStatus { get; set; }
        public string SeasonStart { get; set; }
        public int WeekStartIndex { get; set; }
        public string WeekStart { get; set; }
        public string SeasonEnd { get; set; }
        public int WeekEndIndex { get; set; }
        public string WeekEnd { get; set; }
        public int? OverSchedule { get; set; }
    }
}
