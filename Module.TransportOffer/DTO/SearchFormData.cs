using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportOffer.DTO
{
    public class SearchFormData
    {
        public List<TransportOfferSearchDTO> Data { get; set; }
        public List<Module.Support.DTO.Forwarder> Forwarders { get; set; }
    }
}
