using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DefectsMng.DAL
{
    public partial class DefectsEntities
    {
        public DefectsEntities(string iConnectionString)
            : base(iConnectionString)
        { }
    }
}
