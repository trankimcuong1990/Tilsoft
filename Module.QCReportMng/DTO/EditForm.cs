using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReportMng.DTO
{
    public class EditForm
    {
        public QCReportDTO Data { get; set; }
        public List<DTO.QCReportTestEnvironmentCategoryDTO> QCReportTestEnvironmentCategoryDTOs { get; set; }
        public List<DTO.QCReportSectionDTO> QCReportSectionDTOs { get; set; }
        public List<DTO.SupportInspectionStage> SupportInspectionStages { get; set; }
        public List<DTO.SupportSummaryResult> SupportSummaryResults { get; set; }
        public List<DTO.SupportPackagingMethod> SupportPackagingMethods { get; set; }
        public List<DTO.SupportInspectionResult> SupportInspectionResults { get; set; }
        public List<DTO.ListPIFromFactoryOrderDTO> ListPIFromFactoryOrderDTOs { get; set; }
    }
}
