using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientOfferMng.DTO
{
    public class SearchFormData
    {
        public List<ClientOfferSearch> Data { get; set; }
        public List<Module.Support.DTO.Client> Clients { get; set; }
    }
}
