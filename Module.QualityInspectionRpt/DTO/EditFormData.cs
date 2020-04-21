using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QualityInspectionRpt.DTO
{
    public class EditFormData
    {
        public QualityInspectionData EditData { get; set; }
        public List<QualityInspectionSearchResultData> SearchData { get; set; }

        public List<Support.DTO.FactoryRawMaterialData> SupportSupplier { get; set; }
        public List<QualityInspectionTypeData> SupportQualityInspectionType { get; set; }
        public List<QualityInspectionCorrectActionData> SupportQualityInspectionCorrectAction { get; set; }
        public List<SupportWickerColorData> SupportWickerColor { get; set; }
    }
}
