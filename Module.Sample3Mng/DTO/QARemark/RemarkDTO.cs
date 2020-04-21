using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample3Mng.DTO.QARemark
{
    public class RemarkDTO
    {
        public int SampleQARemarkID { get; set; }
        public int SampleProductID { get; set; }
        public string Remark { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public bool IsEditing { get; set; }

        public List<RemarkImageDTO> RemarkImageDTOs { get; set; }
    }
}
