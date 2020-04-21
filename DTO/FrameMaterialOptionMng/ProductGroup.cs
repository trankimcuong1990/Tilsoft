using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.FrameMaterialOptionMng
{
    public class ProductGroup
    {
        public int FrameMaterialOptionProductGroupID { get; set; }
        public int ProductGroupID { get; set; }
        public bool IsEnabled { get; set; }
        public string ProductGroupNM { get; set; }
    }
}