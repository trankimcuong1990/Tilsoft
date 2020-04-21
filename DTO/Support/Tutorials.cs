using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class Tutorials
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        
    }
}
