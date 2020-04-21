using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.TransferShowroomAreaMng
{
    internal class DataConverter
    {
        public DataConverter() 
        {
            string mapName = "TransferShowroomAreaMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<TransferShowroomAreaMng_TransferShowroomArea_SearchView, DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch, TransferShowroomArea>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.TransferShowroomAreaID, o => o.Ignore())
                   .ForMember(d => d.TransferDate, o => o.Ignore())
                   ;
               
                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        
        public List<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch> DB2DTO_TransferShowroomAreaSearch(List<TransferShowroomAreaMng_TransferShowroomArea_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<TransferShowroomAreaMng_TransferShowroomArea_SearchView>, List<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch>>(dbItems);
        }
        public void DTO2DB_TransferArea(DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch dtoItem, ref TransferShowroomArea dbItem)
        {
            AutoMapper.Mapper.Map<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch, TransferShowroomArea>(dtoItem, dbItem);
            dbItem.TransferDate = DateTime.Now;
        }
        public DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch DB2DTO_TransferShowroomArea(TransferShowroomAreaMng_TransferShowroomArea_SearchView dbItem)
        {
            return AutoMapper.Mapper.Map<TransferShowroomAreaMng_TransferShowroomArea_SearchView, DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch>(dbItem);
        }
    }
}
