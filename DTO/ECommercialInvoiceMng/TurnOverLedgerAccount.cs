using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ECommercialInvoiceMng
{
    public class TurnOverLedgerAccount
    {
        [Display(Name = "TurnOverLedgerAccountID")]
        public int? TurnOverLedgerAccountID { get; set; }

        [Display(Name = "AccountNo")]
        public int? AccountNo { get; set; }

        [Display(Name = "AccountNM")]
        public string AccountNM { get; set; }

    }
}