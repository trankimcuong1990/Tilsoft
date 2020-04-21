namespace Module.ClientComplaint.DTO
{
    
    public class ClientComplaintUser
    {
        public int ClientComplaintUserID { get; set; }
        public int? ClientComplaintID { get; set; }
        public int? EmployeeID { get; set; }
        public string EmployeeNM { get; set; }
        public int? UserID { get; set; }
    }
}