using System.Collections.Generic;

namespace Module.ClientComplaint.DTO
{
    public class FactoryOrderDetailItem
    {
        public int FactoryOrderDetailID { get; set; }
        public string FactoryUd { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string FactoryOrderUD { get; set; }
        public string LDS { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; } 
        public string IsShipped { get; set; }
        public string IsLoaded { get; set; }
        public int? LoadingPlanDetailID { get; set; }
        public decimal? OriginalSellingPrice { get; set; }
        public decimal? TotalSaleValue { get; set; }

    }

    public class FactoryOrderDetailItems
    {
        public List<FactoryOrderDetailItem> Data { get; set; }
        public int TotalRows { get; set; }
    }
}
