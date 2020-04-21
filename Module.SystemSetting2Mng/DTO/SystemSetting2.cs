using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SystemSetting2Mng.DTO
{
    public class SystemSetting2
    {
        public int SystemSettingID { get; set; }
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public string Description { get; set; }
        public bool? IsComboBox { get; set; }
        public string Season { get; set; }
    }
}
