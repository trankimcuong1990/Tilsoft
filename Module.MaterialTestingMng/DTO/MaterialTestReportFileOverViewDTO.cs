using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MaterialTestingMng.DTO
{
    public class MaterialTestReportFileOverViewDTO
    {
        public int? MaterialTestReportFileID { get; set; }
        public int? MaterialTestReportID { get; set; }
        public string FileUD { get; set; }
        public string Remark { get; set; }

        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }

        public string ScanFileLocation { get; set; }
        public bool ScanHasChange { get; set; }
        public string ScanNewFile { get; set; }
    }
}
