using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.DashboardMng
{
    public class SaleByCountryAndSale
    {
        public string Season { get; set; }

        public string ClientCountryNM { get; set; }

        public string SaleUD { get; set; }

        public decimal? TotalCont { get; set; }

        public decimal? USDAmount { get; set; }

        public decimal? EURAmount { get; set; }

        public decimal? TotalEURAmount { get; set; }
        public int? NumberOfClient { get; set; }
    }
}