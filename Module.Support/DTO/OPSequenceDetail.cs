using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class OPSequenceDetail
    {
        public int OPSequenceDetailID { get; set; }
        public string OPSequenceDetailNM { get; set; }
        public int? SequenceIndexNumber { get; set; }
        public int? OPSequenceID { get; set; }
        public string OPSequenceNM { get; set; }
        public int? WorkCenterID { get; set; }
        public decimal? OperatingTime { get; set; }
        public decimal? DefaultCost { get; set; }
        public int? NextWorkCenterID { get; set; }
        public int? DefaultFactoryWarehouseID { get; set; }
        public string DefaultFactoryWarehouseNM { get; set; }
        public string WorkCenterNM { get; set; }
        public bool? IsExceptionAtConfirmFrameStatus { get; set; }

    }
}
