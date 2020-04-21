using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PreOrderPlanningOverviewRpt.DTO
{
    public class ReportDetailDTO
    {
        public string FactoryUD { get; set; }
        public string FactoryNM { get; set; }
        public decimal CapacityB { get; set; }
        public decimal CapacityA { get; set; }
        public decimal OrderB { get; set; }
        public decimal OrderA { get; set; }
        public decimal PreOrderB { get; set; }
        public decimal PreOrderA { get; set; }
        public decimal PreProducedB { get; set; }
        public decimal PreProducedA { get; set; }
        public decimal DeltaB
        {
            get
            {
                return CapacityB + PreProducedB - (OrderB + PreOrderB);
            }
        }
        public decimal DeltaA
        {
            get
            {
                return CapacityA + PreProducedA - (OrderA + PreOrderA);
            }
        }

        List<ReportWeekDetailDTO> _ReportWeekDetailDTOs;
        public List<ReportWeekDetailDTO> ReportWeekDetailDTOs
        {
            get
            {
                if (_ReportWeekDetailDTOs == null)
                {
                    _ReportWeekDetailDTOs = new List<ReportWeekDetailDTO>();
                }
                return _ReportWeekDetailDTOs;
            }
        }
    }

    public class ReportWeekDetailDTO
    {
        public int WeekInfoID { get; set; }
        public string Color { get; set; }
        public decimal Value { get; set; }
    }

}
