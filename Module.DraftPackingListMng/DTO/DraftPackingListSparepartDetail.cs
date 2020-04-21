using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftPackingListMng.DTO
{
    public class DraftPackingListSparepartDetail
    {
        public int DraftPackingListDetailID { get; set; }
        public int? DraftPackingListID { get; set; }
        public string Unit { get; set; }
        public int? Quantity { get; set; }
        public int? PKGs { get; set; }
        public decimal? NettWeight { get; set; }
        public decimal? KGs { get; set; }
        public decimal? CBMs { get; set; }
        public string Remark { get; set; }
        public string InvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public int? DraftCommercialInvoiceDetailID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? DraftCommercialInvoiceID { get; set; }
    }
}
