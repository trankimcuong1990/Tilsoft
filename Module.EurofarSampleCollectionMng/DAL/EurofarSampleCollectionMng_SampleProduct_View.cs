//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.EurofarSampleCollectionMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class EurofarSampleCollectionMng_SampleProduct_View
    {
        public int SampleProductID { get; set; }
        public string SampleProductUD { get; set; }
        public Nullable<int> SampleOrderID { get; set; }
        public string ArticleDescription { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> SampleProductStatusID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string FinishedImageThumbnailLocation { get; set; }
        public string FinishedImageFileLocation { get; set; }
        public string FinishedImage { get; set; }
        public Nullable<bool> IsEurofarSampleCollection { get; set; }
        public string EurofarSampleCollectionName { get; set; }
        public Nullable<System.DateTime> EurofarSampleCollectionDate { get; set; }
        public string Season { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string SampleOrderUD { get; set; }
        public string RankName { get; set; }
        public Nullable<int> EurofarSampleCollectionBy { get; set; }
        public string EurofarSampleCollectionDateReport { get; set; }
    }
}