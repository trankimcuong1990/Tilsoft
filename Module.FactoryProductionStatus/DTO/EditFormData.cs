using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryProductionStatus.DTO
{
    public class EditFormData
    {
        public FactoryProductionStatusDTO Data { get; set; }
        public List<Support.DTO.Factory> Factories { get; set; }
        public List<Support.DTO.WeekSeason> WeekSeasons { get; set; }

    }
}
