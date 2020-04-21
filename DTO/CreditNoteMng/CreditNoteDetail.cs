using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.CreditNoteMng
{
    public class CreditNoteDetail
    {
        public int? CreditNoteDetailID { get; set; }

        public int? CreditNoteID { get; set; }

        public string Description { get; set; }

        public decimal? Amount { get; set; }

    }
}