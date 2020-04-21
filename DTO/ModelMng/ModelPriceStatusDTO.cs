namespace DTO.ModelMng
{
    public class ModelPriceStatusDTO
    {
        public int ModelPriceStatusID { get; set; }
        public int? ModelID { get; set; }
        public string Season { get; set; }
        public bool IsConfirmed { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }

    }
}
