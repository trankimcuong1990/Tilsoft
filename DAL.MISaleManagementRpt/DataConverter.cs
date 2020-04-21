using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.MISaleManagementRpt
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "MISaleManagementRpt";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<MISaleManagementRpt_AllSaleProformaByMonth_View, DTO.MISaleManagementRpt.AllSaleProformaByMonth>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleManagementRpt_ConfirmedSaleProformaByMonth_View, DTO.MISaleManagementRpt.ConfirmedSaleProformaByMonth>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleManagementRpt_DDCProformaBySale_View, DTO.MISaleManagementRpt.DDCProformaBySale>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.MISaleManagementRpt.AllSaleProformaByMonth> DB2DTO_AllSaleProformaByMonthList(List<MISaleManagementRpt_AllSaleProformaByMonth_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleManagementRpt_AllSaleProformaByMonth_View>, List<DTO.MISaleManagementRpt.AllSaleProformaByMonth>>(dbItems);
        }

        public List<DTO.MISaleManagementRpt.ConfirmedSaleProformaByMonth> DB2DTO_ConfirmedSaleProformaByMonthList(List<MISaleManagementRpt_ConfirmedSaleProformaByMonth_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleManagementRpt_ConfirmedSaleProformaByMonth_View>, List<DTO.MISaleManagementRpt.ConfirmedSaleProformaByMonth>>(dbItems);
        }

        public List<DTO.MISaleManagementRpt.DDCProformaBySale> DB2DTO_DDCProformaBySaleList(List<MISaleManagementRpt_DDCProformaBySale_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleManagementRpt_DDCProformaBySale_View>, List<DTO.MISaleManagementRpt.DDCProformaBySale>>(dbItems);
        }
    }
}
