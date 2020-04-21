using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.NhanVienMng.DTO
{
    public class NhanVienSearchResultDTO
    {
        public int NhanVienID { get; set; }
        public string NhanVienUD { get; set; }
        public string NhanVienNM { get; set; }
        public string PhongBanNM { get; set; }
        public string ThumbnailLocation { get; set; }
        public string NgaySinh { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
    }
}
