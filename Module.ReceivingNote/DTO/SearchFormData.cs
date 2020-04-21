using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceivingNote.DTO
{
    public class SearchFormData
    {
        public List<ReceivingNoteSearchDTO> Data { get; set; }
        public List<StatusTypeDTO> StatusTypes { get; set; }
    }
}
