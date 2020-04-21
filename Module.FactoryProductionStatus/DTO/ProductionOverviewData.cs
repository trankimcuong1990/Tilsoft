using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryProductionStatus.DTO
{
    public class ProductionOverviewData
    {
        public List<Support.DTO.Season> Seasons { get; set; }
        public List<Support.DTO.Factory> Factories { get; set; }

        public int CurrentWeek { get; set; }
        public List<Support.DTO.WeekSeason> WeekSeasons { get; set; }
        public List<Order> Orders { get; set; }
        public List<ProductionByWeek> ProductionByWeeks { get; set; }
        public List<FactoryCapacity> FactoryCapacities { get; set; }
    }
}
