using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ConstantEntryMng
{
    public class ConstantEntrySearchResults
    {
         public int ConstantEntryID { get; set; }

         public int EntryValue { get; set; }
         public string DisplayText { get; set;}
        public string  EntryGroup { get; set; }


        public int? DisplayOrder { get; set; }

        //public int ConstantEntryID { get; set; }
        //public string DisplayText { get; set; }

        //public string EntryGroup { get; set; }

        public List<DTO.ConstantEntryMng.EntryGroupList > EntryGroups { get; set; }
       
    }
}
