using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Agents.DTO
{
    public class ClientCitiesDTO
    {
        public int ClientCityID { get; set; }
        public string ClientCityUD { get; set; }
        public string ClientCityNM { get; set; }
        public string ClientCountryNM { get; set; }
        public Nullable<int> ClientCountryID { get; set; }
    }
}
