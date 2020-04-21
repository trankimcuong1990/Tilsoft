using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrder.DTO
{
    public class PreOrderWorkOrderDTO
    {
        public int WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string WorkOrderStatusNM { get; set; }
        public int OPSequenceID { get; set; }
        public string OPSequenceNM { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public int? ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
    }
}
