using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DTO.SystemSettingMng
{
    public class SystemSetting
    {
        public int SystemSettingID { get; set; }

         [MaxLength(255, ErrorMessage = "Setting value accepts no more than 255 characters.")]
        public string SettingValue { get; set; }

        [MaxLength(50, ErrorMessage = "System Setting accepts no more than 50 characters.")]
        public string SettingKey { get; set; }

        [MaxLength(9, ErrorMessage = "Season accepts no more than 9 characters.")]
        public string Season { get; set; }

        
        [MaxLength(255, ErrorMessage = "Description accepts no more than 255 characters.")]
        public string Description { get; set; }
    }
}
