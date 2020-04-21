using System;

namespace DTO.ProductMng
{
    public class ModelPackagingMethodOption2DTO
    {
        public int ModelPackagingMethodOptionID { get; set; }
        public int? ModelID { get; set; }
        public int? PackagingMethodID { get; set; }
        public bool? IsDefault { get; set; }
        public string Description { get; set; }
        public string CartonBoxDimL { get; set; }
        public string CartonBoxDimW { get; set; }
        public string CartonBoxDimH { get; set; }
        public int? Qnt20DC { get; set; }
        public int? Qnt40DC { get; set; }
        public int? Qnt40HC { get; set; }
        public string CBM { get; set; }
        public string QntInBox { get; set; }
        public string GrossWeight { get; set; }
        public string NetWeight { get; set; }
        public int? ClientSpecialPackagingMethodID { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsConfirmed { get; set; }
        public int? ConfirmedBy { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public string UpdatorName { get; set; }
        public string ConfirmerName { get; set; }
        public string PackagingMethodUD { get; set; }
        public string PackagingMethodNM { get; set; }
        public string PackingRemark { get; set; }
        public int? LoadAbilityTypeID { get; set; }
    }
}