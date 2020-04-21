using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.LabelingPackaging.DTO
{
    public class LabelingPackagingOtherFile
    {
        public int? LabelingPackagingOtherFileID { get; set; }
        public int? LabelingPackagingID { get; set; }
        public string Remark { get; set; }
        public string OtherFile { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string OtherFileUrl { get; set; }
        public string OtherFileFriendlyName { get; set; }
        public bool OtherFileHasChange { get; set; }
        public string NewOtherFile { get; set; }
        public bool RemarkTextHasChange { get; set; }
        public string UpdatorName { get; set; }
    }
}
