using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySalesQuotationMng.DTO
{
   public class FactorySaleQuotationAttachedFileDTO
    {
        public int FactorySaleQuotationAttachedFileID { get; set; }
        public int? FactorySaleQuotationID { get; set; }
        public string FileUD { get; set; }        
        public string Remark { get; set; }

        public string OtherFile { get; set; }        
        public string OtherFileUrl { get; set; }
        public string OtherFileFriendlyName { get; set; }
        public bool OtherFileHasChange { get; set; }
        public string NewOtherFile { get; set; }
        public bool RemarkTextHasChange { get; set; }
       
    }
}
