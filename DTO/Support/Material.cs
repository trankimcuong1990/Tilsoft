﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class Material
    {
        public int MaterialID { get; set; }
        public string MaterialUD { get; set; }
        public string MaterialNM { get; set; }
    }
}