using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Agents.DTO
{
    public class EditDTO
    {
        public EditDTO() {
            data = new AgentsDTO();
            amountByClientAndSeasons = new List<AmountByClientAndSeason>();
            clientCountryDTOs = new List<ClientCountryDTO>();
            clientCountryDTOs = new List<ClientCountryDTO>();
            Seasons = new List<Support.DTO.Season>();
        }
        public AgentsDTO data { get; set; }
        public List<ClientCitiesDTO> clientCitiesDTOs { get; set; }
        public List<ClientCountryDTO>clientCountryDTOs { get; set; }
        public List<AmountByClientAndSeason> amountByClientAndSeasons { get; set; }
        public List<Support.DTO.Season> Seasons { get; set; }
    }
}
