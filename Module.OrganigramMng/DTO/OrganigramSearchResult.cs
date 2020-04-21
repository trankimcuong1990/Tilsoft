using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OrganigramMng.DTO
{
    public class OrganigramSearchResult
    {
	    public int? CompanyID {get;set;}
	    public string InternalCompanyNM {get;set;}
	    public int? TotalEmp {get;set;}
        public List<CompanyDirector> CompanyDirectors { get; set; }
        public OrganizationChart OrganizationChart { get; set; }
    }
}
