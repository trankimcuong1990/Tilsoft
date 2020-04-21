using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryFinishedProduct.DTO
{
    public class FactoryFinishedProductDTO
    {
        public int FactoryFinishedProductID { get; set; }
        public string FactoryFinishedProductUD { get; set; }
        public string FactoryFinishedProductNM { get; set; }
        public List<FactoryFinishedProductPriceDTO> FactoryFinishedProductPriceDTOs { get;set; }
    }
}
