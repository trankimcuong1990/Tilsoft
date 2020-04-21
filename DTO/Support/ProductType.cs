using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DTO.Support
{
    public class ProductType
    {
        public int ConstantEntryID { get; set; }
        public int ProductTypeID { get; set; }
        public string ProductTypeNM { get; set; }
        public int DisplayOrder { get; set; }
    }
}