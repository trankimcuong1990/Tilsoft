using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.CushionMng
{
    public class Cushion
    {
        public int CushionID { get; set; }

        [Required]
        public string CushionUD { get; set; }

        [Required]
        public string CushionNM { get; set; }

        public string Season { get; set; }
        public bool IsStandard { get; set; }
        public int DisplayIndex { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public string ConcurrencyFlag_String { get; set; }

        public string ImageFile { get; set; }
        public bool ImageFile_HasChange { get; set; }
        public string ImageFile_NewFile { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
    }
}