using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class MaterialColor
    {
        public int MaterialColorID { get; set; }
        public string MaterialColorUD { get; set; }
        public string MaterialColorNM { get; set; }
    }
}