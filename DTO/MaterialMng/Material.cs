using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DTO.MaterialMng
{
    public class Material
    {
        public int MaterialID { get; set; }
        public string MaterialUD { get; set; }        
        public string MaterialNM { get; set; }
        public int? Total { get; set; }
    }
}
