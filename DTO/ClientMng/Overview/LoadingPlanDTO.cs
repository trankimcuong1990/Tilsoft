using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview
{
    public class LoadingPlanDTO
    {
        public int LoadingPlanID { get; set; }
        public string Season { get; set; }
        public string LoadingPlanUD { get; set; }
        public string FactoryUD { get; set; }
        public string ShippedDate { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerTypeNM { get; set; }
        public string SealNo { get; set; }
        public string BLNo { get; set; }
        public string ETD { get; set; }
        public string ETA { get; set; }
        public string CutOffDate { get; set; }
    }
}
