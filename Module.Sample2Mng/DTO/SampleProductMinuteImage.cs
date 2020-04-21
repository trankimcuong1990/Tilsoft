﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample2Mng.DTO
{
    public class SampleProductMinuteImage
    {
        public int SampleProductMinuteImageID { get; set; }
        public string FileUD { get; set; }
        public string Description { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }

        public bool HasChanged { get; set; }
        public string NewFile { get; set; }
    }
}
