using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class Country
    {
        public int ConstantEntryID { get; set; }
        public int CountryID { get; set; }
        public string CountryNM { get; set; }
        public int DisplayOrder { get; set; }
    }
}