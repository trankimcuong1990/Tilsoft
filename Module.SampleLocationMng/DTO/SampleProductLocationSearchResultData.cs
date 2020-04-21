using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SampleLocationMng.DTO
{
    public class SampleProductLocationSearchResultData
    {
        public int SampleProductItemID { get; set; }
        public string SampleProductItemUD { get; set; }
        public string ArticleDescription { get; set; }
        public string FactoryUD { get; set; }
        public string LocationUD { get; set; }
        public string Season { get; set; }
        public string ClientUD { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public int? Quantity { get; set; }
        public int? SampleProductStatusID { get; set; }
        public string SampleProductStatusNM { get; set; }
        public int? ClientID { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public bool IsSelected { get; set; }
    }
}
