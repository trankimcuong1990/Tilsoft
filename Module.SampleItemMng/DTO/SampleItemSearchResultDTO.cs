using System;

namespace Module.SampleItemMng.DTO
{
    public class SampleItemSearchResultDTO
    {
        public Nullable<int> SampleOrderID { get; set; }
        public string SampleOrderUD { get; set; }
        public string Season { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string ClientUD { get; set; }
        public int SampleProductID { get; set; }
        public string SampleProductUD { get; set; }
        public string SampleProductNM { get; set; }
        public Nullable<int> SampleProductStatusID { get; set; }
        public string SampleProductStatusNM { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string FactoryDeadline { get; set; }
        public Nullable<int> VNSuggestedFactoryID { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }
}
