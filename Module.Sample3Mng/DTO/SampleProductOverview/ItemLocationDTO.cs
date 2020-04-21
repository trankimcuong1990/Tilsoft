using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample3Mng.DTO.SampleProductOverview
{
    public class ItemLocationDTO
    {
        public int SampleProductItemID { get; set; }
        public string SampleProductItemUD { get; set; }
        public string CurrentLocation { get; set; }
        public string LocationDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string EmployeeNM { get; set; }
        public string UpdatedDate { get; set; }
    }
}
