using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class FSCType
    {
        public int FSCTypeID { get; set; }
        public string FSCTypeUD { get; set; }
        public string FSCTypeNM { get; set; }
    }
}