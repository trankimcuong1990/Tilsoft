using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionTeam.DTO
{
    public class ProductionTeamSearchDto
    {
        public int ProductionTeamID { get; set; }
        public string ProductionTeamUD { get; set; }
        public string ProductionTeamNM { get; set; }
        public decimal? OperatingTime { get; set; }
        public decimal? DefaultCost { get; set; }
        public decimal? Capacity { get; set; }
        public int? ResponsibleBy { get; set; }
        public string ResponsibleName { get; set; }
        public bool? HasHyperlink { get; set; }
        public string WorkCenterUD { get; set; }
        public bool? IsDefault { get; set; }
        public int? RowNumber { get; set; }
        public int? CompanyID { get; set; }
    }
}
