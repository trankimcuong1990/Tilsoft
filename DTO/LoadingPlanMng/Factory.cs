using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.LoadingPlanMng
{
    public class Factory
    {
        public int FactoryID { get; set; }

        [Display(Name = "FactoryUD")]
        public string FactoryUD { get; set; }

        [Display(Name = "FactoryName")]
        public string FactoryName { get; set; }

        public List<Booking> Bookings { get; set; }

    }
}