using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class SeasonType
    {
        public int SeasonTypeID { get; set; }

        public string SeasonTypeNM { get; set; }

        public int DisplayOrder { get; set; }

    }
}