using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Module.Sample2Mng.DTO
{
    public class SampleProgress
    {
        public int SampleProgressID { get; set; }
        public int? SampleProductID { get; set; }

        [Required]
        public int? Version { get; set; }

        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }

        public string ShipmentDetail { get; set; }
        public List<SampleProgressImage> SampleProgressImages { get; set; }
    }
}
