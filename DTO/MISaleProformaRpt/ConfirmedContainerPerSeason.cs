using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.MISaleProformaRpt
{
    public class ConfirmedContainerPerSeason
    {
        public string Season { get; set; }
        public decimal? TotalCont { get; set; }
    }
}