using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.DDCMng
{
    public class DDCSearchResult
    {
        public int DDCID { get; set; }
        public string Season { get; set; }
    }
}