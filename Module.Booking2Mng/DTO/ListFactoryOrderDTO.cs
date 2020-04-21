using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Booking2Mng.DTO
{
    public class ListFactoryOrderDTO
    {
        public List<FactoryOrderDTO> FactoryOrderDTOs { get; set; }
        public List<FactoryOrderDetailDTO> FactoryOrderDetailDTOs { get; set; }
        public List<FactoryOrderSparepartDetailDTO> FactoryOrderSparepartDetailDTOs { get; set; }
        public List<FactoryOrderSampleDetailDTO> FactoryOrderSampleDetailDTOs { get; set; }
        public ListFactoryOrderDTO()
        {
            FactoryOrderDTOs = new List<FactoryOrderDTO>();
            FactoryOrderDetailDTOs = new List<FactoryOrderDetailDTO>();
            FactoryOrderSparepartDetailDTOs = new List<FactoryOrderSparepartDetailDTO>();
            FactoryOrderSampleDetailDTOs = new List<FactoryOrderSampleDetailDTO>();
        }
    }
    public class FactoryOrderDTO
    {
        public long KeyID { get; set; }
        public int? FactoryOrderID { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string LDS { get; set; }
        public string FactoryUD { get; set; }
        public int? OrderQnt { get; set; }
    }
    public class FactoryOrderDetailDTO
    {
        public long KeyID { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? FactoryOrderID { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string LDS { get; set; }
        public string FactoryUD { get; set; }
        public int? PlanQnt { get; set; }
        public int? ShippedQnt { get; set; }
    }
    public class FactoryOrderSparepartDetailDTO
    {
        public long KeyID { get; set; }
        public int? FactoryOrderSparepartDetailID { get; set; }
        public int? FactoryOrderID { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string LDS { get; set; }
        public string FactoryUD { get; set; }
        public int? PlanQnt { get; set; }
        public int? ShippedQnt { get; set; }
    }
    public class FactoryOrderSampleDetailDTO
    {
        public long KeyID { get; set; }
        public int? FactoryOrderSampleDetailID { get; set; }
        public int? FactoryOrderID { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string LDS { get; set; }
        public string FactoryUD { get; set; }
        public int? PlanQnt { get; set; }
        public int? ShippedQnt { get; set; }
    }
}
