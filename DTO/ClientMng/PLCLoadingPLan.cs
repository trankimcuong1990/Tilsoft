using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class PLCLoadingPLan
    {
        public object PrimaryID { get; set; }
        public int? PLCID { get; set; }
        public int? LoadingPlanID { get; set; }
        public string LoadingPlanUD { get; set; }
        public string ContainerNo { get; set; }
    }
}
