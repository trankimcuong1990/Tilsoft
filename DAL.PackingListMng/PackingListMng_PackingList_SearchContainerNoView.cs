//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.PackingListMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class PackingListMng_PackingList_SearchContainerNoView
    {
        public long PrimaryID { get; set; }
        public Nullable<int> PackingListID { get; set; }
        public string ContainerNo { get; set; }
    
        public virtual PackingListMng_PackingList_SearchView PackingListMng_PackingList_SearchView { get; set; }
    }
}