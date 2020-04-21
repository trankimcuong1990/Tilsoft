using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductOverviewRpt.DTO
{
    public class ModelDTO
    {
        public int ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string ProductTypeNM { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }

        //
        //these field use if has offerdeteailid
        //
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string PackagingMethodNM { get; set; }
        public string ClientUD { get; set; }
        public decimal? PurchasingPrice { get; set; }

        public List<DTO.PriceOverviewDTO> PriceOverviewDTOs { get; set; }
    }
}
