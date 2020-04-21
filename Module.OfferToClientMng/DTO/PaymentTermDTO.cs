using System.ComponentModel.DataAnnotations;

namespace Module.OfferToClientMng.DTO
{
    public class PaymentTermDTO
    {
        [Display(Name = "PaymentTermID")]
        public int? PaymentTermID { get; set; }

        [Display(Name = "PaymentTermNM")]
        public string PaymentTermNM { get; set; }

        [Display(Name = "PaymentTypeNM")]
        public string PaymentTypeNM { get; set; }

        public int? PaymentTypeID { get; set; }
        public int? InDays { get; set; }
        public string PaymentMethod { get; set; }
        public decimal? DownValue { get; set; }
    }
}
