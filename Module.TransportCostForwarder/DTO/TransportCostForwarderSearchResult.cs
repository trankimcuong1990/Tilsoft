namespace Module.TransportCostForwarder.DTO
{
    public class TransportCostForwarderSearchResult
    {
        public int TransportCostForwarderID { get; set; }
        public int? ForwarderID { get; set; }
        public string TransportInvoiceNumber { get; set; }
        public string TransportInvoiceDate { get; set; }
        public int? BookingID { get; set; }
        public string Currency { get; set; }
        public string Authorisation { get; set; }
        public string BookingUD { get; set; }
        public string ForwarderUD { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string CreatorNM { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorNM { get; set; }
        public string ForwarderNM { get; set; }
        public string BLNo { get; set; }
    }
}
