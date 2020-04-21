using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryGoodsProcedure.DTO
{
    public class FactoryGoodsProcedureDTO
    {
        public int FactoryGoodsProcedureID { get; set; }
        public string FactoryGoodsProcedureUD { get; set; }
        public string FactoryGoodsProcedureNM { get; set; }

        public List<FactoryGoodsProcedureDetailDTO> FactoryGoodsProcedureDetailDTOs { get; set; }
    }
}
