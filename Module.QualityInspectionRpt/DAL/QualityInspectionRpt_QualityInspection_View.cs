//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.QualityInspectionRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class QualityInspectionRpt_QualityInspection_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QualityInspectionRpt_QualityInspection_View()
        {
            this.QualityInspectionRpt_QualityInspectionCategory_View = new HashSet<QualityInspectionRpt_QualityInspectionCategory_View>();
        }
    
        public int QualityInspectionID { get; set; }
        public Nullable<int> QualityInspectionTypeID { get; set; }
        public string QualityInspectionTypeNM { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string ClientUD { get; set; }
        public Nullable<int> QCID { get; set; }
        public string QCName { get; set; }
        public Nullable<System.DateTime> InspectionDate { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string WorkCenterMethod { get; set; }
        public Nullable<decimal> WorkCenterQuantity { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        public Nullable<int> QualityInspectionCorrectActionID { get; set; }
        public string QualityInspectionCorrectActionNM { get; set; }
        public Nullable<int> TeamLeaderID { get; set; }
        public string TeamLeaderName { get; set; }
        public Nullable<int> ProductionSupervisorID { get; set; }
        public string ProductionSupervisorName { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<int> MaterialColorID { get; set; }
        public string MaterialColorNM { get; set; }
        public Nullable<System.DateTime> WorkCenterWorkingDate { get; set; }
        public string ApprovedName { get; set; }
        public string SubSupplierFullNM { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QualityInspectionRpt_QualityInspectionCategory_View> QualityInspectionRpt_QualityInspectionCategory_View { get; set; }
    }
}
