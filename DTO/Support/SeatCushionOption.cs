using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class SeatCushionOption
    {
        public int SeatCushionID { get; set; }
        public bool IsStandard { get; set; }
        public bool IsEnabled { get; set; }
        public int DisplayIndex { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string Season { get; set; }
        public string SeatCushionUD { get; set; }
        public string SeatCushionNM { get; set; }
        public bool IsSelected { get; set; }
    }
}