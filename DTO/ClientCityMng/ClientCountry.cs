using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ClientCityMng
{
    public class ClientCountry
    {

        public int ClientCountryID { get; set; }

       // public string ClientCountryUD { get; set; }

        public string ClientCountryNM { get; set; }

        //public string UpdatorName { get; set; }


        //public Nullable<System.DateTime> UpdatedDate { get; set; }

        //public string CreatorName { get; set; }

        //public Nullable<System.DateTime> CreatedDate { get; set; }

        //public byte[] ConcurrencyFlag { get; set; }
    }
}
