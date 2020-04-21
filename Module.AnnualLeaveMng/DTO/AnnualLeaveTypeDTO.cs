using System;

namespace Module.AnnualLeaveMng.DTO
{
    public class AnnualLeaveTypeDTO
    {
        public int ConstantEntryID { get; set; }
        public Nullable<int> AnnualLeaveTypeID { get; set; }
        public string AnnualLeaveTypeNM { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
    }
}
