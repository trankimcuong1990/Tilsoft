using System;

namespace Module.AnnualLeaveMng.DTO
{
    public class AnnualLeaveStatusDTO
    {
        public int ConstantEntryID { get; set; }
        public Nullable<int> AnnualLeaveStatusID { get; set; }
        public string AnnualLeaveStatusNM { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
    }
}
