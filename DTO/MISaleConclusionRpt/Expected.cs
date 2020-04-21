using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.MISaleConclusionRpt
{
    public class Expected
    {
        public int? ClientID { get; set; }
        public string ClientNM { get; set; }
        public string Season { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}