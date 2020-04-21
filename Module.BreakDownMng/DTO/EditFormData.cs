using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class EditFormData
    {
        public DTO.BreakdownDTO Data { get; set; }
        public List<DTO.BreakdownCategoryDTO> BreakdownCategoryDTOs { get; set; }
        public decimal ExchangeRate { get; set; }
        public int FactoryID { get; set; }
        public List<DTO.FactoryDTO> FactoryDTOs { get; set; }
        public List<DTO.AvailableOptionByArticleCodeDTO> AvailableOptionByArticleCodeDTOs { get; set; }
    }
}
