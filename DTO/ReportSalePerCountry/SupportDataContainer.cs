using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReportSalePerCountry
{
    public class SupportDataContainer
    {
        public List<DTO.Support.Season> Seasons { get; set; }
        public List<DTO.Support.Saler> Salers { get; set; } 
    }
}
