using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.LabelingPackaging.DTO
{
    class LabelingPackagingTemp
    {
        public int? LabelingPackagingID { get; set; }
        public int? FactoryOrderID { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }

        // Approve info
        public int? ApprovedBy { get; set; }
        public string NameApproval { get; set; }
        public string ApprovedDate { get; set; }

        public object VNRep { get; set; }
        public int? LPStatusID { get; set; }
        public string LPStatusNM { get; set; }
        public object HangTagStatusID { get; set; }
        public object BoxShippingMarkStatusID { get; set; }
        public object BrassLabelStatusID { get; set; }
        public object AIStatusID { get; set; }
        public object WashCushionLabelID { get; set; }
        public object FSCLabelStatusID { get; set; }

        //Other file
        public string OtherFile { get; set; }
        public string OtherFileFileUrl { get; set; }
        public string OtherFileFriendlyName { get; set; }
        public bool OtherFileHasChange { get; set; }
        public string NewOtherFile { get; set; }

        //// AIO HangTag
        //public string AIOHangTagFile { get; set; }
        //public string AIOHangTagFile_FileUrl { get; set; }
        //public string AIOHangTagFile_FriendlyName { get; set; }
        //public bool AIOHangTagFileHasChange { get; set; }
        //public string NewAIOHangTagFile { get; set; }

        //// AIO BoxShippingMark
        //public string AIOBoxShippingMarkFile { get; set; }
        //public string AIOBoxShippingMarkFile_FileUrl { get; set; }
        //public string AIOBoxShippingMarkFile_FriendlyName { get; set; }
        //public bool AIOBoxShippingMarkFileHasChange { get; set; }
        //public string NewAIOBoxShippingMarkFile { get; set; }

        //// AIO Brasslabel
        //public string AIOBrassLabelFile { get; set; }
        //public string AIOBrassLabelFile_FileUrl { get; set; }
        //public string AIOBrassLabelFile_FriendlyName { get; set; }
        //public bool AIOBrassLabelFileHasChange { get; set; }
        //public string NewAIOBrassLabelFile { get; set; }

        //// AIO AI
        //public string AIOAIFile { get; set; }
        //public string AIOAIFile_FileUrl { get; set; }
        //public string AIOAIFile_FriendlyName { get; set; }
        //public bool AIOAIFileHasChange { get; set; }
        //public string NewAIOAIFile { get; set; }

        //// AIO WashCushion
        //public string AIOWashCushionLabelFile { get; set; }
        //public string AIOWashCushionLabelFile_FileUrl { get; set; }
        //public string AIOWashCushionLabelFile_FriendlyName { get; set; }
        //public bool AIOWashCushionLabelFileHasChange { get; set; }
        //public string NewAIOWashCushionLabelFile { get; set; }

        // FSC Label
        public string AIOFSCLabelFile { get; set; }
        public string AIOFSCLabelFile_FileUrl { get; set; }
        public string AIOFSCLabelFile_FriendlyName { get; set; }
        public bool AIOFSCLabelFileHasChange { get; set; }
        public string NewAIOFSCLabelFile { get; set; }

        // Client info
        public string ClientUD { get; set; }

        public int? SaleOrderID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientOrderNumber { get; set; }
        public int? FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string SaleNM { get; set; }

        // Rejected Comment
        public string RejectComment { get; set; }

        //Rejected comment file
        public string RejectCommentFile { get; set; }
        public string RejectCommentFile_FileUrl { get; set; }
        public string RejectCommentFile_FriendlyName { get; set; }
        public bool RejectCommentFileHasChange { get; set; }
        public string NewRejectCommentFile { get; set; }

        public List<LabelingPackagingDetail> LabelingPackagingDetails { get; set; }
        public List<LabelingPackagingSparepartDetail> LabelingPackagingSparepartDetails { get; set; }
        public List<LabelingPackagingRemark> LabelingPackagingRemarks { get; set; }
        public List<LabelingPackagingOtherFile> LabelingPackagingOtherFiles { get; set; }
        public string DirectLink { get; set; }

        public List<LabelingPackagingRejection> LabelingPackagingRejections { get; set; }
    }
}
