//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.SeatCushionMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class SeatCushionMng_SeatCushionProductGroup_View
    {
        public int SeatCushionProductGroupID { get; set; }
        public Nullable<int> SeatCushionID { get; set; }
        public Nullable<int> ProductGroupID { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
        public string ProductGroupNM { get; set; }
    
        public virtual SeatCushionMng_SeatCushion_View SeatCushionMng_SeatCushion_View { get; set; }
    }
}
