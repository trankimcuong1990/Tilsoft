using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Agents.DTO
{
    public class SearchClientDTO
    {
        public int ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string ZipCode { get; set; }
        public Nullable<int> ClientCityID { get; set; }
        public string ClientCityNM { get; set; }
        public Nullable<int> ClientCountryID { get; set; }
        public string ClientCountryNM { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
    }
}
