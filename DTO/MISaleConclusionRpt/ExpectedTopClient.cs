using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.MISaleConclusionRpt
{
    public class ExpectedTopClient
    {
        public int? ClientCountryID { get; set; }
        public string ClientCountryUD { get; set; }
        public string ClientCountryNM { get; set; }
        public string ClientNM { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? LastYearTotalAmount { get; set; }
    }
}