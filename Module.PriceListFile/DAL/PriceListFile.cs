//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PriceListFile.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PriceListFile
    {
        public int PriceListFileID { get; set; }
        public string Season { get; set; }
        public string PDFFileUD { get; set; }
        public string ExcelFileUD { get; set; }
        public string Comment { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
    }
}
