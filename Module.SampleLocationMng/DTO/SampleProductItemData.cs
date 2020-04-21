namespace Module.SampleLocationMng.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SampleProductItemData
    {
        public int SampleProductItemID { get; set; }
        public string SampleProductItemUD { get; set; }
        public string ArticleDescription { get; set; }
        public string SampleProductStatusNM { get; set; }
        public int? Quantity { get; set; }
        public string FactoryUD { get; set; }
        public int? FactoryLocationID { get; set; }
        public int? OtherLocationID { get; set; }
        public string LocationDate { get; set; }
        public string Remark { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
        public string SampleOtherLocationNM { get; set; }
        public string FactoryLocationNM { get; set; }
        public string DisplayLocation { get; set; }

        public List<SampleProductLocationData> SampleProductLocations { get; set; }
    }
}
