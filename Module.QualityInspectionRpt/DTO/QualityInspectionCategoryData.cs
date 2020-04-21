using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QualityInspectionRpt.DTO
{
    public class QualityInspectionCategoryData
    {
        public QualityInspectionCategoryData()
        {
            QualityInpsectionCategoryImages = new List<QualityInspectionCategoryImageData>();
        }

        public int QualityInspectionCategoryID { get; set; }

        public int? QualityInspectionID { get; set; }

        public int? QualityInspectionResultID { get; set; }

        public string Description { get; set; }

        public List<QualityInspectionCategoryImageData> QualityInpsectionCategoryImages { get; set; }
    }
}
