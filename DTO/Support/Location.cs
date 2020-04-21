using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class Location
    {
        public int LocationID { get; set; }

        [Display(Name = "LocationNM")]
        public string LocationNM { get; set; }
    }
}