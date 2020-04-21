using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.PLCMng
{
    public class InitFormData
    {
        public List<Support.Factory> Factories { get; set; }
        public List<PLCMng.ItemForCreatePLC> Items { get; set; }
    }
}
