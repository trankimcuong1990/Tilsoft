using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShowroomAreaMng
{
    public class ShowroomAreaImage
    {
        public int ShowroomAreaImageID { get; set; }
        public string AreaImageUD { get; set; }
        public string AreaImageNM { get; set; }
        public string ImageFile { get; set; }
        public int? ShowroomAreaID { get; set; }
        public string ShowroomAreaThumbnailImage { get; set; }
        public string ShowroomAreaFullImage { get; set; }

        public bool? ImageFile_HasChange { get; set; }
        public string ImageFile_NewFile { get; set; }
    }
}
