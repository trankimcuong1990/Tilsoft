using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DevRequestMng.DTO
{
    public class DevRequestSearchResult
    {
        public int DevRequestID { get; set; }
        public string DevRequestTypeNM { get; set; }
        public string DevRequestPriorityNM { get; set; }
        public string DevRequestProjectNM { get; set; }
        public string Title { get; set; }
        public string RequesterName { get; set; }
        public string InternalCompanyNM { get; set; }
        public string DevRequestStatusNM { get; set; }
        public string StartDate { get; set; }
        public string EstEndDate { get; set; }
        public int? TotalHour { get; set; }
        public int? TotalAction { get; set; }
        public string LastActionDescription { get; set; }
        public string LastActionUpdatorName { get; set; }
        public string LastActionUpdatedDate { get; set; }
        public int? DevRequestStatusID { get; set; }
        public int? DevRequestProjectID { get; set; }
        public int? DevRequestTypeID { get; set; }
        public int? RequesterID { get; set; }
        public int? Priority { get; set; }

        public List<DTO.DevRequestAssignment> DevRequestAssignments { get; set; }
    }
}
