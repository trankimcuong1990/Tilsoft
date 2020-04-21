using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SampleMng
{
    public class SampleProductMinute
    {
        public int SampleProductMinuteID { get; set; }
        public int SampleProductID { get; set; }
        public string MeetingMinute { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public List<SampleProductMinuteImage> SampleProductMinuteImages { get; set; }
    }
}
