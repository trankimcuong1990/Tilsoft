using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ModelMng
{
    public class PackagingMethodOption
    {
        public int ModelPackagingMethodOptionID { get; set; }
        public int PackagingMethodID { get; set; }
        public bool? IsDefault { get; set; }
        public string Description { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool IsConfirmed { get; set; }
        public int ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public string UpdatorName { get; set; }
        public string ConfirmerName { get; set; }
        public string PackagingMethodUD { get; set; }
        public string PackagingMethodNM { get; set; }
        public string PackingRemark { get; set; }
        public int LoadAbilityTypeID { get; set; }

        public string CartonBoxDimL { get; set; }
        public string CartonBoxDimW { get; set; }
        public string CartonBoxDimH { get; set; }
        public int Qnt20DC { get; set; }
        public int Qnt40DC { get; set; }
        public int Qnt40HC { get; set; }
        public string CBM { get; set; }
        public string QntInBox { get; set; }
        public string NetWeight { get; set; }
        public string GrossWeight { get; set; }
        public int? ClientSpecialPackagingMethodID { get; set; }
    }
}