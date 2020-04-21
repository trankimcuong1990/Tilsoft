using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPLCMng.DTO
{
    public class PLCSearchResult
    {
        public int PLCID { get; set; }
        public bool? IsConfirmed { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryUD { get; set; }
        public string BookingUD { get; set; }
        public string BLNo { get; set; }
        public string Season { get; set; }
        public string ClientUD { get; set; }
        public string Rating { get; set; }
        public string RatorName1 { get; set; }
        public string RatorName2 { get; set; }
        public string RatedDate { get; set; }
        public string UpdatorName1 { get; set; }
        public string UpdatorName2 { get; set; }
        public string UpdatedDate { get; set; }
        public string ConfirmerName1 { get; set; }
        public string ConfirmerName2 { get; set; }
        public string ConfirmedDate { get; set; }
        public int? FactoryID { get; set; }
        public int TotalLoadingPlanDetail { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public bool IsSelected { get; set; }

        public int? RatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? ConfirmedBy { get; set; }
    }
}
