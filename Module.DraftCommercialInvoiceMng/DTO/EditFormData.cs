using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftCommercialInvoiceMng.DTO
{
    public class EditFormData
    {
        public DTO.DraftCommercialInvoiceDTO Data { get; set; }
        public List<DTO.Season> Seasons { get; set; }
        public List<DTO.DeliveryTermDTO> DeliveryTerms { get; set; }
        public List<DTO.PaymentTermDTO> PaymentTerms { get; set; }
        public List<DTO.TurnOverLedgerAccountDTO> TurnOverLedgerAccounts { get; set; }

        public DTO.DraftCommercialInvoiceOverViewDTO DataOverView { get; set; }
    }
}
