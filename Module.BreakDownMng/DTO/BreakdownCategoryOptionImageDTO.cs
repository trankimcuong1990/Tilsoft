using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class BreakdownCategoryOptionImageDTO
    {
        public int BreakdownCategoryOptionImageID { get; set; }
        public int? BreakdownCategoryOptionID { get; set; }
        public string FileUD { get; set; }
        public string Description { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
    }
}
