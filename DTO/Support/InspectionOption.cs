﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class InspectionOption
    {
        public int InspectionOptionID { get; set; }
        public string InspectionOptionNM { get; set; }
    }
}