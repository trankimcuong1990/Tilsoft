using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPLCMng.DTO
{
    public class EditFormData
    {
        public PLC Data { get; set; }
        public List<Module.Support.DTO.PLCImageType> PLCImageTypes { get; set; }
    }
}
