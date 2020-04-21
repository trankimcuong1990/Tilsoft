using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.NhanVienMng.DTO
{
    public class NhanVienDTO
    {
        public int NhanVienID { get; set; }
        public string NhanVienUD { get; set; }
        public string NhanVienNM { get; set; }
        public int PhongBanID { get; set; }
        public string Photo { get; set; }
        public string NgaySinh { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        

        public List<DTO.NguoiPhuThuocDTO> NguoiPhuThuocDTOs { get; set; }


        public string NewPhoto { get; set; }
        public bool PhotoHasChanged { get; set; }
    }
}
