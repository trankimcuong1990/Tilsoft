using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class NotifyType
    {
        public int NotifyTypeID { get; set; }

        public string NotifyTypeNM { get; set; }

        public int? DisplayOrder { get; set; }

    }
}