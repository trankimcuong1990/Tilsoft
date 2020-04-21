using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.MISaleByCountryRpt
{
    public class Summary
    {
        public string Season { get; set; }
        public string ClientCountryUD { get; set; }
        public string ClientCountryNM { get; set; }
        public string ClientCodeForLog { get; set; }
        public decimal? TotalCont { get; set; }
        public decimal? TotalEURAmount { get; set; }
        public decimal? USDAmount { get; set; }
        public decimal? EURAmount { get; set; }
        public int? TotalClient { get; set; }
    }
}