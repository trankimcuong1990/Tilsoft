using System.Collections.Generic;

namespace DTO.ProductLoadAbilityMng
{
    public class DataContainerDto
    {
        public List<ProductLoadAbilityDto> ListProductLoadAbilityDto { get; set; }
        public List<ModelDto> ListModelDto { get; set; }
        public List<MaterialOptionDto> ListMaterialOptionDto { get; set; }
        public List<CushionOptionDto> ListCushionOptionDto { get; set; }
        public List<FrameMaterialDto> ListFrameMaterialDto { get; set; }
        public List<PackagingMethodDto> ListPackagingMethodDto { get; set; } 
    }

    public class ProductLoadAbilityDto
    {
        public int ProductLoadAbilityId { get; set; }
        public int ModelId { get; set; }
        public int MaterialOptionId { get; set; }
        public int CushionOptionId { get; set; }
        public int FrameMaterialId { get; set; }
        public int PackagingMethodId { get; set; }
        public int Qnt20Dc { get; set; }
        public int Qnt40Dc { get; set; }
        public int Qnt40Hc { get; set; }
        public string Comment { get; set; }
        public bool IsConfirmed { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
    }
    public class ModelDto
    {
        public int ModelId { get; set; }
        public string ModelUd { get; set; }
        public string ModelNm { get; set; }
    }
    public class MaterialOptionDto
    {
        public int MaterialOptionId { get; set; }
        public string MaterialOptionUd { get; set; }
        public string MaterialOptionNm { get; set; }
    }
    public class CushionOptionDto
    {
        public int CushionOptionId { get; set; }
        public string CushionOptionUd { get; set; }
        public string CushionOptionNm { get; set; }
    }
    public class FrameMaterialDto
    {
        public int FrameMaterialId { get; set; }
        public string FrameMaterialUd { get; set; }
        public string FrameMaterialNm { get; set; }
       
    }
    public class PackagingMethodDto
    {
        public int PackagingMethodId { get; set; }
        public string PackagingMethodUd { get; set; }
        public string PackagingMethodNm { get; set; }
    }
}
