using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample2Mng.DTO
{
    public class SampleProductMinute
    {
        public int SampleProductMinuteID { get; set; }
        public string MeetingMinute { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }

        public List<DTO.SampleProductMinuteImage> SampleProductMinuteImages { get; set; }
    }
}
