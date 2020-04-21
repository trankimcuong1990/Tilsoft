using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Module.BackCushionMng.DTO
{
    public class BackCushionDTO
    {
        public int BackCushionID { get; set; }
        public string BackCushionUD { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Back cushion name must be from 1 to 255 chars length")]

        public string BackCushionNM { get; set; }
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
        public List<ProductGroupDTO> ProductGroups { get; set; }

        public string CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public string CreatorName { get; set; }

        public string CreatorName2 { get; set; }
        public string UpdatorName2 { get; set; }
        public int Total { get; set; }

        public static explicit operator JObject(BackCushionDTO v)
        {
            throw new NotImplementedException();
        }
    }
}
