using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Module.PriceListFile.DTO
{
    public class PriceListFile
    {
        public int? PriceListFileID { get; set; }
        public string Season { get; set; }
        public string PDFFileUD { get; set; }
        public string ExcelFileUD { get; set; }
        public string Comment { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public object ConcurrencyFlag { get; set; }

        //Updator Name
        public string UpdatorNM { get; set; }

        // PDF File
        public string PDFFriendlyName { get; set; }
        public string PDFFileLocation { get; set; }
        public string NewPDFFile { get; set; }
        public bool PDFFileLocation_HasChange { get; set; }

        // Excel File
        public string ExcelFriendlyName { get; set; }
        public string ExcelFileLocation { get; set; }
        public string NewExcelFile { get; set; }
        public bool ExcelFileLocation_HasChange { get; set; }

        //
        // concurrency
        //
        public string ConcurrencyFlag_String { get; set; }
    }
}
