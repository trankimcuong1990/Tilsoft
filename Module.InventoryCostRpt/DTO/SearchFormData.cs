namespace Module.InventoryCostRpt.DTO
{
    using System.Collections.Generic;

    public class SearchFormData
    {
        public SearchFormData()
        {
            Data = new List<InventoryCostData>();
        }

        public List<InventoryCostData> Data { get; set; }
    }
}
