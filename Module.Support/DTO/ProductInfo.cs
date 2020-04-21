using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class ProductInfo
    {
        public string ArticleCode { get; set; }
        public int? ModelID { get; set; }
        public string Description { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public int? MaterialID { get; set; }
        public string MaterialUD { get; set; }
        public string MaterialNM { get; set; }
        public int? MaterialTypeID { get; set; }
        public string MaterialTypeUD { get; set; }
        public string MaterialTypeNM { get; set; }
        public int? MaterialColorID { get; set; }
        public string MaterialColorUD { get; set; }
        public string MaterialColorNM { get; set; }
        public int? SubMaterialID { get; set; }
        public string SubMaterialUD { get; set; }
        public string SubMaterialNM { get; set; }
        public int? FrameMaterialID { get; set; }
        public string FrameMaterialUD { get; set; }
        public string FrameMaterialNM { get; set; }
        public int? FrameMaterialColorID { get; set; }
        public string FrameMaterialColorUD { get; set; }
        public string FrameMaterialColorNM { get; set; }
        public int? BackCushionID { get; set; }
        public string BackCushionUD { get; set; }
        public string BackCushionNM { get; set; }
        public int? SeatCushionID { get; set; }
        public string SeatCushionUD { get; set; }
        public string SeatCushionNM { get; set; }
        public int? CushionID { get; set; }
        public string CushionUD { get; set; }
        public string CushionNM { get; set; }
        public string CushionRemark { get; set; }
        public string OverallDimL { get; set; }
        public string OverallDimW { get; set; }
        public string OverallDimH { get; set; }
        public int? Qnt20DC { get; set; }
        public int? Qnt40DC { get; set; }
        public int? Qnt40HC { get; set; }
        public string CartonBoxDimL { get; set; }
        public string CartonBoxDimW { get; set; }
        public string CartonBoxDimH { get; set; }
        public string NetWeight { get; set; }
        public string GrossWeight { get; set; }
        public string QntInBox { get; set; }
        public int? FactoryID { get; set; }
    }
}
