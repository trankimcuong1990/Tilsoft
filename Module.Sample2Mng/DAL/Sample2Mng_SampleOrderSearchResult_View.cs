//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.Sample2Mng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sample2Mng_SampleOrderSearchResult_View
    {
        public int SampleOrderID { get; set; }
        public string SampleOrderUD { get; set; }
        public string Season { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string PurposeNM { get; set; }
        public string TransportTypeNM { get; set; }
        public string SampleOrderStatusNM { get; set; }
        public string Destination { get; set; }
        public Nullable<System.DateTime> HardDeadline { get; set; }
        public Nullable<System.DateTime> Deadline { get; set; }
        public string Remark { get; set; }
        public string CreatorName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string StatusUpdatorName { get; set; }
        public Nullable<System.DateTime> StatusUpdatedDate { get; set; }
        public Nullable<int> TransportTypeID { get; set; }
        public Nullable<int> PurposeID { get; set; }
        public Nullable<int> SampleOrderStatusID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<int> SaleID { get; set; }
        public string SaleNM { get; set; }
        public Nullable<int> SampleProductTotalNumber { get; set; }
        public Nullable<int> SampleProductNewDevelopmentNumber { get; set; }
        public Nullable<int> SampleProductNoNewDevelopmentNumber { get; set; }
        public Nullable<int> SampleProductTableFinishNumber { get; set; }
        public Nullable<int> SampleProductTableInProgressNumber { get; set; }
        public Nullable<int> SampleProductdFromExistModelNumber { get; set; }
        public string RankName { get; set; }
    }
}
