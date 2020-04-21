using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class ModelSearchFormData
    {
        public ModelSearchFormData()
        {
            Data = new List<ModelDTO>();
        }
        public List<ModelDTO> Data { get; set; }
        public int TotalRows { get; set; }
    }
}
