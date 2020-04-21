using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class ContainerType
    {
        public int ConstantEntryID { get; set; }

        [Display(Name = "ContainerTypeID")]
        public int? ContainerTypeID { get; set; }

        [Display(Name = "ContainerTypeNM")]
        public string ContainerTypeNM { get; set; }

        [Display(Name = "DisplayOrder")]
        public int? DisplayOrder { get; set; }

    }
}