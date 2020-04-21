using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkCenterMng.DTO
{
    public class WorkCenterDto
    {
        public int WorkCenterID { get; set; }
        public string WorkCenterUD { get; set; }
        public string WorkCenterNM { get; set; }
        public decimal? OperatingTime { get; set; }
        public decimal? DefaultCost { get; set; }
        public decimal? Capacity { get; set; }
        public int? ResponsibleBy { get; set; }
        public string EmployeeNM { get; set; }
        public int RowNumber { get; set; }
        public int? DefaultFactoryWarehouseID { get; set; }
        public string DefaultFactoryWarehouseNM { get; set; }
        public int? PlanningTime { get; set; }
        public int? WorkingTime { get; set; }
        public bool IsVirtual { get; set; }

        public List<WorkCenterDetailDTO> WorkCenterDetails { get; set; }

        public WorkCenterDto()
        {
            WorkCenterDetails = new List<WorkCenterDetailDTO>();
        }
    }
}