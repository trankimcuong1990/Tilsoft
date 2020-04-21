using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CatalogFileMng.DAL
{
    public partial class CatalogFileMngEntities
    {
        public CatalogFileMngEntities(string iConnectionString)
            : base(iConnectionString)
        { }
    }
}
