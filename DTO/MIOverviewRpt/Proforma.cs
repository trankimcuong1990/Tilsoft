using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.MIOverviewRpt
{
    public class Proforma
    {
        public string DisplayText { get; set; }
        public decimal? LastYearUSD { get; set; }
        public decimal? LastYearEUR { get; set; }
        public decimal? LastYearTotalEUR { get; set; }
        public decimal? LastYearCont { get; set; }
        public decimal? ThisYearUSD { get; set; }
        public decimal? ThisYearEUR { get; set; }
        public decimal? ThisYearTotalEUR { get; set; }
        public decimal? ThisYearCont { get; set; }
    }
}