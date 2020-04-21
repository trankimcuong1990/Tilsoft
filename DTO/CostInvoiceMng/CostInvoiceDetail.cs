using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.CostInvoiceMng
{
    public class CostInvoiceDetail
    {
        public int? CostInvoiceDetailID { get; set; }

        public int? CostInvoiceID { get; set; }

        public int? LoadingPlanID { get; set; }

        public string ContainerNo { get; set; }

        public string Description { get; set; }

        public decimal? Amount { get; set; }
        public List<CostInvoiceDetailExtend> CostInvoiceDetailExtends { get; set; }

        public string CostType { get; set; }

        public int? ClientID { get; set; }

    }
}