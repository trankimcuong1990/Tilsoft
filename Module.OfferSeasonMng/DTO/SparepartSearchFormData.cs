using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    class SparepartSearchFormData
    {
        public SparepartSearchFormData()
        {
            Data = new List<SparepartDTO>();
        }
        public List<SparepartDTO> Data { get; set; }
        public int TotalRows { get; set; }
    }
}
