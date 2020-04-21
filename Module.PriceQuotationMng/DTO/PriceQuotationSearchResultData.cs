using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceQuotationMng.DTO
{
    public class PriceQuotationSearchResultData
    {
        public int QuotationDetailID { get; set; }
        public int? QuotationID { get; set; }
        public string Season { get; set; }
        public int? Qnt40HC { get; set; }
        public int OrderQnt { get; set; }
        public decimal? OrderQnt40HC { get; set; }
        public int? FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
        public string PriceDifferenceCode { get; set; }
        public decimal? PriceDifferenceRate { get; set; }
        public string Remark { get; set; }
        public string ProductionStatus { get; set; }
        public string LDS { get; set; }
        public int? QuotationStatusID { get; set; }
        public string QuotationStatusNM { get; set; }
        public string StatusUpdatedDate { get; set; }
        public string PriceUpdatedDate { get; set; }
        public decimal? FactoryPrice1 { get; set; }
        public decimal? FactoryPrice2 { get; set; }
        public decimal? FactoryPrice3 { get; set; }
        public decimal? FurnindoPrice1 { get; set; }
        public decimal? FurnindoPrice2 { get; set; }
        public decimal? FurnindoPrice3 { get; set; }
        public int? IsSetPrice { get; set; }
        public int? ModelID { get; set; }
        public int? FrameMaterialID { get; set; }
        public int? MaterialID { get; set; }
        public int? MaterialTypeID { get; set; }
        public string WaitingComment { get; set; }
        public decimal? OldPrice1 { get; set; }
        public decimal? OldPrice2 { get; set; }
        public decimal? OldPrice3 { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? FactoryOrderSparepartDetailID { get; set; }
        public bool? IsSelected { get; set; }
        public string OldPriceRemark { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? TargetPrice { get; set; }
        public decimal? PurchasingPrice { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public bool IsEdit { get; set; }
        public decimal? Price { get; set; }
    }
}
