using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PaymentNoteMng.DTO
{
    public class SearchForm
    {
        public SearchForm()
        {
            Data = new List<DTO.PaymentNoteSearchResult>();
            paymentSupportStatuses = new List<DTO.PaymentSupportStatus>();
            paymentSupportTypes = new List<PaymentSupportType>();
        }
        public List<DTO.PaymentNoteSearchResult> Data { get; set; }
        public List<DTO.PaymentSupportStatus> paymentSupportStatuses { get; set; }
        public List<DTO.PaymentSupportType> paymentSupportTypes { get; set; }
    }
}
