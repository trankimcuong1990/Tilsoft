using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PlanningProductionMng.DTO
{
    public class WorkOrderDTO
    {
        public int? PlanningProductionID { get; set; }
        public int? WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string ProductArticleCode { get; set; }
        public string ProductDescription { get; set; }
        public int? Quantity { get; set; }
        public string WorkOrderStatusNM { get; set; }
        public string WorkOrderTypeNM { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string OPSequenceNM { get; set; }
    }
}
