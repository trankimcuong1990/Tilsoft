using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelMng
{
    public partial class ModelMngEntities
    {
        public ModelMngEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}
