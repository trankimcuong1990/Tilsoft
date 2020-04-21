using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CompanyBranchMng.DTO
{
    public class BranchQuickSearchResultDTO
    {
        public int ID { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }

        public string StreetAddress { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int? FactoryID { get; set; }
    }
}
