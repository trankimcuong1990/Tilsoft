using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class ClientCity
    {
        public int? ClientCityID { get; set; }
        public string ClientCityUD { get; set; }
        public string ClientCityNM { get; set; }
        public string ClientCountryNM { get; set; }
    }
}
