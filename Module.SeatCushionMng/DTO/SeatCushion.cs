using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.SeatCushionMng.DTO
{
    public class SeatCushion
    {
        public int SeatCushionID { get; set; }
        public string SeatCushionUD { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Seat cushion name must be from 1 to 255 chars length")]

        public string SeatCushionNM { get; set; }
        public string Season { get; set; }
        public string ImageFile { get; set; }
        public bool? IsStandard { get; set; }
        public bool? IsEnabled { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ConcurrencyFlag_String { get; set; }
        public string UpdatorName { get; set; }
        public bool ImageFile_HasChange { get; set; }
        public string ImageFile_NewFile { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public int DisplayIndex { get; set; }
        public string ProductGroupNM { get; set; }
        public List<ProductGroup> ProductGroups { get; set; }
        public List<spProductGroup> spProductGroups { get; set; }
        public string CreatedDate { get; set; }

        public int? CreatedBy { get; set; }
        public string CreatorName { get; set; }
        
        public int Total { get; set; }
    }
}