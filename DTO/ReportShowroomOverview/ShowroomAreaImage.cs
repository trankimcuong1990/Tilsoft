using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReportShowroomOverview
{
    public class ShowroomAreaImage
    {
        public int ShowroomAreaImageID { get; set; }
        public int? ShowroomAreaID { get; set; }
        public string AreaImageUD { get; set; }
        public string ShowroomAreaThumbnailImage { get; set; }
        public string ShowroomAreaFullImage { get; set; }
    }
}
