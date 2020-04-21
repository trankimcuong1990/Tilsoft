using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShowroomAreaMng
{
    public class ShowroomArea
    {
        public int? ShowroomAreaID { get; set; }
        public string ShowroomAreaUD { get; set; }
        public string ShowroomAreaNM { get; set; }
        public List<ShowroomAreaImage> ShowroomAreaImages { get; set; }
    }
}
