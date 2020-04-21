namespace Module.PurchasingCalendarMng.DTO
{
    public class PurchasingCalendarAppointmentSearchResultDTO
    {
        public int PurchasingCalendarAppointmentID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string PersonInChargeNM { get; set; }
        public string OwnerNM { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string RemindTime { get; set; }
        public int TotalAttachedFile { get; set; }       
        public int MeetingLocationID { get; set; }
        public int UserID { get; set; }
    }
}
