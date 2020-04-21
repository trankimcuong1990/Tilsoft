using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.LoadingPlanMng
{
    public class LoadingPlanSampleProductDetail
    {
        public int LoadingPlanSampleDetailID { get; set; }
        public Nullable<int> LoadingPlanID { get; set; }
        public Nullable<int> FactoryOrderSampleDetailID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Unit { get; set; }
        public Nullable<int> PKGs { get; set; }
        public Nullable<decimal> NettWeight { get; set; }
        public Nullable<decimal> KGs { get; set; }
        public Nullable<decimal> CBMs { get; set; }
        public string Remark { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string LDS { get; set; }
        public string ClientUD { get; set; }
        public string FactoryUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> Qnt40HC { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string ImageFile { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<int> OfferLineID { get; set; }
        public int OfferLineSampleProductID { get; set; }
        public Nullable<int> PLCID { get; set; }
        public Nullable<bool> IsCreatedPLC { get; set; }
        public decimal OrderProductQnt40HC { get; set; }
        public Nullable<int> RemainQnt { get; set; }
        public string HSCode { get; set; }
    }
}
