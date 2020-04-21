using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftCommercialInvoiceMng.DTO
{
    public class SupportClient
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
        public string ClientNM { get; set; }
        public string ClientUD { get; set; }
        public string VATNo { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
    }
}
