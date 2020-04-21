using System.Collections.Generic;

namespace Module.PurchasingPriceComparisonMng.DTO
{
    public class InitFormData
    {
        public string CurrentSeason { get; set; }
        public List<Support.DTO.Season> Seasons { get; set; }

        public InitFormData()
        {
            Seasons = new List<Support.DTO.Season>();
        }
    }
}
