namespace Module.FactoryQuotation2Mng.DTO
{
    public class ImportQuotationDetailDTO
    {
        public int QuotationDetailID { get; set; }
        public string Season { get; set; }
        public decimal? FactoryPrice { get; set; }   
        public string NewComment { get; set; }
    }
}
