using System.Collections.Generic;

namespace Module.SampleItemMng.DTO
{
    public class EditFormDataDTO
    {
        public SampleProductDTO Data { get; set; }
        public List<Support.DTO.SampleProductStatus> SampleProductStatuses { get; set; }
    }
}
