using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.WarehouseCIMng
{
    public class WarehouseCI
    {
        [Display(Name = "WarehouseCIID")]
        public int? WarehouseCIID { get; set; }

        [Display(Name = "InvoiceNo")]
        public string InvoiceNo { get; set; }

        [Display(Name = "IssuedDate")]
        public DateTime? IssuedDate { get; set; }

        [Display(Name = "ClientID")]
        public int? ClientID { get; set; }

        [Display(Name = "OrderNo")]
        public string OrderNo { get; set; }

        [Display(Name = "RefNo")]
        public string RefNo { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Display(Name = "ExRate")]
        public decimal? ExRate { get; set; }

        [Display(Name = "LedgerAccountID")]
        public int? LedgerAccountID { get; set; }

        [Display(Name = "WarehouseID")]
        public int? WarehouseID { get; set; }

        [Display(Name = "VATRate")]
        public decimal? VATRate { get; set; }

        [Display(Name = "SubTotal")]
        public decimal? SubTotal { get; set; }

        [Display(Name = "TotalAmount")]
        public decimal? TotalAmount { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "ConcurrencyFlag")]
        public byte[] ConcurrencyFlag { get; set; }

        [Display(Name = "CreatedBy")]
        public int? CreatedBy { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "UpdatedBy")]
        public int? UpdatedBy { get; set; }

        [Display(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "ClientNM")]
        public string ClientNM { get; set; }

        [Display(Name = "CreatorName")]
        public string CreatorName { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        public string IssuedDateFormated { get; set; }

        public string CreatedDateFormated { get; set; }

        public string UpdatedDateFormated { get; set; }

        public string ConcurrencyFlag_String { get; set; }

        public List<WarehouseCIDetail> WarehouseCIDetails { get; set; }

        public List<WarehouseCIExtDetail> WarehouseCIExtDetails { get; set; }

    }
}