using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceListFile.DTO
{
    public class PriceListFileSearch
    {
        public int? PriceListFileID { get; set; }
        public string Season { get; set; }
        public string PDFFileUD { get; set; }
        public string ExcelFileUD { get; set; }
        public string Comment { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string PDFFriendlyName { get; set; }
        public string PDFFileLocation { get; set; }
        public string ExcelFriendlyName { get; set; }
        public string ExcelFileLocation { get; set; }
        public string UpdatorNM { get; set; }
    }
}
