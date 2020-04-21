using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SupplierMng.DTO
{
    public class EditFormData
    {
        public Supplier Data { get; set; }
        public List<PaymentTerm> PaymentTerms { get; set; }
        public List<DeliveryTerm> DeliveryTerms { get; set; }
        public List<ManufacturerCountry> ManufacturerCountries { get; set; }
        public List<Company> Companies { get; set; }
    }
}
