using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.MIOverviewRpt
{
    public class DDC
    {
        public string DisplayText { get; set; }
        public decimal? FobAmount { get; set; }
        public decimal? FrancoAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? ContAmount { get; set; }
    }
}