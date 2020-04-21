using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QAQCMng.DTO
{
    public class ReportDefectsImageDTO
    {
        public int ReportDefectImageID { get; set; }
        public Nullable<int> ReportDefectID { get; set; }
        public string DefectImageMobileStringID { get; set; }
        public string FileUD { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public string ImageBase64 { get; set; }
    }
}
