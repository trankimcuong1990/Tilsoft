using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class SearchFormData
    {
        public List<OfferSeasonSearchDTO> Data { get; set; } 
        public List<OfferSeasonTypeDTO> OfferSeasonTypeDTOs { get; set; }
        public List<AccManagerDTO> AccManagerDTOs { get; set; }
    }
}
