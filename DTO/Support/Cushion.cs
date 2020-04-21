using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class Cushion
    {
        public int CushionID { get; set; }

        [Display(Name = "CushionUD")]
        public string CushionUD { get; set; }

        [Display(Name = "CushionNM")]
        public string CushionNM { get; set; }

    }
}