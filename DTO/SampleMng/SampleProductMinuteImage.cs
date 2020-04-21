﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.SampleMng
{
    public class SampleProductMinuteImage
    {
        public int SampleProductMinuteImageID { get; set; }

        public string FileUD { get; set; }

        public string Description { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string NewFile { get; set; }
        public bool HasChange { get; set; }

    }
}