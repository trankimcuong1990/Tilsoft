using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QualityInspectionRpt.DTO
{
    public class QualityInspectionCategoryImageData
    {
        public int QualityInspectionCategoryImageID { get; set; }

        public int? QualityInspectionCategoryID { get; set; }

        public string FileUD { get; set; }

        public string FriendlyName { get; set; }

        public string FileLocation { get; set; }

        public string ThumbnailLocation { get; set; }

        public bool? HasChange { get; set; }

        public string NewFile { get; set; }
    }
}
