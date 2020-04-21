using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QualityInspectionRpt.DTO
{
    public class SupportFormData
    {
        public QualityInspectionTypeData TypeData { get; set; }
        public List<QualityInspectionTypeData> TypeList { get; set; }

        public QualityInspectionCorrectActionData CorrectActionData { get; set; }
        public List<QualityInspectionCorrectActionData> CorrectActionList { get; set; }

        public QualityInspectionDefaultSettingData DefaultSettingData { get; set; }
        public List<QualityInspectionDefaultSettingData> DefaultSettingList { get; set; }
        public List<Support.DTO.WorkCenter> SupportWorkCenter { get; set; }
    }
}
