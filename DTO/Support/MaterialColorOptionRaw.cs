using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class MaterialColorOptionRaw
    {
        public int MaterialColorID { get; set; }

        public string MaterialColorUD { get; set; }

        public string MaterialColorNM { get; set; }

        public int? MaterialTypeID { get; set; }

        public int? MaterialID { get; set; }

    }
}