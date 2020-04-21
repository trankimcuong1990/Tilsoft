using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItem.DTO
{
    public class EditFormData
    {
        public DTO.ProductionItem Data { get; set; }
        //public List<Module.Support.DTO.FactoryWarehouseDto> FactoryWarehouses { get; set; }
        public List<DTO.FactoryWarehouse> FactoryWarehouses { get; set; }
        public List<DTO.FactoryRawMaterialSupplier> FactoryRawMaterialSuppliers { get; set; }
        public List<Support.DTO.Unit> Units { get; set; }
        public List<DTO.ProductionItemGroup> ProductionItemGroups { get; set; }
        public List<DTO.ProductionItemType> ProductionItemMaterialTypes { get; set; }
        public List<DTO.ProductionItemSpec> ProductionItemSpecs { get; set; }
        public List<Support.DTO.ProductionItemType> ProductionItemTypes { get; set; }
        public List<DTO.AssetClassDTO> assetClassDTOs { get; set; }
        public List<DTO.DepreciationTypeDTO> depreciationTypeDTOs { get; set; }
        public List<DTO.BreakDownCategoryDTO> breakDownCategoryDTOs { get; set; }
        public List<DTO.OutSourcingCostTypeDTO> outSourcingCostTypeDTOs { get; set; }

        // Add new support: Branch.
        public List<BranchDTO> Branches { get; set; }
        public int? UserGroupID { get; set; }
    }
}
