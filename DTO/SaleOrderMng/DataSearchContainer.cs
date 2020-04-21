using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SaleOrderMng
{
    public class DataSearchContainer
    {
        public List<OfferSearch> OfferSearchs { get; set; }
        public List<DTO.Support.Season> Seasons { get; set; }
        public List<DTO.Support.Saler> Salers { get; set; }
        public List<DTO.SaleOrderMng.OrderType> OrderTypes { get; set; }
    } 
}
