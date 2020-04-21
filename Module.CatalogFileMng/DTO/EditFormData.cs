using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CatalogFileMng.DTO
{
    public class EditFormData
    {
        public DTO.CatalogFileDTO Data { get; set; }

        public List<Support.DTO.Season> Seasons { get; set; }
    }
}
