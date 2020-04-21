using System;

namespace Module.QAQCMng.DTO
{
    public class DefectDTO
    {
        public int DefectID { get; set; }
        public string DefectUD { get; set; }
        public string DefectNM { get; set; }
        public Nullable<int> DefectA { get; set; }
        public Nullable<int> DefectB { get; set; }
        public Nullable<int> DefectC { get; set; }
        public Nullable<int> DefectGroupID { get; set; }
        public string DefectGroupUD { get; set; }
        public string DefectGroupNM { get; set; }
        public string DefectViNM { get; set; }
    }
}
