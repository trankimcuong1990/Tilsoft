using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DevelopmentItemsRpt.DTO
{
    public class DevelopmentItemSearchResult
    {
        public int SampleProductID { get; set; }
        public string SampleProductUD { get; set; }
        public int? SampleOrderID { get; set; }
        public string ArticleDescription { get; set; }
        public int? ModelID { get; set; }
        public string ClientUD { get; set; }
        public string VNSuggestedFactoryUD { get; set; }
        public int? Quantity { get; set; }
        public string MaterialDescription { get; set; }
        public string MaterialTypeDescription { get; set; }
        public string MaterialColorDescription { get; set; }
        public string Material2Description { get; set; }
        public string Material2TypeDescription { get; set; }
        public string Material2ColorDescription { get; set; }
        public string Material3Description { get; set; }
        public string Material3TypeDescription { get; set; }
        public string Material3ColorDescription { get; set; }
        public string SeatCushionThickness { get; set; }
        public string SeatCushionSpecification { get; set; }
        public string BackCushionThickness { get; set; }
        public string BackCushionSpecification { get; set; }
        public string BackCushionDescription { get; set; }
        public string SeatCushionDescription { get; set; }
        public string CushionColorDescription { get; set; }
        public string RequestTypeNM { get; set; }
        public string NLSuggestedFactoryUD { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string SampleProductStatusNM { get; set; }
        public string ModelThumbnailLocation { get; set; }
        public string ModelFileLocation { get; set; }
        public string FinishedImageThumbnailLocation { get; set; }
        public string FinishedImageFileLocation { get; set; }
        public string FinishedImage { get; set; }
        public string Season { get; set; }
    }
}
