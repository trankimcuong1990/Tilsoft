using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample2Mng.DTO
{
    public class SampleProductCartonBoxDTO
    {
        public int SampleProductCartonBoxID { get; set; }
        public Nullable<int> SampleProductID { get; set; }
        public string SampleProductCartonBoxNM { get; set; }
        public string SampleProductCartonBoxItem { get; set; }
        public string CartonBoxDimL { get; set; }
        public string CartonBoxDimW { get; set; }
        public string CartonBoxDimH { get; set; }
        public string NetWeight { get; set; }
        public string GrossWeight { get; set; }
        public string Remark { get; set; }
    }
}
