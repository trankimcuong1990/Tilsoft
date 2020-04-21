using System;

namespace Module.NormOfProduction.DTO
{
    public class SupportWorkCenterDTO
    {
        public int WorkCenterID { get; set; }
        public string WorkCenterUD { get; set; }
        public string WorkCenterNM { get; set; }
        public Nullable<decimal> OperatingTime { get; set; }
    }
}
