using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.CushionColorMng.DTO
{
    public class CushionType
    {
        public int CushionTypeID { get; set; }
        public string CushionTypeNM { get; set; }
        public int DisplayOrder { get; set; }
    }
}