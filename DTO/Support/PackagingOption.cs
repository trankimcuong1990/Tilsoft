using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class PackagingOption
    {
        public int PackagingOptionID { get; set; }
        public string PackagingOptionNM { get; set; }
    }
}