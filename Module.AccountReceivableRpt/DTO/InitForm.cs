using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AccountReceivableRpt.DTO
{
    public class InitForm
    {
        public List<KeyRawMaterial> keyRawMaterials { get; set; }
        public List<DueColorDTO> dueColorDTOs { get; set; }
        public List<SupplierDTO> SupplierDTO { get; set; }
        public string CurrentDate { get; set; }
    }
    public class KeyRawMaterial
    {
        public int KeyRawMaterialID { get; set; }
        public string keyRawMaterialNM { get; set; }
    }
}
