using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SampleLocationMng.DTO
{
    public class EditFormData
    {
        public List<DTO.SampleProductItemData> SampleProducts { get; set; }
        public List<Support.DTO.Factory> FactoryLocations { get; set; }
        public List<DTO.SupportSampleOtherLocationData> OtherLocations { get; set; }
    }
}
