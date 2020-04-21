using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview.CIS
{
    public class ModelDTO
    {
        public int? ClientID { get; set; }
        public string Season { get; set; }
        public int? ModelID { get; set; }
        public decimal? TotalUnitPriceUSD { get; set; }
        public decimal? TotalUnitPriceEUR { get; set; }
        public int? TotalOrdered { get; set; }
        public int? TotalShipped { get; set; }
        public decimal? TotalAmountUSD { get; set; }
        public decimal? TotalAmountEUR { get; set; }
        public decimal? TotalConvertToEUR { get; set; }
        public decimal? TotalOrderQnt40HC { get; set; }
        public decimal? TotalShippedQnt40HC { get; set; }
        public decimal? TotalToBeShippedQnt40HC { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }
        public int RowIndex { get; set; }
        public decimal? TotalShippedPrice { get; set; }
    }
}
