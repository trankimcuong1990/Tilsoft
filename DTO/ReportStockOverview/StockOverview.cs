using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ReportStockOverview
{
    public class StockOverview
    {
        public string KeyID { get; set; }
        public int? GoodsID { get; set; }
        public int? ProductStatusID { get; set; }
        public string ProductType { get; set; }
        public string ProductThumbnailLocation { get; set; }
        public string ProductFileLocation { get; set; }
        public string ModelThumbnailLocation { get; set; }
        public string ModelFileLocation { get; set; }
        public string GardenImageFile { get; set; }
        public string ProductGardenFileLocation { get; set; }
        public string ProductStatusNM { get; set; }
        public string BarCode { get; set; }
        public string ImageFile { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? Qnt40HC { get; set; }
        public int? PhysicalQnt { get; set; }
        public decimal? PhysicalQntIn40HC { get; set; }
        public string WarehouseAreaUD { get; set; }
        public string QntPerWarehouseArea { get; set; }
        public decimal? StockValueInEuro { get; set; }
        public decimal? TotalStockValueInEuro { get; set; }
        public decimal? PurchaseFobPrice { get; set; }
        public decimal? TransportPerItem { get; set; }
        public int? OnSeaQnt { get; set; }
        public int? TobeShippedQnt { get; set; }
        public int? ReservationQnt { get; set; }
        public int? FTSQnt { get; set; }
        public decimal? SalePrice { get; set; }
        public int? FTS_Onsea {
            get 
            { 
                return FTSQnt + OnSeaQnt;
            }
        }
        public int? FTS_Onsea_TobeShipped
        {
            get
            {
                return FTSQnt + OnSeaQnt + TobeShippedQnt;
            }
        }
    }
}