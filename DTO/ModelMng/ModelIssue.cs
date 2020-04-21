namespace DTO.ModelMng
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ModelIssue
    {
        public int ModelIssueID { get; set; }
        public string StrongPoint { get; set; }
        public string WeakPoint { get; set; }
        public int ModelID { get; set; }
        public List<ModelIssueTrack> ModelIssueTracks { get; set; }
    }
}
