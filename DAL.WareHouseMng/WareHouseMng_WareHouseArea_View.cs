//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.WareHouseMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class WareHouseMng_WareHouseArea_View
    {
        public int WareHouseAreaID { get; set; }
        public Nullable<int> WareHouseID { get; set; }
        public string AreaCode { get; set; }
        public string Description { get; set; }
    
        public virtual WareHouseMng_WareHouse_View WareHouseMng_WareHouse_View { get; set; }
    }
}