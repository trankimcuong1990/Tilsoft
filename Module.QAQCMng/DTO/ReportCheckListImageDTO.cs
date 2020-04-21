using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QAQCMng.DTO
{
    public class ReportCheckListImageDTO
    {
        public int ReportCheckListImageID { get; set; }
        public Nullable<int> ReportCheckListID { get; set; }
        public string CheckListImageStringID { get; set; }
        public string FileUD { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
    }
}
