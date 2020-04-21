using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AccountPayableRpt.DTO
{
    public class LiabilitiesDto
    {
        public List<TotalLiabilities> totalLiabilities { get; set; }
        public List<SumDetailOfLiabilitiesDto> sumDetailOfLiabilities { get; set; }
        public List<DueColorDTO> dueColorDTOs { get; set; }
        public List<ListChartDetailDTO> listChartDetailDTOs { get; set; }
        public List<ListColorCatagoryDTO> ListColorCatagoryDTOs { get; set; }
    }
}
