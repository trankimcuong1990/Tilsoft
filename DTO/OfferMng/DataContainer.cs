using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.OfferMng
{
    public class DataContainer
    {
        public Offer OfferData { get; set; }
        public SupportDataContainer SupportData { get; set; }
        public List<FactoryMng_Function_GetFactoryPermission_ResultDTO> FactoryDTOs { get; set; }
    }
}
