using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng.DTO
{
    public class KeyRawMaterial
    {
        public int? KeyRawMaterialID { get; set;}
        public string KeyRawMaterialNM { get; set;}
        public int? AddedBy { get; set; }
        public string AddedDate { get; set; }
    }
}
