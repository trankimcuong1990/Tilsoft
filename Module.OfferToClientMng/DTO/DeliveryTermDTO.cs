using System.ComponentModel.DataAnnotations;

namespace Module.OfferToClientMng.DTO
{
    public class DeliveryTermDTO
    {
        [Display(Name = "DeliveryTermID")]
        public int DeliveryTermID { get; set; }

        [Display(Name = "DeliveryTermNM")]
        public string DeliveryTermNM { get; set; }

        [Display(Name = "DeliveryTypeNM")]
        public string DeliveryTypeNM { get; set; }
    }
}
