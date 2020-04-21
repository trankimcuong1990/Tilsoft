using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ModelMng
{
    public class EditFormData2
    {
        public DTO.ModelMng.ModelIssueTrack Data { get; set; }
        public List<Support.User> SupportUsers { get; set; }
        public List<QCTrackingStatus> SupportQCTrackingStatus { get; set; }
        public List<QCTrackingType> SupportQCTrackingType { get; set; }
    }

    public class QCTrackingStatus
    {
        public int TrackingStatusID { get; set; }
        public string TrackingStatusUD { get; set; }
        public string TrackingStatusNM { get; set; }
    }

    public class QCTrackingType
    {
        public int TrackingTypeID { get; set; }
        public string TrackingTypeUD { get; set; }
        public string TrackingTypeNM { get; set; }
    }
}
