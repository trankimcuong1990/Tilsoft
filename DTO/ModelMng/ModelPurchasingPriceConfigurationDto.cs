using System.Collections.Generic;

namespace DTO.ModelMng
{
    public class ModelPurchasingPriceConfigurationDTO
    {
        public ModelPurchasingPriceConfigurationDTO()
        {
            ModelPurchasingPriceConfigurationDetailDTOs = new List<ModelPurchasingPriceConfigurationDetailDTO>();
        }

        public int ModelPurchasingPriceConfigurationID { get; set; }
        public int? FactoryID { get; set; }
        public int? ModelID { get; set; }
        public string Season { get; set; }
        public int? ProductElementID { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ProductElementNM { get; set; }
        public List<DTO.ModelMng.ModelPurchasingPriceConfigurationDetailDTO> ModelPurchasingPriceConfigurationDetailDTOs { get; set; }
    }
}
