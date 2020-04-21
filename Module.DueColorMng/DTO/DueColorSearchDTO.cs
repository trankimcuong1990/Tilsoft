using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DueColorMng.DTO
{
    public class DueColorSearchDTO
    {
        public int DueColorID { get; set; }
        public string DueColorUD { get; set; }
        public string DueColorNM { get; set; }
        public Nullable<int> FromDue { get; set; }
        public Nullable<int> ToDue { get; set; }
    }
}
