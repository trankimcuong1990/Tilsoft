using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class BreakDownSearchDTO
    {
        public int BreakdownID { get; set; }
        public string BreakdownUD { get; set; }
        public int? ModelID { get; set; }
        public int? SampleProductID { get; set; }
        public string ItemDescription { get; set; }
        public int? ClientID { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public bool? IsConfirmed { get; set; }
        public int? ConfirmedBy { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string ConfirmedDate { get; set; }
        public string ModelUD { get; set; }
        public string ModelDescription { get; set; }
        public string SampleProductDescription { get; set; }
        public decimal? TotalAmountVND { get; set; }
        public string ClientUD { get; set; }
        public string UpdaterName { get; set; }
        public string ConfirmerName { get; set; }
        public string CreaterName { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public decimal? SeasonSpecPercent { get; set; }
        public string SampleProductUD { get; set; }
        public string Season { get; set; }
        public string DefaultFactory { get; set; }
        public string ModelStatusNM { get; set; }
        public int? ModelStatusID { get; set; }

        public decimal? AVFBreakdownAmountVND { get; set; }
        public decimal? AVTBreakdownAmountVND { get; set; }
        
    }
}

