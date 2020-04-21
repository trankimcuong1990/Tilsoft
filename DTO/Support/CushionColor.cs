using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class CushionColor
    {
        public int CushionColorID { get; set; }

        [Display(Name = "CushionColorUD")]
        public string CushionColorUD { get; set; }

        [Display(Name = "CushionColorNM")]
        public string CushionColorNM { get; set; }

    }
}