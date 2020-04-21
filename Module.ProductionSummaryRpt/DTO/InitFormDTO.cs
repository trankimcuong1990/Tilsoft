using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionSummaryRpt.DTO
{
    public class InitFormDTO
    {
        public List<Support.DTO.Factory> Factories { get; set; }
        public List<Support.DTO.Season> Seasons { get; set; }

        public InitFormDTO()
        {
            Factories = new List<Support.DTO.Factory>();
            Seasons = new List<Support.DTO.Season>();
        }
    }
}
