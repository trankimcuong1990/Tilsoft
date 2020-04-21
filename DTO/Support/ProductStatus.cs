using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class ProductStatus
    {
        public int ConstantEntryID { get; set; }
        public int ProductStatusID { get; set; }
        public string ProductStatusNM { get; set; }
        public int DisplayOrder { get; set; }
    }
}