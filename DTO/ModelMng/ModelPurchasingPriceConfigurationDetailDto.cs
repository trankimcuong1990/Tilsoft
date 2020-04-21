namespace DTO.ModelMng
{
    public class ModelPurchasingPriceConfigurationDetailDTO
    {
        public int ModelPurchasingPriceConfigurationDetailID { get; set; }
        public int? ModelPurchasingPriceConfigurationID { get; set; }        
        public int? OptionID { get; set; }
        public decimal? PercentValue { get; set; }
        public decimal? USDAmount { get; set; }
        public string OptionNM { get; set; }
        public decimal? PurchasingUSDAmount { get; set; }        
    }
}
