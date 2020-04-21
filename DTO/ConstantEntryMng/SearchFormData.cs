using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ConstantEntryMng
{
    public class SearchFormData
    {
        public List<ConstantEntrySearchResults> Data { get; set; }
        public List<EntryGroupList> EntryGroups { get; set; }
    }
}
