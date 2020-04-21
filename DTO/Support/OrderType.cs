using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class OrderType
    {
        public int OrderTypeID { get; set; }
        public string OrderTypeNM { get; set; }
    }
}