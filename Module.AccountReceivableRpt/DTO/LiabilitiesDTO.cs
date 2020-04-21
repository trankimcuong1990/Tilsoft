using System.Collections.Generic;

namespace Module.AccountReceivableRpt.DTO
{
    public class LiabilitiesDTO
    {
        public List<TotalLiabilities> totalLiabilities { get; set; }
        public List<SumDetailOfLiabilitiesDto> sumDetailOfLiabilities { get; set; }
        public List<DueColorDTO> dueColorDTOs { get; set; }
        public List<ListChartDetailDTO> listChartDetailDTOs { get; set; }
        public List<ListColorCatagoryDTO> ListColorCatagoryDTOs { get; set; }
    }
}
