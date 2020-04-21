using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryTeam.DTO
{
    public class FactoryTeamDTO
    {
        public int FactoryTeamID { get; set; }
        public string FactoryTeamUD { get; set; }
        public string FactoryTeamNM { get; set; }
        public List<FactoryTeamStepDTO> FactoryTeamStepDTOs { get; set; }
        public List<FactoryMaterialGroupTeamDTO> FactoryMaterialGroupTeamDTOs { get; set; }
    }
}
