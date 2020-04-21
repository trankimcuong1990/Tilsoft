using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.LoadingPlanMng
{
    public class SampleProductSearchResult
    {
        public int FactoryOrderSampleDetailID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string LDS { get; set; }
        public string ClientUD { get; set; }
        public string FactoryUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int Qnt40HC { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public Nullable<int> LoadedQnt { get; set; }
        public Nullable<int> RemainQnt { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<decimal> QuotationSalePrice { get; set; }
        public Nullable<int> OfferLineSampleProductID { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string ImageFile { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> SampleProductID { get; set; }
    }
}
