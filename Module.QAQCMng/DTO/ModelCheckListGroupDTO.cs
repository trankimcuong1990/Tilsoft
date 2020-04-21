using System;

namespace Module.QAQCMng.DTO
{
    public class ModelCheckListGroupDTO
    {
        public int ModelCheckListGroupID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> CheckListGroupID { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
