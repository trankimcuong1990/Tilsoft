using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ModelMng
{
    public class ModelCheckListGroupDTO
    {
        public int ModelCheckListGroupID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> CheckListGroupID { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }

        public virtual Model Model { get; set; }
        public string CheckListGroupNM { get; set; }
        public string CheckListGroupUD { get; set; }
    }
}
