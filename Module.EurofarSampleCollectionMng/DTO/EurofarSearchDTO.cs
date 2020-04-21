using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EurofarSampleCollectionMng.DTO
{
    public class EurofarSearchDTO
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
        public string EurofarSampleCollectionDate { get; set; }
        public string Season { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string SampleOrderUD { get; set; }
        public string RankName { get; set; }
        public int? EurofarSampleCollectionBy { get; set; }
    }
}
