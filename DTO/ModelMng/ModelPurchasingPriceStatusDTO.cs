namespace DTO.ModelMng
{
    public class ModelPurchasingPriceStatusDTO
    {
        public int ModelPurchasingPriceStatusID { get; set; }
        public int? FactoryID { get; set; }
        public int? ModelID { get; set; }
        public string Season { get; set; }
        public bool IsConfirmed { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
    }
}
