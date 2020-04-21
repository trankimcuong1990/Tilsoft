using System;

namespace Module.Sample2Mng.DTO
{
    public class SampleOrderReportDTO
    {
        public string SCMMonitor { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string SampleOrderUD { get; set; }
        public DateTime? HardDeadline { get; set; }
        public DateTime? Deadline { get; set; }
        public string PurposeNM { get; set; }
        public string ShipmentDetail { get; set; }
        public string Season { get; set; }
    }
}