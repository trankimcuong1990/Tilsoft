using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class PriceDifference
    {
        public int PriceDifferenceID { get; set; }
        public string PriceDifferenceUD { get; set; }
        public decimal Rate { get; set; }
        public string Season { get; set; }
    }
}