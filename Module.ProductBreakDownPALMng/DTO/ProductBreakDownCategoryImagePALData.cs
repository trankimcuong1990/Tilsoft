﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownPALMng.DTO
{
    public class ProductBreakDownCategoryImagePALData
    {
        public int ProductBreakDownCategoryImageID { get; set; }
        public int? ProductBreakDownCategoryID { get; set; }
        public string FileUD { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public bool? HasChange { get; set; }
        public string NewFile { get; set; }
    }
}
