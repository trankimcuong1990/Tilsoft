using System.Collections.Generic;

namespace Module.DashboardMng.DTO
{
    public class DashboardDetalResultDTO
    {
        public List<DTO.DashboardDeltaChartDTO> Data { get; set; }
        public List<string> Months { get; set; }
    }
}
