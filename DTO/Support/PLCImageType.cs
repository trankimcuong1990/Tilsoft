﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class PLCImageType
    {
        public int PLCImageTypeID { get; set; }
        public string PLCImageTypeNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}