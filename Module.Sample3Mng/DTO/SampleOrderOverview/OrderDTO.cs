using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample3Mng.DTO.SampleOrderOverview
{
    public class OrderDTO
    {
        public int SampleOrderID { get; set; }
        public string SampleOrderUD { get; set; }
        public string Season { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string Deadline { get; set; }
        public string HardDeadline { get; set; }
        public string SampleOrderStatusNM { get; set; }
        public string PurposeNM { get; set; }
        public string TransportTypeNM { get; set; }
        public string Destination { get; set; }
        public string Remark { get; set; }
        public int CreatedBy { get; set; }
        public string CreatorName { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatorName { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public List<ProductDTO> ProductDTOs { get; set; }
        public List<MonitorDTO> MonitorDTOs { get; set; }
    }
}
