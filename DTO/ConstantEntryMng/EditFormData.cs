using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ConstantEntryMng
{
    public class EditFormData
    {
        public DTO.ConstantEntryMng.ConstantEntry Data { get; set; }
        public List< DTO.ConstantEntryMng.EntryGroupList> EntryGroups { get; set; }

    }
}
