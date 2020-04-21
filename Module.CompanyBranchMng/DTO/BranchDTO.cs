using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CompanyBranchMng.DTO
{
    public class BranchDTO
    {
        public int BranchID { get; set; }
        public string BranchUD { get; set; }
        public string BranchNM { get; set; }
        public int? CompanyID { get; set; }
        public string StreetAddress { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int? FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
        public bool? IsDefault { get; set; }
        public string Remark { get; set; }
    }
}
