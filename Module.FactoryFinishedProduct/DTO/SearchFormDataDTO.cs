using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryFinishedProduct.DTO
{
    public class SearchFormDataDTO
    {
        public List<FactoryFinishedProductDTO> Data { get; set; }
        public List<Support.DTO.FactoryTeam> FactoryTeams { get; set; }
        public List<Support.DTO.FactoryStep> FactorySteps { get; set; }
        
    }
}
