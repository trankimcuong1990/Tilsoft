using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Framework.DTO
{
    public class EntityTracking
    {
        public int DataTrackingActionID { get; set; }
        public DAL.DataTrackingAction DataTrackingActionObj { get; set; }
        public DbEntityEntry ToBeTrackedEntity { get; set; }
    }
}
