using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class ShipmentToOption
    {
        public int ShipmentToOptionID { get; set; }
        public string ShipmentToOptionNM { get; set; }
    }
}