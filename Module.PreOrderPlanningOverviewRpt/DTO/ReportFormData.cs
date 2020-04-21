using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PreOrderPlanningOverviewRpt.DTO
{
    public class ReportFormData
    {
        public decimal TotalPreOrder { get; set; }
        public int TetWeekInfoID { get; set; }
        public List<DTO.WeekInfoDTO> WeekInfoDTOs { get; set; }
        public List<DTO.ReportDetailDTO> ReportDetailDTOs { get; set; }
        public DTO.ReportDetailDTO SumDataRow { get; set; }
    }
}
