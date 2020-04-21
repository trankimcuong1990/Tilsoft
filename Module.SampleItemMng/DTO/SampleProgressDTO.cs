using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Module.SampleItemMng.DTO
{
    public class SampleProgressDTO
    {
        public int SampleProgressID { get; set; }
        public int? SampleProductID { get; set; }

        [Required]
        public int? Version { get; set; }

        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }

        public List<SampleProgressImageDTO> SampleProgressImageDTOs { get; set; }
    }
}
