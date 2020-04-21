using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownMng.DTO
{
    public class ProductBreakDownSearchResultData
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

        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
    }
}
