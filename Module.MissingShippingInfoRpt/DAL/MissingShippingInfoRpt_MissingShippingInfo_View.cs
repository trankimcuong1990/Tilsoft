//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.MissingShippingInfoRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class MissingShippingInfoRpt_MissingShippingInfo_View
    {
        public int ShippingInstructionID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string Season { get; set; }
        public string ClientUD { get; set; }
        public Nullable<System.DateTime> PIReceivedDate { get; set; }
        public Nullable<System.DateTime> LDS { get; set; }
        public string PIReceivedDateFormatted { get; set; }
        public string LDSFormatted { get; set; }
    }
}
