using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample2Mng.DTO
{
   public class SamplePackaging
    {
        public int SamplePackagingID { get; set; }
        public int? SampleProductID { get; set; }
        public string CartonBoxDimL { get; set; }
        public string CartonBoxDimW { get; set; }
        public string CartonBoxDimH { get; set; }
        public string Remark { get; set; }
    }
}
