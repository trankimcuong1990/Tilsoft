namespace DTO.ModelMng
{
    public class ModelPurchasingFixPriceConfigurationDTO
    {
        public int ModelPurchasingFixPriceConfigurationID { get; set; }
        public int? FactoryID { get; set; }
        public int? ModelID { get; set; }
        public string Season { get; set; }
        public int? MaterialTypeID { get; set; }
        public decimal? USDAmount { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string EmployeeNM { get; set; }
        public string MaterialTypeNM { get; set; }
    }
}
