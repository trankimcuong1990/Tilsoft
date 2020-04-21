using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng.DTO
{
    public class EditFormData
    {
        public FactoryRawMaterial Data { get; set; }
        public List<Module.Support.DTO.FactoryLocation> Locations { get; set; }
        public List<DTO.KeyRawMaterial> KeyRawMaterials { get; set; }
        public List<Module.Support.DTO.Employee> Employees { get; set; }
        public List<DTO.SupplierPaymentTerm> SupplierPaymentTerms { get; set; }
        public List<DTO.SubSupplierDocumentTypeDTO> SubSupplierDocumentTypeDTOs { get; set; }
        public List<DTO.ProductGroupDTO> ProductGroupDTOs { get; set; }
        public List<DTO.MaterialsPrice> materialsPrices { get; set; }
        public List<DTO.StatusMaterialPrice> statusMaterialPrices { get; set; }       
        public List<DTO.UsersDTO> UsersDTO { get; set; }
        public List<DTO.AppointmentDTO> AppointmentDTO { get; set; }
    }
}
