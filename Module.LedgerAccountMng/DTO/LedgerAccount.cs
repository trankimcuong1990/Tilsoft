using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.LedgerAccountMng.DTO
{
    public class LedgerAccount
    {
        public int? LedgerAccountID { get; set; }
        public string AccountNo { get; set; }
        public string VietnameseNM { get; set; }
        public string EnglishNM { get; set; }
        public int? ParentID { get; set; }
        public decimal? OpeningDebit { get; set; }
        public decimal? OpeningCredit { get; set; }
        public decimal? ClosingDebit { get; set; }
        public decimal? ClosingCredit { get; set; }
        public decimal? TotalDebitAmount { get; set; }
        public decimal? TotalCreditAmount { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public List<LedgerAccountDetailOverview> LedgerAccountDetailOverviews { get; set; }
        //
        // concurrency
        //
        public string ConcurrencyFlag_String { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatorName2 { get; set; }
    }
}
