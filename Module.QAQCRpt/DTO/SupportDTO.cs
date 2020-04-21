using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QAQCRpt.DTO
{
    public class SupportDTO
    {
        public SupportDTO()
        {
            clients = new List<SupportClient>();
            factories = new List<SupportFactory>();
            productGroups = new List<SupportProductGroup>();
        }
        public List<DTO.SupportClient> clients { get; set; }
        public List<DTO.SupportFactory> factories { get; set; }
        public List<DTO.SupportProductGroup> productGroups { get; set; }
    }

    public class SupportClient
    {
        public int ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
    }

    public class SupportFactory
    {
        public int FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
    }

    public class SupportProductGroup
    {
        public int ConstantEntryID { get; set; }
        public Nullable<int> ProductGroupID { get; set; }
        public string ProductGroupNM { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
    }
}
