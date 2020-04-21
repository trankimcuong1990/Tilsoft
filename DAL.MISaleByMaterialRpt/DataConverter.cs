using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.MISaleByMaterialRpt
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "MISaleByMaterialRpt";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<MISaleByMaterialRpt_AllMaterial_View, DTO.MISaleByMaterialRpt.AllMaterial>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByMaterialRpt_Wood_View, DTO.MISaleByMaterialRpt.Wood>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByMaterialRpt_AllMaterial_ProformaInvoice_View, DTO.MISaleByMaterialRpt.ProformaInvoiceAllMaterial>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByMaterialRpt_Wood_ProformaInvoice_View, DTO.MISaleByMaterialRpt.ProformaInvoiceWood>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.MISaleByMaterialRpt.AllMaterial> DB2DTO_AllMaterialList(List<MISaleByMaterialRpt_AllMaterial_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByMaterialRpt_AllMaterial_View>, List<DTO.MISaleByMaterialRpt.AllMaterial>>(dbItems);
        }

        public List<DTO.MISaleByMaterialRpt.Wood> DB2DTO_WoodList(List<MISaleByMaterialRpt_Wood_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByMaterialRpt_Wood_View>, List<DTO.MISaleByMaterialRpt.Wood>>(dbItems);
        }
    }
}
