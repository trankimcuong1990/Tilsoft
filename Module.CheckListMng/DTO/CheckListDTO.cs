using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CheckListMng.DTO
{
    public class CheckListDTO
    {
        public int CheckListID { get; set; }
        public string CheckListUD { get; set; }
        public string CheckListNM { get; set; }
        public Nullable<int> CheckListGroupID { get; set; }
        public Nullable<int> TypeOfInspecID { get; set; }
        public string CheckListGroupNM { get; set; }
        public string TypeOfInspecNM { get; set; }
        public string CheckListName { get; set; }
        public string TypeName { get; set; }
        public string CheckListNMVi { get; set; }

    }
}
