namespace DTO.ModelMng
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ModelIssueTrack
    {
        public int ModelIssueTrackID { get; set; }
        public string Description { get; set; }
        public DateTime? IssueDate { get; set; }
        public int StatusBy { get; set; }
        public string StatusName { get; set; }
        public int FollowUp { get; set; }
        public string EmployeeName { get; set; }
        public string FullName { get; set; }
        public int ModelIssueID { get; set; }
        public int ModelID { get; set; }
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public string Comment { get; set; }
        public string OtherContent { get; set; }

        public List<ModelIssueTrackImageError> ModelIssueTrackImagesError { get; set; }
        public List<ModelIssueTrackImageFinish> ModelIssueTrackImagesFinish { get; set; }

        public string IssueDateFormatted { get; set; }

        public string QualityReport { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public bool QualityReport_HasChange { get; set; }
        public string QualityReport_NewFile { get; set; }
    }

    public class ModelIssueTrackImageError
    {
        public int ModelIssueTrackImageErrorID { get; set; }
        public int ModelIssueTrackID { get; set; }
        public string ImageFile { get; set; }

        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public bool? ImageFile_HasChange { get; set; }
        public string ImageFile_NewFile { get; set; }
    }

    public class ModelIssueTrackImageFinish
    {
        public int ModelIssueTrackImageFinishID { get; set; }
        public int ModelIssueTrackID { get; set; }
        public string ImageFile { get; set; }

        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public bool? ImageFile_HasChange { get; set; }
        public string ImageFile_NewFile { get; set; }
    }
}
