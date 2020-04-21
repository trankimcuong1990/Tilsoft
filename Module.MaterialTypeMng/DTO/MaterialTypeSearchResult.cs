using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MaterialTypeMng.DTO
{
    public class MaterialTypeSearchResult
    {
        public int MaterialTypeID { get; set; }
        public string MaterialTypeUD { get; set; }
        public string MaterialTypeNM { get; set; }
        public string HangTagFileUrl { get; set; }
        public string HangTagFileFriendlyName { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdateName { get; set; }
        public decimal Total { get; set; }
    }
}
