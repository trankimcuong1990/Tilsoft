using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class OfferStatusDTO
    {
        public List<DTO.AdminDashboardOfferSeasonNotApprovedYetDTO> Data { get; set; }
        public decimal? DeltaApproved { get; set; }
        public decimal? DeltaNotApproved { get; set; }
    }
}
