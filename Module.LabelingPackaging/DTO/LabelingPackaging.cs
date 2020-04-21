using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.LabelingPackaging.DTO
{
    public class LabelingPackaging
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

        // AIO HangTag
        public string AIOHangTagFile { get; set; }
        public string AIOHangTagFile_FileUrl { get; set; }
        public string AIOHangTagFile_FriendlyName { get; set; }
        public bool AIOHangTagFileHasChange { get; set; }
        public string NewAIOHangTagFile { get; set; }

        // AIO BoxShippingMark
        public string AIOBoxShippingMarkFile { get; set; }
        public string AIOBoxShippingMarkFile_FileUrl { get; set; }
        public string AIOBoxShippingMarkFile_FriendlyName { get; set; }
        public bool AIOBoxShippingMarkFileHasChange { get; set; }
        public string NewAIOBoxShippingMarkFile { get; set; }

        // AIO Brasslabel
        public string AIOBrassLabelFile { get; set; }
        public string AIOBrassLabelFile_FileUrl { get; set; }
        public string AIOBrassLabelFile_FriendlyName { get; set; }
        public bool AIOBrassLabelFileHasChange { get; set; }
        public string NewAIOBrassLabelFile { get; set; }

        // AIO AI
        public string AIOAIFile { get; set; }
        public string AIOAIFile_FileUrl { get; set; }
        public string AIOAIFile_FriendlyName { get; set; }
        public bool AIOAIFileHasChange { get; set; }
        public string NewAIOAIFile { get; set; }

        // AIO WashCushion
        public string AIOWashCushionLabelFile { get; set; }
        public string AIOWashCushionLabelFile_FileUrl { get; set; }
        public string AIOWashCushionLabelFile_FriendlyName { get; set; }
        public bool AIOWashCushionLabelFileHasChange { get; set; }
        public string NewAIOWashCushionLabelFile { get; set; }

        // FSC Label
        public string AIOFSCLabelFile { get; set; }
        public string AIOFSCLabelFile_FileUrl { get; set; }
        public string AIOFSCLabelFile_FriendlyName { get; set; }
        public bool AIOFSCLabelFileHasChange { get; set; }
        public string NewAIOFSCLabelFile { get; set; }

        //option 1
        public object Option1StatusID { get; set; }
        public string AIOOption1File { get; set; }
        public string Option1Label { get; set; }
        public string AIOOption1File_FileUrl { get; set; }
        public string AIOOption1File_FriendlyName { get; set; }
        public bool AIOOption1FileHasChange { get; set; }
        public string NewAIOOption1File { get; set; }

        //option 2
        public object Option2StatusID { get; set; }
        public string AIOOption2File { get; set; }
        public string Option2Label { get; set; }
        public string AIOOption2File_FileUrl { get; set; }
        public string AIOOption2File_FriendlyName { get; set; }
        public bool AIOOption2FileHasChange { get; set; }
        public string NewAIOOption2File { get; set; }

        //option 3
        public object Option3StatusID { get; set; }
        public string AIOOption3File { get; set; }
        public string Option3Label { get; set; }
        public string AIOOption3File_FileUrl { get; set; }
        public string AIOOption3File_FriendlyName { get; set; }
        public bool AIOOption3FileHasChange { get; set; }
        public string NewAIOOption3File { get; set; }

        //option 4
        public object Option4StatusID { get; set; }
        public string Option4Label { get; set; }

        //option 5
        public object Option5StatusID { get; set; }
        public string Option5Label { get; set; }

        //option 6
        public object Option6StatusID { get; set; }
        public string Option6Label { get; set; }

        //option 7
        public object Option7StatusID { get; set; }
        public string Option7Label { get; set; }

        //option 8
        public object Option8StatusID { get; set; }
        public string Option8Label { get; set; }

        //option 9
        public object Option9StatusID { get; set; }
        public string Option9Label { get; set; }

        //option 10
        public object Option10StatusID { get; set; }
        public string Option10Label { get; set; }

        //option 11
        public object Option11StatusID { get; set; }
        public string Option11Label { get; set; }

        //option 12
        public object Option12StatusID { get; set; }
        public string Option12Label { get; set; }

        //option 13
        public object Option13StatusID { get; set; }
        public string Option13Label { get; set; }

        //option 14
        public object Option14StatusID { get; set; }
        public string Option14Label { get; set; }

        //option 15
        public object Option15StatusID { get; set; }
        public string Option15Label { get; set; }

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

        public bool? IsAutoUpdateSimilarLP { get; set; }

        public string LDS { get; set; }
        public string ProductionStatus { get; set; }
        public bool? IsSaveConfig { get; set; }

        public List<LabelingPackagingDetail> LabelingPackagingDetails { get; set; }
        public List<LabelingPackagingSparepartDetail> LabelingPackagingSparepartDetails { get; set; }
        public List<LabelingPackagingRemark> LabelingPackagingRemarks { get; set; }
        public List<LabelingPackagingOtherFile> LabelingPackagingOtherFiles { get; set; }
        public string DirectLink { get; set; }

        public List<LabelingPackagingRejection> LabelingPackagingRejections { get; set; }
        public List<LabelingPackagingFileMonitor> LabelingPackagingFileMonitors { get; set; }
    }
}
