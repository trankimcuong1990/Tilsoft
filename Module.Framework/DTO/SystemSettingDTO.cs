using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Module.Framework.DTO
{
    public class SystemSettingDTO
    {
        public int SystemSettingID { get; set; }

        [MaxLength(50, ErrorMessage = "Key can not exceed 50 chars!")]
        public string SettingKey { get; set; }
        [MaxLength(255, ErrorMessage = "Value can not exceed 255 chars!")]
        public string SettingValue { get; set; }
        [MaxLength(9, ErrorMessage = "Season can not exceed 9 chars!")]
        public string Season { get; set; }
        [MaxLength(255, ErrorMessage = "Description can not exceed 255 chars!")]
        public string Description { get; set; }
        [MaxLength(8, ErrorMessage = "Type can not exceed 8 chars!")]
        public string SettingValueType { get; set; }
        public int? UpdatedBy { get; set; }
        public string EmployeeNM { get; set; }
        public string UpdatedDate { get; set; }
    }
}
