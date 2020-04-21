using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductWizardMng.DAL
{
    public partial class ProductWizardMngEntities
    {
        public ProductWizardMngEntities(string iConnectionString)
           : base(iConnectionString)
        { }
    }
}
