﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class SeatCushionOptionDTO
    {
        public int SeatCushionID { get; set; }
        public string SeatCushionNM { get; set; }

        public string Season { get; set; }
        public bool IsStandard { get; set; }
        public int DisplayIndex { get; set; }
        public string ThumbnailLocation { get; set; }
    }
}
