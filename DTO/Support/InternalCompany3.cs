using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Support
{
    public class InternalCompany3
    {
        /*
	        - only get eurofar & orange pie company
	        - use for module eurofar commercial invoice
        */
        public int ConstantEntryID { get; set; }
        public int InternalCompanyID { get; set; }
        public string InternalCompanyNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
