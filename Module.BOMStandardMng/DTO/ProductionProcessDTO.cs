using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BOMStandardMng.DTO
{
    public class ProductionProcessDTO
    {
        public int ProductionProcessID { get; set; }
        public int? ProductID { get; set; }
        public int? SampleProductID { get; set; }
        public int? OPSequenceID { get; set; }
        public int? CompanyID { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsDefaultOfModel { get; set; }
        public string ProductDescription { get; set; }
        public string ProductArticleCode { get; set; }
        public string SampleArticleDescription { get; set; }
        public int? BOMStandardID { get; set; }
        public bool? HasBOM { get; set; }
    }
}
