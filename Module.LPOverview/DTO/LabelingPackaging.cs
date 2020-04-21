using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.LPOverview.DTO
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

        // AIO HangTag
        public string AIOHangTagFile { get; set; }
        public string AIOHangTagFile_FileUrl { get; set; }
        public string AIOHangTagFile_FriendlyName { get; set; }

        // AIO BoxShippingMark
        public string AIOBoxShippingMarkFile { get; set; }
        public string AIOBoxShippingMarkFile_FileUrl { get; set; }
        public string AIOBoxShippingMarkFile_FriendlyName { get; set; }

        // AIO Brasslabel
        public string AIOBrassLabelFile { get; set; }
        public string AIOBrassLabelFile_FileUrl { get; set; }
        public string AIOBrassLabelFile_FriendlyName { get; set; }

        // AIO AI
        public string AIOAIFile { get; set; }
        public string AIOAIFile_FileUrl { get; set; }
        public string AIOAIFile_FriendlyName { get; set; }

        // AIO WashCushion
        public string AIOWashCushionLabelFile { get; set; }
        public string AIOWashCushionLabelFile_FileUrl { get; set; }
        public string AIOWashCushionLabelFile_FriendlyName { get; set; }

        //Option 1
        public object Option1StatusID { get; set; }
        public string AIOOption1File { get; set; }
        public string Option1Label { get; set; }
        public string AIOOption1File_FileUrl { get; set; }
        public string AIOOption1File_FriendlyName { get; set; }

        //Option 2
        public object Option2StatusID { get; set; }
        public string AIOOption2File { get; set; }
        public string Option2Label { get; set; }
        public string AIOOption2File_FileUrl { get; set; }
        public string AIOOption2File_FriendlyName { get; set; }

        //Option 3
        public object Option3StatusID { get; set; }
        public string AIOOption3File { get; set; }
        public string Option3Label { get; set; }
        public string AIOOption3File_FileUrl { get; set; }
        public string AIOOption3File_FriendlyName { get; set; }

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

        public string ProformaInvoiceNo { get; set; }
        public string ClientOrderNumber { get; set; }
        public string FactoryUD { get; set; }
        public string SaleNM { get; set; }
        public string LPStatusNM { get; set; }
        public List<LabelingPackagingDetail> LabelingPackagingDetails { get; set; }
        public List<LabelingPackagingSparepartDetail> LabelingPackagingSparepartDetails { get; set; }
        public List<LabelingPackagingRemark> LabelingPackagingRemarks { get; set; }
        public List<LabelingPackagingOtherFile> LabelingPackagingOtherFiles { get; set; }
    }
}
