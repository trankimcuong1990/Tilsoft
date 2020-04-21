using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class ProductCategory
    {
        public int? ProductCategoryID { get; set; }
        public string ProductCategoryNM { get; set; }
    }
}