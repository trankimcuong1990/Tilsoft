using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryFinishedProduct.DTO
{
    public class FactoryFinishedProductPriceDTO
    {
        public int FactoryFinishedProductPriceID { get; set; }
        public int? FactoryFinishedProductID { get; set; }
        public int? FactoryTeamID { get; set; }
        public int? FactoryStepID { get; set; }
        public decimal? Price { get; set; }
        public decimal? HalfRoundPrice { get; set; }
        public decimal? DoubleRoundPrice { get; set; }
    }
}
