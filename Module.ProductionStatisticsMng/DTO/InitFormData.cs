using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionStatisticsMng.DTO
{
    public class InitFormData
    {
        public List<WorkCenterDTO> WorkCenterDTOs { get; set; }
    } 

    public class WorkCenterDTO
    {
        public int WorkCenterID { get; set; }
        public string WorkCenterNM { get; set; }
    }
}
