namespace Module.OrderedItemOverviewRpt.DTO
{
    public class OrderedItemOverviewReportViewDTO
    {
        public string Season { get; set; }
        public int? ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public int? TotalOrderQnt { get; set; }
        public decimal? TotalOrderCont { get; set; }
        public string ClientUDs { get; set; }
    }
}