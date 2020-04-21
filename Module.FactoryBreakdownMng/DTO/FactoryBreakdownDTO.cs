using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryBreakdownMng.DTO
{
    public class FactoryBreakdownDTO
    {
        public int FactoryBreakdownID { get; set; }
        public int? SampleProductID { get; set; }
        public int? ClientID { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsConfirmed { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public string PackingDimensionL { get; set; }
        public string PackingDimensionW { get; set; }
        public string PackingDimensionH { get; set; }
        public string CushionDimensionL { get; set; }
        public string CushionDimensionW { get; set; }
        public string CushionDimensionH { get; set; }
        public string Remark { get; set; }
        public decimal? IndicatedPrice { get; set; }
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
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string MaterialNM { get; set; }
        public string MaterialTypeNM { get; set; }
        public string MaterialColorNM { get; set; }
        public string FrameMaterialNM { get; set; }
        public string FrameMaterialColorNM { get; set; }
        public string SubMaterialNM { get; set; }
        public string SubMaterialColorNM { get; set; }
        public string BackCushionNM { get; set; }
        public string SeatCushionNM { get; set; }
        public string CushionColorNM { get; set; }
        public string FSCTypeNM { get; set; }
        public string FSCPercentNM { get; set; }
        public string ClientUD { get; set; }
        public string UpdatorName { get; set; }
        public string ConfirmerName { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string SampleOrderUD { get; set; }
        public int? ModelID { get; set; }


        public List<DTO.FactoryBreakdownDetailDTO> FactoryBreakdownDetailDTOs { get; set; }
        public List<DTO.FactoryBreakdownModelDTO> FactoryBreakdownModels { get; set; }

        public FactoryBreakdownDTO()
        {
            FactoryBreakdownDetailDTOs = new List<FactoryBreakdownDetailDTO>();
            FactoryBreakdownModels = new List<FactoryBreakdownModelDTO>();
        }
    }
}
