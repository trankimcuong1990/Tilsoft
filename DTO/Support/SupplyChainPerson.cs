using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class SupplyChainPerson
    {
        public int SupplyChainPersonID { get; set; }
        public string SupplyChainPersonNM { get; set; }
        public int? LocationInChargeID { get; set; }
    }
}