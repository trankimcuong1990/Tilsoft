using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseRequestMng.DTO
{
    public class EditFormData
    {
        public PurchaseRequestDTO Data { get; set; }        
        public List<PurchaseQuotationDTO> PurchaseQuotationDTOs { get; set; }
        public List<PurchaseQuotationDetailDTO> PurchaseQuotationDetailDTOs { get; set; }
        public List<PurchaseRequestDetailApprovalDTO> PurchaseRequestDetailApprovalDTOs { get; set; }
        public List<PurchaseRequestStatusDTO> PurchaseRequestStatusDTOs { get; set; }
        public List<DTO.ClientByWorkOrderDTO> ClientByWorkOrders { get; set; }
        public List<DTO.ProformaInvoiceNoByWorkOrderDTO> ProformaInvoiceByWorkOrders { get; set; }
    }
}
