namespace Module.BifaCompanyMng.DTO
{
    public class BifaEventFile
    {
        public int BifaEventFileID { get; set; }
        public int? BifaEventID { get; set; }
        public string FileUD { get; set; }
        public string Description { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string EmployeeNM { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public bool HasChange { get; set; }
        public string NewFile { get; set; }
    }
}
