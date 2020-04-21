using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MaterialMng
{
    public partial class MaterialMngEntities
    {
        public MaterialMngEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}
