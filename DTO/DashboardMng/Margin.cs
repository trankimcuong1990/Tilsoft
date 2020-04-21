using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.DashboardMng
{
    public class Margin
    {
        public string Season { get; set; }

        public decimal? GrossMargin { get; set; }

        public decimal? NetMargin { get; set; }

    }
}