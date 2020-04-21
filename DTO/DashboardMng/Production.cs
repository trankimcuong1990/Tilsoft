using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.DashboardMng
{
    public class Production
    {
        public string ManufacturerCountryNM { get; set; }
        public decimal? TotalOrder { get; set; }
        public decimal? TotalShipped { get; set; }
        public decimal? TotalToBeShipped { get; set; }
        public decimal? PercentShipped { get; set; }
        public decimal? PercentToBeShipped { get; set; }
        public string Season { get; set; }
    }
}