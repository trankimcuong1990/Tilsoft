using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferToClientMng.DTO
{
    public class OfferSeasonDetailSearchFormData
    {
        public OfferSeasonDetailSearchFormData()
        {
            Data = new List<OfferSeasonDetailDTO>();
        }
        public List<OfferSeasonDetailDTO> Data { get; set; }
        public int TotalRows { get; set; }
    }
}
