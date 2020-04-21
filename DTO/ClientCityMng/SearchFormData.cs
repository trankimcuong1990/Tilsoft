using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientCityMng
{
    public class SearchFormData
    {
        //public List<ClientCityMng.ClientCitySearchResult> ClientCountrys;
        public List<DTO.Support.ClientCountry> ClientCountrys { get; set; }
        public List<DTO.ClientCityMng.ClientCitySearchResult> Data { get; set; }
    }
}
