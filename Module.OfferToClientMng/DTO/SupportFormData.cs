using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferToClientMng.DTO
{
    public class SupportFormData
    {
        public List<SeasonDTO> Seasons { get; set; }
        public List<OfferSeasonTypeDTO> OfferSeasonTypes { get; set; }
        public List<ClientDTO> Clients { get; set; }
    }
}
