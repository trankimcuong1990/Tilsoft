using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Agents.DTO
{
    public class AgentsDTO
    {
        public int AgentID { get; set; }
        public string AgentUD { get; set; }
        public string AgentNM { get; set; }
        public string ShortName { get; set; }
        public Nullable<bool> Visible { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public Nullable<int> ClientCountryID { get; set; }
        public Nullable<int> ClientCityID { get; set; }
        public string ZipCode { get; set; }
        public Nullable<bool> isActive { get; set; }
        public string StreetAddress { get; set; }
        public string ClientCityUD { get; set; }
        public string ClientCityNM { get; set; }
        public string ClientCountryNM { get; set; }
        public string ClientCountryUD { get; set; }
    }
}
