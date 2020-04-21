using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DTO.ForwarderMng
{
    public class Forwarder
    {
        public int ForwarderID { get; set; }

        [Required]
        [MaxLength(4, ErrorMessage = "Forwarder Code must has 4 characters, only number or letter accepted")]
        [MinLength(2, ErrorMessage = "Forwarder Code must has 2 characters, only number or letter accepted")]
        public string ForwarderUD { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Forwarder Name only accept 255 characters")]
        public string ForwarderNM { get; set; }

        public int? CountryID { get; set; }

        public string CountryNM { get; set; }

        public string Address { get; set; }

        public string TaxCode { get; set; }

        public string Name { get; set; }

        public string JobTitle { get; set; }

        public int? InternalDepartmentID { get; set; }

        public string InternalDepartmentNM { get; set; }

        public string Tel { get; set; }

        public string Fax { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string PIC { get; set; }

        public string Skype { get; set; }

        public string Comment { get; set; }

        public int? UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public string UpdatorName { get; set; }

        public List<ForwarderImage> ForwarderImages { get; set; }
        public List<ForwarderPIC> ForwarderPICs { get; set; }
    }
}
