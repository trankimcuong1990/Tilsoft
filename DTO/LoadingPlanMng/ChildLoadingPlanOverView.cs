using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.LoadingPlanMng
{
   public class ChildLoadingPlanOverView
    {
        public int LoadingPlanID { get; set; }

        public int? ParentID { get; set; }

        [Display(Name = "IsSent")]
        public object IsSent { get; set; }

        [Display(Name = "IsLoaded")]
        public bool? IsLoaded { get; set; }

        [Display(Name = "IsShipped")]
        public bool? IsShipped { get; set; }

        [Display(Name = "IsConfirmed")]
        public bool? IsConfirmed { get; set; }

        [Display(Name = "LoadingPlanUD")]
        public string LoadingPlanUD { get; set; }

        [Display(Name = "ContainerRefNo")]
        public string ContainerRefNo { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "FactoryUD")]
        public string FactoryUD { get; set; }

        [Display(Name = "ControllerName")]
        public string ControllerName { get; set; }

        [Display(Name = "BookingUD")]
        public string BookingUD { get; set; }

        [Display(Name = "BLNo")]
        public string BLNo { get; set; }

        [Display(Name = "ContainerNo")]
        public string ContainerNo { get; set; }

        [Display(Name = "ContainerTypeNM")]
        public string ContainerTypeNM { get; set; }

        [Display(Name = "LoadingDate")]
        public string LoadingDate { get; set; }

        [Display(Name = "ShippedDate")]
        public string ShippedDate { get; set; }

        [Display(Name = "ShipmentDate")]
        public string ShipmentDate { get; set; }

        [Display(Name = "SenderName")]
        public string SenderName { get; set; }

        [Display(Name = "SentDate")]
        public string SentDate { get; set; }

        [Display(Name = "ConfirmerName")]
        public string ConfirmerName { get; set; }

        [Display(Name = "ConfirmedDate")]
        public string ConfirmedDate { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "UpdatedDate")]
        public string UpdatedDate { get; set; }
    }
}
