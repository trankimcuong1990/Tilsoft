using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.FactoryMng
{
    public class EditFormData
    {
        public DTO.FactoryMng.Factory Data { get; set; }
        public List<DTO.Support.Supplier> Suppliers { get; set; }
        public List<DTO.Support.Location> Locations { get; set; }
    }
}
