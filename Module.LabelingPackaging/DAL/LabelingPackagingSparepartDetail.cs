//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.LabelingPackaging.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class LabelingPackagingSparepartDetail
    {
        public int LabelingPackagingSparepartDetailID { get; set; }
        public Nullable<int> LabelingPackagingID { get; set; }
        public Nullable<int> FactoryOrderSparepartDetailID { get; set; }
        public string HangTagFile { get; set; }
        public string BoxShippingMarkFile { get; set; }
        public string BrassLabelFile { get; set; }
        public string AIFile { get; set; }
        public string WashCushionLabelFile { get; set; }
        public string Option1File { get; set; }
        public string Option2File { get; set; }
        public string Option3File { get; set; }
        public string Option4File { get; set; }
        public string Option5File { get; set; }
        public string Option6File { get; set; }
        public string Option7File { get; set; }
        public string Option8File { get; set; }
        public string Option9File { get; set; }
        public string Option10File { get; set; }
        public string Option11File { get; set; }
        public string Option12File { get; set; }
        public string Option13File { get; set; }
        public string Option14File { get; set; }
        public string Option15File { get; set; }
        public string Option4FilePI { get; set; }
        public string Option5FilePI { get; set; }
        public string Option6FilePI { get; set; }
        public string Option7FilePI { get; set; }
        public string Option8FilePI { get; set; }
        public string Option9FilePI { get; set; }
        public string Option10FilePI { get; set; }
        public string Option11FilePI { get; set; }
        public string Option12FilePI { get; set; }
        public string Option13FilePI { get; set; }
        public string Option14FilePI { get; set; }
        public string Option15FilePI { get; set; }
    
        public virtual LabelingPackaging LabelingPackaging { get; set; }
    }
}
