using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ClientMng
{
    public class ClientContractDetail
    {
        [Display(Name = "ClientContractDetailID")]
        public int? ClientContractDetailID { get; set; }

        [Display(Name = "ClientContractID")]
        public int? ClientContractID { get; set; }

        [Display(Name = "SaleOrderID")]
        public int? SaleOrderID { get; set; }

        [Display(Name = "ProformaInvoiceNo")]
        public string ProformaInvoiceNo { get; set; }

    }
}