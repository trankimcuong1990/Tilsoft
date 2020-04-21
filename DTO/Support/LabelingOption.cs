using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class LabelingOption
    {
        public int LabelingOptionID { get; set; }
        public string LabelingOptionNM { get; set; }
    }
}