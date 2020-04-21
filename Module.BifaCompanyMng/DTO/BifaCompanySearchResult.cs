namespace Module.BifaCompanyMng.DTO
{
    public class BifaCompanySearchResult
    {
        public int BifaCompanyID { get; set; }
        public int? BifaIndustryID { get; set; }
        public int? BifaCityID { get; set; }
        public int? NumberOfEmployee { get; set; }
        public int? UpdatedBy { get; set; }
        public string BifaCompanyUD { get; set; }
        public string BifaCompanyNM { get; set; }
        public string Logo { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public string TaxCode { get; set; }
        public string FoundingDate { get; set; }
        public string BifaIndustryNM { get; set; }
        public string BifaCityNM { get; set; }
        public string JoinedDate { get; set; }
        public string Remark { get; set; }
        public string UpdatedDate { get; set; }
        public string EmployeeNM { get; set; }
        public decimal? Acreage { get; set; }
        public bool? IsActive { get; set; }
    }
}
