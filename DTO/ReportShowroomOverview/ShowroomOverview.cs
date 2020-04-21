using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReportShowroomOverview
{
    public class ShowroomOverview
    {
        public object KeyID { get; set; }
        public int? ShowroomAreaID { get; set; }
        public int? ShowroomItemID { get; set; }
        public string ShowroomAreaUD { get; set; }
        public string ShowroomItemThumbnailImage { get; set; }
        public string ShowroomItemFullImage { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }

        public int? ProductID { get; set; }
        public string ProductArt { get; set; }
        public string ProductDescription { get; set; }
        public string ProductThumbnailImage { get; set; }
        
    }
}
