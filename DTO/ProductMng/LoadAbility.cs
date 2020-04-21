using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ProductMng
{
    public class LoadAbility
    {
        public int ProductLoadAbilityID { get; set; }

        public int PackagingMethodID { get; set; }

        public int Qnt20DC { get; set; }

        public int Qnt40DC { get; set; }

        public int Qnt40HC { get; set; }

        public string Comment { get; set; }

        public bool IsConfirmed { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int ConfirmedBy { get; set; }

        public DateTime ConfirmedDate { get; set; }

        public decimal GrossWeight { get; set; }

        public string QntInBox { get; set; }

        public string UpdatorName { get; set; }

        public string ConfirmerName { get; set; }

    }
}