using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class BreakdownDTO
    {
        public int BreakdownID { get; set; }
        public string BreakdownUD { get; set; }
        public int? ModelID { get; set; }
        public int? SampleProductID { get; set; }
        public string ItemDescription { get; set; }
        public string ItemImage { get; set; }
        public decimal? SeasonSpecPercent { get; set; }
        public int? ClientID { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public bool? IsConfirmed { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string SampleProductUD { get; set; }
        public string ArticleDescription { get; set; }

        public string MaterialDescription { get; set; }
        public string MaterialTypeDescription { get; set; }
        public string MaterialColorDescription { get; set; }
        public string Material2Description { get; set; }
        public string Material2TypeDescription { get; set; }
        public string Material2ColorDescription { get; set; }
        public string Material3Description { get; set; }
        public string Material3TypeDescription { get; set; }
        public string Material3ColorDescription { get; set; }
        public string BackCushionDescription { get; set; }
        public string SeatCushionDescription { get; set; }
        public string CushionColorDescription { get; set; }

        public string ClientUD { get; set; }
        public string UpdatorName { get; set; }
        public string CreatorName { get; set; }
        public string ConfirmerName { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public int ProductGroupID { get; set; }
        public int? PackagingMethodID { get; set; }
        public int? ClientSpecialPackagingMethodID { get; set; }

        public List<DTO.BreakdownCategoryOptionDTO> BreakdownCategoryOptionDTOs { get; set; }
    }
}
