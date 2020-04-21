using System;

namespace Module.NormOfProduction.DTO
{
    public class SupportWorkOrderStatusDTO
    {
        public int ConstantEntryID { get; set; }
        public Nullable<int> WorkOrderStatusID { get; set; }
        public string WorkOrderStatusNM { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
    }
}
