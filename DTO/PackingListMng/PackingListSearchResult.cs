using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.PackingListMng
{
    public class PackingListSearchResult
    {
        [Display(Name = "PackingListID")]
        public int? PackingListID { get; set; }

        [Display(Name = "PackingListUD")]
        public string PackingListUD { get; set; }

        [Display(Name = "PackingListDate")]
        public DateTime? PackingListDate { get; set; }

        [Display(Name = "DescriptionOfGoods")]
        public string DescriptionOfGoods { get; set; }

        [Display(Name = "ContainerRemark")]
        public string ContainerRemark { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

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

        [Display(Name = "CreatorID")]
        public int CreatorID { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "UpdatorID")]
        public int UpdatorID { get; set; }

        public string PackingListDateFormated
        {
            get
            {
                if (PackingListDate.HasValue)
                    return PackingListDate.Value.ToString("dd/MM/yyyy");
                else
                    return "";
            }
        }

        public string CreatedDateFormated
        {
            get
            {
                if (CreatedDate.HasValue)
                    return CreatedDate.Value.ToString("dd/MM/yyyy");
                else
                    return "";
            }
        }

        public string UpdatedDateFormated
        {
            get
            {
                if (UpdatedDate.HasValue)
                    return UpdatedDate.Value.ToString("dd/MM/yyyy");
                else
                    return "";
            }
        }

        public string SupplierUD { get; set; }

        public string SupplierNM { get; set; }


        public List<PackingListSearchContainerNo> PackingListSearchContainerNos { get; set; }

    }
}