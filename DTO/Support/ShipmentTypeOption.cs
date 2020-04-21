using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class ShipmentTypeOption
    {
        public int ShipmentTypeOptionID { get; set; }
        public string ShipmentTypeOptionNM { get; set; }
    }
}