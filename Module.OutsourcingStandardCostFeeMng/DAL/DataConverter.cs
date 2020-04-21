using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.OutsourcingStandardCostFeeMng.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwfactory = new Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;

            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<OutsourcingStandardCostFeeMng_Model_SearchView, DTO.OutsourcingModel>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.outSourcingModelDetailSearches, s => s.MapFrom(o => o.OutsourcingStandardCostFeeMng_OutsourcingStandardCostFeeDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OutsourcingStandardCostFeeMng_OutsourcingStandardCostFeeDetail_View, DTO.OutSourcingModelDetailSearch>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OutsourcingStandardCostFeeMng_OutsourcingCost_View, DTO.OutSourcingCostSP>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OutsourcingStandardCostFeeMng_OutsourcingCostType_View, DTO.OutSourcingCostTypeSP>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OutsourcingStandardCostFeeMng_OutsourcingStandardCostFee_View, DTO.OutsourcingStandardCostFeeDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ValidFrom, o => o.ResolveUsing(s => !string.IsNullOrEmpty(s.ValidFrom) ? s.ValidFrom : DateTime.Now.ToString("dd/MM/yyyy")))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OutsourcingStandardCostFeeMng_ModelPiece_SearchView, DTO.OutsourcingModelPiece>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.outSourcingModelDetailSearches, s => s.MapFrom(o => o.OutsourcingStandardCostFeeMng_OutsourcingStandardCostFeeDetail_View))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.OutsourcingModel> BD2DTO_SearchModel(List<OutsourcingStandardCostFeeMng_Model_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OutsourcingStandardCostFeeMng_Model_SearchView>, List<DTO.OutsourcingModel>>(dbItems);
        }

        public List<DTO.OutSourcingModelDetailSearch> DB2DTO_SearchModelDetail(List<OutsourcingStandardCostFeeMng_OutsourcingStandardCostFeeDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OutsourcingStandardCostFeeMng_OutsourcingStandardCostFeeDetail_View>, List<DTO.OutSourcingModelDetailSearch>>(dbItems);
        }
        public List<DTO.OutsourcingStandardCostFeeDTO> DB2DTO_GetDetail(List<OutsourcingStandardCostFeeMng_OutsourcingStandardCostFee_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OutsourcingStandardCostFeeMng_OutsourcingStandardCostFee_View>, List<DTO.OutsourcingStandardCostFeeDTO>>(dbItems);
        }
        public List<DTO.OutsourcingModelPiece> BD2DTO_SearchModelPice(List<OutsourcingStandardCostFeeMng_ModelPiece_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OutsourcingStandardCostFeeMng_ModelPiece_SearchView>, List<DTO.OutsourcingModelPiece>>(dbItems);
        }

    }

}
