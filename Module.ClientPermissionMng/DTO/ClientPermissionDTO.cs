using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientPermissionMng.DTO
{
    public class ClientPermissionDTO
    {
        public int? ClientPermissionID { get; set; }
        public int? ClientID { get; set; }      
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Remark { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public int? CreatedID { get; set; }
        public string CreatedNM { get; set; }
        public int? UpdatorID { get; set; }
        public string UpdatorNM { get; set; }
        public List<DTO.ClientPermissionDetailDTO> ClientPermissionDetailDTOs { get; set; }
    }
}
