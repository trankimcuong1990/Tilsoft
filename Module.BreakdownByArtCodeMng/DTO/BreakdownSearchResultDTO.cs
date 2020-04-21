using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakdownByArtCodeMng.DTO
{
    public class BreakdownSearchResultDTO
    {
        public int ModelID { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public int OfferLineID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
    }
}
