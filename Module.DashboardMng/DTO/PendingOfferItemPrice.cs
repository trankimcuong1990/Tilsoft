using System;

namespace Module.DashboardMng.DTO
{
    public class PendingOfferItemPrice
    {      
        public int QuotationDetailID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string ClientUD { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> WaitingStatusID { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
    }
}
