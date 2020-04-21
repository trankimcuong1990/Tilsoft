using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AdditionalConditionMng.DTO
{
    public class AdditionalConditionSearch
    {
        public int KeyID { get; set; }
        public int AdditionalConditionID { get; set; }
        public int TypeAC { get; set; }
        public string AdditionalConditionNM { get; set; }
        public string Remark { get; set; }
        public int UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateName { get; set; }
        public string AdditionalConditionTypeNM { get; set; }
        public bool? ActiveFactory { get; set; }
        public bool? WhoToPay { get; set; }
    }
}
