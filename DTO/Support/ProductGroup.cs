﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class ProductGroup
    {
        public int ProductGroupID { get; set; }
        public string ProductGroupNM { get; set; }
        public int DisplayOrder { get; set; }
    }
}