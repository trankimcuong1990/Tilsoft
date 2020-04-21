using System.Collections.Generic;

namespace DTO.ClientMng.Overview.CIS
{
    public class ItemDataContainer
    {
        public string Season { get; set; }
        public decimal ExRate { get; set; }
        public List<DTO.ClientMng.Overview.CIS.ItemDTO> ItemDTOs { get; set; }
        public List<DTO.ClientMng.Overview.CIS.ModelDTO> ModelDTOs { get; set; }

        public ItemDataContainer()
        {
            ItemDTOs = new List<ItemDTO>();
            ModelDTOs = new List<ModelDTO>();
        }
    }
}
