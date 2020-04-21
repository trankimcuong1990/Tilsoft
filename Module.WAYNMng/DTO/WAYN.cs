using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.WAYNMng.DTO
{
    public class WAYN
    {
        public int? WAYNID { get; set; }
        public string WorkingDate { get; set; }
		public string UpdatorName{get;set;}
		public string UpdatedDate{get;set;}

        //
        // calendar attribute
        //
        public string start_string { get; set; }
        public string end_string { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public List<EmployeeList> EmployeeLists { get; set; }
    }
}
