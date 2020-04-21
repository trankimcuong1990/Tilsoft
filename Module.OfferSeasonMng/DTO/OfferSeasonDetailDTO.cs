using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class OfferSeasonDetailDTO
    {
        public int OfferSeasonDetailID { get; set; }
        public Nullable<int> OfferSeasonID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> FrameMaterialID { get; set; }
        public Nullable<int> FrameMaterialColorID { get; set; }
        public Nullable<int> MaterialID { get; set; }
        public Nullable<int> MaterialTypeID { get; set; }
        public Nullable<int> MaterialColorID { get; set; }
        public Nullable<int> SubMaterialID { get; set; }
        public Nullable<int> SubMaterialColorID { get; set; }
        public Nullable<int> BackCushionID { get; set; }
        public Nullable<int> SeatCushionID { get; set; }
        public Nullable<int> CushionColorID { get; set; }
        public Nullable<int> FSCTypeID { get; set; }
        public Nullable<int> FSCPercentID { get; set; }
        public Nullable<bool> UseCushionFR { get; set; }
        public Nullable<bool> UseFSCLabel { get; set; }
        public string CushionRemark { get; set; }
        public Nullable<int> ManufacturerCountryID { get; set; }
        public Nullable<int> PackagingMethodID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public Nullable<int> RowIndex { get; set; }
        public Nullable<int> ClientSpecialPackagingMethodID { get; set; }
        public string SpecialRequirement { get; set; }
        public Nullable<decimal> PlaningPurchasingPrice { get; set; }
        public Nullable<int> PlaningPurchasingPriceSourceID { get; set; }
        public Nullable<int> PlaningPurchasingPriceFactoryID { get; set; }
        public Nullable<bool> IsPlaningPurchasingPriceSelected { get; set; }
        public Nullable<int> PlaningPurchasingPriceSelectedBy { get; set; }
        public string PlaningPurchasingPriceSelectedDate { get; set; }
        public string PlaningPurchasingPriceRemark { get; set; }
        public Nullable<int> ItemStatus { get; set; }
        public Nullable<int> SetItemStatusBy { get; set; }
        public string SetItemStatusDate { get; set; }
        public Nullable<int> PartID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> SparepartID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int? FavouriteFactoryID { get; set; }
        public string Remark { get; set; }       
        public bool? IsClientSelected { get; set; }
        public decimal? EstimatedPurchasingPrice { get; set; }
        public int? EstimatedPurchasingPriceFromFactoryID { get; set; }
        public int? EstimatedPurchasingPriceUpdatedByID { get; set; }
        public string EstimatedPurchasingPriceUpdatedDate { get; set; }
        public int? UnApprovedBy { get; set; }
        public string UnApprovedDate { get; set; }
        public bool IsNeedFactoryQuotation { get; set; }
        public decimal? SalePriceConfig { get; set; }
        public bool IsRepeatItem { get; set; }
        public decimal? CommissionPercent { get; set; }
        public int? NeedFactoryQuotationBy { get; set; }
        public string NeedFactoryQuotationDate { get; set; }
        public decimal? PurchasingPriceConfig { get; set; }
        public string PlaningPurchasingPriceFile { get; set; }
        public int? ClientSelectedBy { get; set; }
        public string ClientSelectedDate { get; set; }

        public string ProductFileLocation { get; set; }
        public string ProductThumbnailLocation { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public string StatustorName { get; set; }
        public bool IsEditing { get; set; }
        public string FavouriteFactoryUD { get; set; }
        public string PlaningPurchasingPriceFactoryUD { get; set; }
        public string PackagingMethodNM { get; set; }
        public int? Qnt40HC { get; set; }
        public string PlaningPurchasingPriceUpdatorName { get; set; }
        public int? RowID { get; set; } //this field is used in case update many rows at same time               
        public bool? IsChangedProperties { get; set; }
        public bool? IsInFactoryOrder { get; set; }
        public bool? MarkAsApproved { get; set; }  //this field mark as row is approving      
        public bool? IsManuallyKeyIn { get; set; }
        public string EstimatedPurchasingPriceUpdatedByName { get; set; }
        public bool? MarkAsUnApproved { get; set; }
        public string UnApprovedName { get; set; }
        public decimal? CurrentSupplierPrice { get; set; }
        public string CurrentSupplierUD { get; set; }        
        public int? OfferProductID { get; set; }        
        public int? CataloguePageNo { get; set; }
        public bool? MarkAsNeedFactoryQuotation { get; set; }
        public string NeedFactoryQuotationName { get; set; }
        public string PlaningPurchasingPriceFileLocation { get; set; }
        public string PlaningPurchasingPriceFileFriendlyName { get; set; }
        public bool? PlaningPurchasingPriceFileHasChange { get; set; }
        public string PlaningPurchasingPriceFileNewFile { get; set; }
        public bool? MarkAsClientSelected { get; set; }
        public string ClientSelectorName { get; set; }

        public List<OfferSeasonDetailPriceOptionDTO> OfferSeasonDetailPriceOptionDTOs { get; set; }
        public List<OfferSeasonDetailRemarkDTO> OfferSeasonDetailRemarkDTOs { get; set; }
        public List<OfferSeasonDetailPriceHistoryDTO> OfferSeasonDetailPriceHistoryDTOs { get; set; }
        public OfferSeasonDetailDTO() {            
            OfferSeasonDetailPriceOptionDTOs = new List<OfferSeasonDetailPriceOptionDTO>();
            OfferSeasonDetailRemarkDTOs = new List<OfferSeasonDetailRemarkDTO>();
            OfferSeasonDetailPriceHistoryDTOs = new List<OfferSeasonDetailPriceHistoryDTO>();
        }
    }
}
