using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    class ProductSearchFormData
    {
        public ProductSearchFormData()
        {
            Data = new List<ProductDTO>();
        }
        public List<ProductDTO> Data { get; set; }
        public int TotalRows { get; set; }
    }
}
