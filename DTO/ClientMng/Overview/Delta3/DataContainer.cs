using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview.Delta3
{
    public class DataContainer
    {
        public List<EurofarPriceOverviewDTO> EurofarPriceOverviewDTOs { get; set; }
        public string Season { get; set; }
        public decimal ExRate { get; set; }
    }
}
