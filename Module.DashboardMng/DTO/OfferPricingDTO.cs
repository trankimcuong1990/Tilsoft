namespace Module.DashboardMng.DTO
{
    public class OfferPricingDTO
    {
        public int OfferID { get; set; }
        public string OfferUD { get; set; }
        public int? TotalOfferItem { get; set; }
        public int? TotalValidOfferItem { get; set; }
        public int? TotalApprovedOfferItem { get; set; }
        public bool? IsApproved { get; set; }
        public string Season { get; set; }
        public int? ClientID { get; set; }
        public string ClientNM { get; set; }
        public string SaleNM { get; set; }
        public int? SaleID { get; set; }
        public bool? IsAccountManager { get; set; }
        public string Currency { get; set; }
        public decimal? DeltaPercent { get; set; }
        public decimal? SaleAmount { get; set; }
        public string TrackingStatusNM { get; set; }
        public string ApprovedDate { get; set; }
        public string ApprovedUser { get; set; }
    }
}
