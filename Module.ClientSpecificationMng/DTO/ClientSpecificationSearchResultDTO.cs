using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientSpecificationMng.DTO
{
    public class ClientSpecificationSearchResultDTO
    {
        public int ClientSpecificationID { get; set; }
        public string ClientSpecificationUD { get; set; }
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ClientSpecificationFileUD { get; set; }
        public string FriendlyName { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public int? ClientSpecificationTypeID { get; set; }
        public int? FactoryID { get; set; }
        public int? ClientSpecificationUpdatedBy { get; set; }
        public string ClientSpecificationUpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public bool? HasUrlLink { get; set; }
        public List<ClientSpecificationOfferPIDTO> ClientSpecificationPIs { get; set; }
    }
}
