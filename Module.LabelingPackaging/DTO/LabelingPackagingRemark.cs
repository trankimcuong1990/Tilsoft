using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.LabelingPackaging.DTO
{
    public class LabelingPackagingRemark
    {
        public int? LabelingPackagingRemarkID { get; set; }
        public int? LabelingPackagingID { get; set; }
        public string Remark { get; set; }
        public string RemarkFile { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string RemarkFileUrl { get; set; }
        public string RemarkFriendlyName { get; set; }
        public bool RemarkFileHasChange { get; set; }
        public string NewRemarkFile { get; set; }
        public bool RemarkTextHasChange { get; set; }
        public string UpdatorName { get; set; }
    }
}
