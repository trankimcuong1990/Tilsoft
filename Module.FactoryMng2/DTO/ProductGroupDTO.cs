using System;

namespace Module.FactoryMng2.DTO
{
    public class ProductGroupDTO
    {
        public int ConstantEntryID { get; set; }
        public Nullable<int> ProductGroupID { get; set; }
        public string ProductGroupNM { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public bool IsChecked { get; set; }
    }
}
