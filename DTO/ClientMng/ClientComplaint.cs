using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class ClientComplaint
    {
        public int? ClientComplaintID { get; set; }
        public string ReceivedDate { get; set; }
        public string ComplaintNumber { get; set; }
        public string YearSeason { get; set; }
        public string ContactPerson { get; set; }
        public int? ComplaintType { get; set; }
        public int? ComplaintStatus { get; set; }
        public bool? ComplaintAccepted { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApproverName { get; set; }
        public string ApprovedDate { get; set; }
        public bool? CreditNoteNeeded { get; set; }
        public bool? SparepartNeeded { get; set; }
        public bool? ReplacementNeeded { get; set; }
        public string Reason { get; set; }
        public string Action { get; set; }
        public string ActionFollowUpBy { get; set; }
        public string CreatedPINumberSolution { get; set; }
        public string EANCodeCreated { get; set; }
        public string StatusOrderNetherlands { get; set; }
        public string DeliveryDateClient { get; set; }
        public string SaleNM { get; set; }
        public string Sale2NM { get; set; }
        public string SaleVnNM { get; set; }
        public int? SaleID { get; set; }
        public int? Sale2ID { get; set; }
        public int? SaleVnID { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public string Article { get; set; }
        public string ComplaintDescription { get; set; }
        public int? ClientID { get; set; }
    }
}
