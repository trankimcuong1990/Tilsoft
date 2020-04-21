namespace Module.Support.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProductionTeamDto
    {
        public int ProductionTeamID { get; set; }
        public string ProductionTeamUD { get; set; }
        public string ProductionTeamNM { get; set; }
        public int? CompanyID { get; set; }
    }
}
