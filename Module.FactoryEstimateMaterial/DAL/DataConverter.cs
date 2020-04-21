using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.FactoryEstimateMaterial.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryEstimateMaterialMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<FactoryEstimateMaterialMng_FactoryOrderDetail_View, DTO.FactoryOrderDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryOrderDetail, DTO.FactoryOrderDetail>()
                    .IgnoreAllNonExisting()
                    //.ForMember(d => d.IsSelected, o => o.UseValue(true))
                    .ForMember(d => d.IsSelected, o => o.ResolveUsing(s => s.FactoryMaterialID.HasValue))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryOrderDetail> DB2DTO_FactoryOrderDetail(List<FactoryEstimateMaterialMng_FactoryOrderDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryEstimateMaterialMng_FactoryOrderDetail_View>, List<DTO.FactoryOrderDetail>>(dbItems);
        }

        public List<DTO.FactoryOrderDetail> DTO2DTO_FactoryOrderDetail(List<DTO.FactoryOrderDetail> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DTO.FactoryOrderDetail>, List<DTO.FactoryOrderDetail>>(dbItems);
        }
    }
}
