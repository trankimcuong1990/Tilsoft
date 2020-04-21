using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ConstantEntryMng
{
    public class ConstantEntry
    {
        public int ConstantEntryID { get; set; }

        public int EntryValue { get; set; }

         [Required]
        [Display(Name = "DisplayText")]
        public string DisplayText { get; set; }
         [Required]
         [Display(Name = "EntryGroup")]
        public string EntryGroup { get; set; }

        public int? DisplayOrder { get; set; }
      

        //[Display(Name = "EntryValue")]
        //public int EntryValue { get; set; }
       
        //public string DisplayText { get; set; }

        //[Required ]
        //[Display(Name="ConstantEntry")]
        //public string EntryGroup { get; set; }

        //public int EntryValue { get; set;}

        //public int DisplayOrder { get; set; }
    }
}