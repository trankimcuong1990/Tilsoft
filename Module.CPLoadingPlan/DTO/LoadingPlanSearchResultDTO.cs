using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CPLoadingPlan.DTO
{
    public class LoadingPlanSearchResultDTO
    {
        public int LoadingPlanID { get; set; }
        public string BLNo { get; set; }
        public string ETD { get; set; }
        public string ETA { get; set; }
        public string LoadingPlanUD { get; set; }
        public string ContainerTypeNM { get; set; }
        public string ContainerNo { get; set; }
        public string SealNo { get; set; }
        public bool? IsLoaded { get; set; }
        public string LoadingDate { get; set; }
        public bool? IsShipped { get; set; }
        public string ShippedDate { get; set; }
        public string ShipmentDate { get; set; }
    }
}
