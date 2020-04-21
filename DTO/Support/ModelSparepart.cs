using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Support
{
    public class ModelSparepart
    {
        public int ModelSparepartID { get; set; }
        public int ModelID { get; set; }
        public string PartUD { get; set; }
        public string PartNM { get; set; }
    }
}
