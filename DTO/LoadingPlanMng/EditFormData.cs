using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.LoadingPlanMng
{
    public class EditFormData
    {
        public LoadingPlan Data { get; set; }
        public List<DTO.Support.ContainerType> ContainerTypes { get; set; }
        public List<DTO.LoadingPlanMng.User2> Users { get; set; }
    }
}
