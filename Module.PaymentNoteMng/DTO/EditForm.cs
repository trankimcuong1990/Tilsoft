using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PaymentNoteMng.DTO
{
    public class EditForm
    {
        public DTO.PaymentNoteEditResult Data { get; set; }
        public DTO.PaymentNoteSupport supportData { get; set; }
    }
}
