namespace Module.NotificationMng.DTO
{
    public class NotificationGroupStatusDTO
    {
        public int NotificationGroupStatusID { get; set; }
        public int? NotificationGroupID { get; set; }
        public int? ModuleStatusID { get; set; }
        public bool? IsSelected { get; set; }
        public string StatusNM { get; set; }
        public int? ModuleID { get; set; }
    }
}
