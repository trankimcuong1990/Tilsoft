using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReportMng.DTO
{
    public class QCReportImageDTO
    {
        public int QCReportImageID { get; set; }
        public int? QCReportID { get; set; }
        public string FileUD { get; set; }
        public string ImageTitle { get; set; }
        public string Description { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }

        public bool ScanHasChange { get; set; }
        public string ScanNewFile { get; set; }
    }
}
