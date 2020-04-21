using System;

namespace Module.WorkOrder.DTO
{
    public class WorkOrderReportHeaderDTO
    {
        public int OPSequenceDetailID { get; set; }
        public Nullable<int> OPSequenceID { get; set; }
        public Nullable<int> WorkCenterID { get; set; }
        public string WorkCenterUD { get; set; }
        public string WorkCenterNM { get; set; }
        public long PrimaryID { get; set; }      
    }
}
