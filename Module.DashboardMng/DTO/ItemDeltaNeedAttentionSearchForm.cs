using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class ItemDeltaNeedAttentionSearchForm
    {
        public int TotalRows { get; set; }
        public List<DTO.ItemDeltaNeedAttentionDTO> Data { get; set; }
    }
}
