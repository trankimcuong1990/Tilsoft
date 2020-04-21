using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.MaterialMng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private DateTime tmpDate; 

        public DataConverter()
        {
            string mapName = "MaterialMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<MaterialMng_MaterialSearchResult_View, DTO.MaterialSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MaterialMng_Material_View, DTO.Material>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.Material, Material>()
                    .ForMember(d => d.MaterialID, o => o.Ignore())
                    .IgnoreAllNonExisting();

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.MaterialSearchResult> DB2DTO_MaterialSearchResult(List<MaterialMng_MaterialSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MaterialMng_MaterialSearchResult_View>, List<DTO.MaterialSearchResult>>(dbItems);
        }

        public DTO.Material DB2DTO_Material(MaterialMng_Material_View dbItem)
        {
            return AutoMapper.Mapper.Map<MaterialMng_Material_View, DTO.Material>(dbItem);
        }

        public void DTO2DB(DTO.Material dtoItem, ref Material dbItem, string TmpFile, int userId)
        {
            AutoMapper.Mapper.Map<DTO.Material, Material>(dtoItem, dbItem);
        }
    }
}
