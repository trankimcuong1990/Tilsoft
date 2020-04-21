using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.PackingListMng
{
    public class PackingList
    {
        [Display(Name = "PackingListID")]
        public int? PackingListID { get; set; }

        [Display(Name = "PackingListUD")]
        public string PackingListUD { get; set; }

        [Display(Name = "PackingListDate")]
        public DateTime? PackingListDate { get; set; }

        [Display(Name = "PurchasingInvoiceID")]
        public int? PurchasingInvoiceID { get; set; }

        [Display(Name = "DescriptionOfGoods")]
        public string DescriptionOfGoods { get; set; }

        [Display(Name = "ContainerRemark")]
        public string ContainerRemark { get; set; }

        [Display(Name = "CreatedBy")]
        public int? CreatedBy { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "UpdatedBy")]
        public int? UpdatedBy { get; set; }

        [Display(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "ConcurrencyFlag")]
        public byte[] ConcurrencyFlag {get;set;}

        [Display(Name = "InvoiceNo")]
        public string InvoiceNo { get; set; }

        [Display(Name = "BLNo")]
        public string BLNo { get; set; }

        [Display(Name = "PODName")]
        public string PODName { get; set; }

        [Display(Name = "POLName")]
        public string POLName { get; set; }

        [Display(Name = "CreatorName")]
        public string CreatorName { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        public string PackingListDateFormated {get;set;}
        
        public string CreatedDateFormated {get;set;}

        public string UpdatedDateFormated {get;set;}

        public string ConcurrencyFlag_String { get; set; }

        public List<PackingListDetail> PackingListDetails { get; set; }

        public List<PackingListDetailExtend> PackingListDetailExtends { get; set; }

        public List<ECommercialInvoice> ECommercialInvoices { get; set; }

        public List<PackingListSparepartDetail> PackingListSparepartDetails { get; set; }

        public string SupplierUD { get; set; }

        public string SupplierNM { get; set; }

        

    }
}