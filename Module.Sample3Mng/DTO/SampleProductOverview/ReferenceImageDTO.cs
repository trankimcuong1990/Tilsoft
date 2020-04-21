using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample3Mng.DTO.SampleProductOverview
{
    public class ReferenceImageDTO
    {
        public int SampleReferenceImageID { get; set; }
        public string BringInBy { get; set; }
        public string BringInDate { get; set; }
        public string Remark { get; set; }
        public bool? IsDefault { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
    }
}
