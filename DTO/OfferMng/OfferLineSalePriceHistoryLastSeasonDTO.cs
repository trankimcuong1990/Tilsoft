using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.OfferMng
{
    public class OfferLineSalePriceHistoryLastSeasonDTO
    {
        public int? OfferLineID { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? LastUnitPrice { get; set; }
        public string Currency { get; set; }
        public string OfferUD { get; set; }
        public int? OfferID { get; set; }
    }
}
