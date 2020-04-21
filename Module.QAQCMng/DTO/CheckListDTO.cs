using System;

namespace Module.QAQCMng.DTO
{
    public class CheckListDTO
    {
        public int CheckListID { get; set; }
        public string CheckListUD { get; set; }
        public string CheckListNM { get; set; }
        public Nullable<int> TypeOfInspecID { get; set; }
        public Nullable<int> CheckListGroupID { get; set; }
        public string CheckListGroupUD { get; set; }
        public string CheckListGroupNM { get; set; }
        public string CheckListNMVi { get; set; }
    }
}
