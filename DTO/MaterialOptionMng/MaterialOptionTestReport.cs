using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MaterialOptionMng
{
    public class MaterialOptionTestReport
    {
        public int MaterialOptionTestReportID { get; set; }
        public int? MaterialOptionID { get; set; }
        public string FileUD { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        //public string FileExtension { get; set; }
        public string ThumbnailLocation { get; set; }
        //public int? FileSize { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public bool HasUrlLink { get; set; }

        public bool TestReportHasChange { get; set; }
        public string TestReportNewFile { get; set; }
    }
}
