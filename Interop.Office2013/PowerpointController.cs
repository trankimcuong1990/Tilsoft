using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Interop.Office2013
{
    public class PowerpointController
    {
        public static readonly object Powerpoint2013LockObject = new object();

        public string ExportOffer(DataSet data)
        {
            throw new NotImplementedException();

            //lock (PowerpointController.Powerpoint2013LockObject)
            //{

            //}
        }
    }
}
