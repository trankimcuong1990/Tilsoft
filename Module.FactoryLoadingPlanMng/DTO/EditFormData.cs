using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryLoadingPlanMng.DTO
{
    public class EditFormData
    {
        public DTO.LoadingPlan Data { get; set; }
        public List<Module.Support.DTO.ContainerType> ContainerTypes { get; set; }
    }
}
