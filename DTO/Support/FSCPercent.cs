﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class FSCPercent
    {
        public int FSCPercentID { get; set; }
        public string FSCPercentUD { get; set; }
        public string FSCPercentNM { get; set; }
        public int FSCTypeID { get; set; }
    }
}