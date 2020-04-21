//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.LPOverview.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class LabelingPackagingMng_LabelingPackaging_View
    {
        public LabelingPackagingMng_LabelingPackaging_View()
        {
            this.LabelingPackagingMng_LabelingPackagingDetail_View = new HashSet<LabelingPackagingMng_LabelingPackagingDetail_View>();
            this.LabelingPackagingMng_LabelingPackagingSparepartDetail_View = new HashSet<LabelingPackagingMng_LabelingPackagingSparepartDetail_View>();
            this.LabelingPackagingMng_LabelingPackagingRemark_View = new HashSet<LabelingPackagingMng_LabelingPackagingRemark_View>();
            this.LabelingPackagingMng_LabelingPackagingOtherFile_View = new HashSet<LabelingPackagingMng_LabelingPackagingOtherFile_View>();
        }
    
        public int LabelingPackagingID { get; set; }
        public Nullable<int> FactoryOrderID { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<byte> VNRep { get; set; }
        public byte LPStatusID { get; set; }
        public byte HangTagStatusID { get; set; }
        public byte BoxShippingMarkStatusID { get; set; }
        public byte BrassLabelStatusID { get; set; }
        public byte AIStatusID { get; set; }
        public byte WashCushionLabelID { get; set; }
        public string OtherFile { get; set; }
        public string AIOHangTagFile { get; set; }
        public string AIOBoxShippingMarkFile { get; set; }
        public string AIOBrassLabelFile { get; set; }
        public string AIOAIFile { get; set; }
        public string AIOWashCushionLabelFile { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string LPStatusNM { get; set; }
        public string OtherFileFileUrl { get; set; }
        public string OtherFileFriendlyName { get; set; }
        public string AIOHangTagFile_FileUrl { get; set; }
        public string AIOHangTagFile_FriendlyName { get; set; }
        public string AIOBoxShippingMarkFile_FileUrl { get; set; }
        public string AIOBoxShippingMarkFile_FriendlyName { get; set; }
        public string AIOBrassLabelFile_FileUrl { get; set; }
        public string AIOBrassLabelFile_FriendlyName { get; set; }
        public string AIOAIFile_FileUrl { get; set; }
        public string AIOAIFile_FriendlyName { get; set; }
        public string AIOWashCushionLabelFile_FileUrl { get; set; }
        public string AIOWashCushionLabelFile_FriendlyName { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientOrderNumber { get; set; }
        public string FactoryUD { get; set; }
        public string SaleNM { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public string NameApproval { get; set; }
        public string AIOFSCLabelFile { get; set; }
        public string RejectComment { get; set; }
        public string RejectCommentFile { get; set; }
        public string AIOFSCLabelFile_FileUrl { get; set; }
        public string AIOFSCLabelFile_FriendlyName { get; set; }
        public string RejectCommentFile_FileUrl { get; set; }
        public string RejectCommentFile_FriendlyName { get; set; }
        public byte FSCLabelStatusID { get; set; }
        public Nullable<bool> IsAutoUpdateSimilarLP { get; set; }
        public Nullable<System.DateTime> LDS { get; set; }
        public string ProductionStatus { get; set; }
        public Nullable<byte> Option1StatusID { get; set; }
        public Nullable<byte> Option2StatusID { get; set; }
        public Nullable<byte> Option3StatusID { get; set; }
        public string Option1Label { get; set; }
        public string Option2Label { get; set; }
        public string Option3Label { get; set; }
        public string AIOOption1File_FileUrl { get; set; }
        public string AIOOption1File_FriendlyName { get; set; }
        public string AIOOption2File_FileUrl { get; set; }
        public string AIOOption2File_FriendlyName { get; set; }
        public string AIOOption3File_FileUrl { get; set; }
        public string AIOOption3File_FriendlyName { get; set; }
        public Nullable<byte> Option4StatusID { get; set; }
        public string Option4Label { get; set; }
        public Nullable<byte> Option5StatusID { get; set; }
        public string Option5Label { get; set; }
        public Nullable<byte> Option6StatusID { get; set; }
        public string Option6Label { get; set; }
        public Nullable<byte> Option7StatusID { get; set; }
        public string Option7Label { get; set; }
        public Nullable<byte> Option8StatusID { get; set; }
        public string Option8Label { get; set; }
        public Nullable<byte> Option9StatusID { get; set; }
        public string Option9Label { get; set; }
        public Nullable<byte> Option10StatusID { get; set; }
        public string Option10Label { get; set; }
        public Nullable<byte> Option11StatusID { get; set; }
        public string Option11Label { get; set; }
        public Nullable<byte> Option12StatusID { get; set; }
        public string Option12Label { get; set; }
        public Nullable<byte> Option13StatusID { get; set; }
        public string Option13Label { get; set; }
        public Nullable<byte> Option14StatusID { get; set; }
        public string Option14Label { get; set; }
        public Nullable<byte> Option15StatusID { get; set; }
        public string Option15Label { get; set; }
    
        public virtual ICollection<LabelingPackagingMng_LabelingPackagingDetail_View> LabelingPackagingMng_LabelingPackagingDetail_View { get; set; }
        public virtual ICollection<LabelingPackagingMng_LabelingPackagingSparepartDetail_View> LabelingPackagingMng_LabelingPackagingSparepartDetail_View { get; set; }
        public virtual ICollection<LabelingPackagingMng_LabelingPackagingRemark_View> LabelingPackagingMng_LabelingPackagingRemark_View { get; set; }
        public virtual ICollection<LabelingPackagingMng_LabelingPackagingOtherFile_View> LabelingPackagingMng_LabelingPackagingOtherFile_View { get; set; }
    }
}
