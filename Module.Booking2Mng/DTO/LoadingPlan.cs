using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Booking2Mng.DTO
{
    public class LoadingPlan
    {
        public int LoadingPlanID { get; set; }
        public string LoadingPlanUD { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerTypeNM { get; set; }
        public string SealNo { get; set; }
        public bool IsShipped { get; set; }
        public bool IsLoaded { get; set; }
        public bool IsConfirmed { get; set; }
        public int IsUploadingImageFinish { get; set; }
    }
}
