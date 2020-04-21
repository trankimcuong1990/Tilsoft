using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.MISaleConclusionRpt
{
    public class RangeExpected
    {
        public string RangeNM { get; set; }
        public int? TotalClient { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}