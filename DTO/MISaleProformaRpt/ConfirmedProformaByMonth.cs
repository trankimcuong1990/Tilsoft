using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.MISaleProformaRpt
{
    public class ConfirmedProformaByMonth
    {
        public string Season { get; set; }

        public decimal? January { get; set; }

        public decimal? Febuary { get; set; }

        public decimal? March { get; set; }

        public decimal? April { get; set; }

        public decimal? May { get; set; }

        public decimal? June { get; set; }

        public decimal? July { get; set; }

        public decimal? August { get; set; }

        public decimal? September { get; set; }

        public decimal? October { get; set; }

        public decimal? November { get; set; }

        public decimal? December { get; set; }

    }
}