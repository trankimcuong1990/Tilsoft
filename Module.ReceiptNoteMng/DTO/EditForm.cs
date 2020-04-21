using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceiptNoteMng.DTO
{
    public class EditForm
    {
        public DTO.ReceiptNoteEditResult Data { get; set; }

        public DTO.ReceiptNoteSupport supportData { get; set; }
    }
}
