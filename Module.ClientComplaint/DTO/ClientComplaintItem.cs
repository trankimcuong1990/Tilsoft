using System;
using System.Collections.Generic;

namespace Module.ClientComplaint.DTO
{
    public class ClientComplaintItem
    {
        public int ClientComplaintItemID { get; set; }
        public int ClientComplaintID { get; set; }
        public int? LoadingPlanDetailID { get; set; }
        public int? QuantityOfComplaint { get; set; }
        public string ComplaintDescription { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public string ArticleCode { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string FactoryOrderDetailDescription { get; set; }
        public string LDS { get; set; }
        public decimal? OriginalSellingPrice { get; set; }
        public decimal? TotalSaleValue { get; set; }

        public List<ClientComplaintItemImage> ClientComplaintItemImages { get; set; }
        
    }

}
