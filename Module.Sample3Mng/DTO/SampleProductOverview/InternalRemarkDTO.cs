using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample3Mng.DTO.SampleProductOverview
{
    public class InternalRemarkDTO
    {
        public int SampleRemarkID { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string ThumbnailLocation { get; set; }
        public string UpdatedDate { get; set; }

        public List<InternalRemarkImageDTO> InternalRemarkImageDTOs { get; set; }
    }
}
