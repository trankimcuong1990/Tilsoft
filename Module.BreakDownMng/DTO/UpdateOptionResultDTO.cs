using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class UpdateOptionResultDTO
    {
        public DTO.BreakdownCategoryOptionDTO Data { get; set; }
        public List<DTO.AvailableOptionByArticleCodeDTO> AvailableOptionByArticleCodeDTOs { get; set; }
    }
}
