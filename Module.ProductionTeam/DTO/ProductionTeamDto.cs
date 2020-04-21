namespace Module.ProductionTeam.DTO
{
    public class ProductionTeamDto
    {
        public int ProductionTeamID { get; set; }
        public string ProductionTeamUD { get; set; }
        public string ProductionTeamNM { get; set; }
        public string Description { get; set; }
        public int? CompanyID { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public bool? HasUpdateHyperlink { get; set; }
        public string InternalCompanyNM { get; set; }
        public decimal? OperatingTime { get; set; }
        public decimal? DefaultCost { get; set; }
        public decimal? Capacity { get; set; }
        public int? ResponsibleBy { get; set; }
        public string ResponsibleName { get; set; }
        public bool? HasResponsibleHyperlink { get; set; }
        public int? WorkCenterID { get; set; }
        public bool? IsDefault { get; set; }
        public bool? IsOutsource { get; set; }
        public int? SubSupplier { get; set; }
        public decimal? DistanceToFactory { get; set; }
    }
}
