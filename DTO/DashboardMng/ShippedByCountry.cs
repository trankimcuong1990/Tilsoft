using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.DashboardMng
{
    public class ShippedByCountry
    {

        public string Season { get; set; }

        public string ClientCountryNM { get; set; }

        public string MaterialNM { get; set; }

        public decimal? TotalQnt { get; set; }

        public decimal? TotalShipped { get; set; }

        public decimal? ToBeShipped { get; set; }

    }
}