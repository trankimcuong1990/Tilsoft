using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.WAYNMng.DTO
{
    public class EmployeeList
    {
        public int? WAYNID { get; set; }
        public int? EmployeeID { get; set; }
        public string EmployeeUD { get; set; }
        public string EmployeeNM { get; set; }
        public string WorkingDate { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public bool? IsOutOfOffice { get; set; }
        public string Description { get; set; }
    }
}
