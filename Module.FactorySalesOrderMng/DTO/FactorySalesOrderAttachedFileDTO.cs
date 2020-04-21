using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySalesOrderMng.DTO
{
   public class FactorySalesOrderAttachedFileDTO
    {
        public int FactorySaleOrderAttachedFileID { get; set; }
        public int? FactorySaleOrderID { get; set; }
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
