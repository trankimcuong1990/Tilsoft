﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.NhanVienMng.DTO
{
    public class EditFormData
    {
        public DTO.NhanVienDTO Data { get; set; }
        public List<DTO.PhongBanDTO> PhongBanDTOs { get; set; }
    }
}
