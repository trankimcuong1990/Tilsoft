using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview.CIS
{
    public class PriceComparisonDataContainer
    {
        public string Season { get; set; }
        public decimal ExRate { get; set; }
        public string SeasonToCompare { get; set; }
        public decimal ExRateToCompare { get; set; }
        public List<DTO.ClientMng.Overview.CIS.PriceComparisonDTO> PriceComparisonDTOs { get; set; }
    }
}
