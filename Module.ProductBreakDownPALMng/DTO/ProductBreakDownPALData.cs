using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownPALMng.DTO
{
    public class ProductBreakDownPALData
    {
        public int ProductBreakDownID { get; set; }
        public string ItemSize { get; set; }
        public string CartonSize { get; set; }
        public string FrameDescription { get; set; }
        public string CushionDescription { get; set; }
        public string Remark { get; set; }
        public bool? IsConfirmed { get; set; }
        public int? ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public int? SampleProductID { get; set; }
        public string ArticleDescription { get; set; }
        public string SampleProductUD { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public string ConfirmerName { get; set; }
        public decimal? SeasonSpec { get; set; }
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
        public string PriceDate { get; set; }

        public int? ProductID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }

        public int? CurrencyID { get; set; }
        public string CurrencyNM { get; set; }
        public decimal? ExchangeRate { get; set; }

        public int? Qnt40HC { get; set; }

        public List<ProductBreakDownCategoryPALData> ProductBreakDownCategoryPAL { get; set; }
        public List<ProductBreakDownCategoryAmountPALData> ProductBreakDownCategoryAmountPAL { get; set; }
        public List<ProductBreakDownCategoryPercentPALData> ProductBreakDownCategoryPercentPAL { get; set; }
        public ModelDefaultOption ModelDefaultOption { get; set; }

        public ProductBreakDownPALData()
        {
            SeasonSpec = 10;
            CurrencyID = 2;
            CurrencyNM = "USD";
            ExchangeRate = 23300;

            ProductBreakDownCategoryPAL = new List<ProductBreakDownCategoryPALData>();
            ProductBreakDownCategoryAmountPAL = new List<ProductBreakDownCategoryAmountPALData>();
            ProductBreakDownCategoryPercentPAL = new List<ProductBreakDownCategoryPercentPALData>();            
        }
    }
}
