using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample3Mng.DTO.ProductInfo
{
    public class ProductDTO
    {
        public int SampleProductID { get; set; }
        public string SampleProductUD { get; set; }
        public string FinishedImageThumbnailLocation { get; set; }
        public string FinishedImageFileLocation { get; set; }
        public string SampleProductStatusNM { get; set; }
        public string SampleOrderUD { get; set; }
        public string ClientUD { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryDeadline { get; set; }
        public int? ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ArticleDescription { get; set; }
        public int? MaterialID { get; set; }
        public string MaterialUD { get; set; }
        public string MaterialDescription { get; set; }
        public int? MaterialTypeID { get; set; }
        public string MaterialTypeUD { get; set; }
        public string MaterialTypeDescription { get; set; }
        public int? MaterialColorID { get; set; }
        public string MaterialColorUD { get; set; }
        public string MaterialColorDescription { get; set; }
        public int? Material2ID { get; set; }
        public string Material2UD { get; set; }
        public string Material2Description { get; set; }
        public int? Material2TypeID { get; set; }
        public string Material2TypeUD { get; set; }
        public string Material2TypeDescription { get; set; }
        public int? Material2ColorID { get; set; }
        public string Material2ColorUD { get; set; }
        public string Material2ColorDescription { get; set; }
        public int? Material3ID { get; set; }
        public string Material3UD { get; set; }
        public string Material3Description { get; set; }
        public int? Material3TypeID { get; set; }
        public string Material3TypeUD { get; set; }
        public string Material3TypeDescription { get; set; }
        public int? Material3ColorID { get; set; }
        public string Material3ColorUD { get; set; }
        public string Material3ColorDescription { get; set; }
        public int? BackCushionID { get; set; }
        public string BackCushionUD { get; set; }
        public string BackCushionDescription { get; set; }
        public string BackCushionThickness { get; set; }
        public string BackCushionSpecification { get; set; }
        public int? SeatCushionID { get; set; }
        public string SeatCushionUD { get; set; }
        public string SeatCushionDescription { get; set; }
        public string SeatCushionThickness { get; set; }
        public string SeatCushionSpecification { get; set; }
        public int? CushionColorID { get; set; }
        public string CushionColorUD { get; set; }
        public string CushionColorDescription { get; set; }
        public int? Quantity { get; set; }
        public string ItemPerBox { get; set; }
        public string BoxPerSet { get; set; }
        public bool? IsTDNeeded { get; set; }
        public int? SampleOrderID { get; set; }

        public List<ReferenceImageDTO> ReferenceImageDTOs { get; set; }
    }
}
