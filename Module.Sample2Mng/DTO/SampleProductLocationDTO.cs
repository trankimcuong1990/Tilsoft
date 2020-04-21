using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample2Mng.DTO
{
    public class SampleProductLocationDTO
    {
        public int SampleProductItemID { get; set; }
        public string SampleProductItemUD { get; set; }
        public string Remark { get; set; }
        public string CreatedDate { get; set; }
        public string CurrentLocation { get; set; }
        public string LocationDate { get; set; }
        public string EmployeeNM { get; set; }
        public string UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
