using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.PLCMng
{
    public class EditFormData
    {
        public PLC Data { get; set; }
        public List<Support.PLCImageType> PLCImageTypes { get; set; }
    }
}
