using Module.ProductionItemGroup.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItemGroup.DTO
{
    public class ProductionItemGroupDTO
    {
        public int? ProductionItemGroupID { get; set; }
        public string ProductionItemGroupUD { get; set; }
        public string ProductionItemGroupNM { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorNM { get; set; }
        public decimal? WastagePercent { get; set; }
        public List<ProductionItemGroupNotificationDTO> ListProductionItemGroupNotification { get; set; }
        public List<ProductionItemGroupStockNotificationDTO> ListProductionItemGroupStockNotification { get; set; }
    }
    
    // list user 
    public class UserOnProductionItemGroup
    {
        public int? UserID { get; set; }
        public string EmployeeNM { get; set; }
        public int EmployeeID { get; set; }      
        
        public string InternalCompanyNM { get; set; }
        public Nullable<int> CompanyID { get; set; }
    }

}
