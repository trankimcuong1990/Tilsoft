using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CompanyBranchMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "CompanyBranchMng";

            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<CompanyBranchMng_CompanySearchResult_View, DTO.CompanySearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaUrl + (string.IsNullOrEmpty(s.FileLocation) ? "no-image.jpg" : s.FileLocation)))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (string.IsNullOrEmpty(s.ThumbnailLocation) ? "no-image.jpg" : s.ThumbnailLocation)))
                    .ForMember(d => d.UpdatorName, o => o.ResolveUsing(s => s.EmployeeNM))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CompanyBranchMng_Company_View, DTO.CompanyDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaUrl + (string.IsNullOrEmpty(s.FileLocation) ? "no-image.jpg" : s.FileLocation)))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (string.IsNullOrEmpty(s.ThumbnailLocation) ? "no-image.jpg" : s.ThumbnailLocation)))
                    .ForMember(d => d.Branches, o => o.MapFrom(s => s.CompanyBranchMng_Branch_View))
                    .ForMember(d => d.UpdatorName, o => o.ResolveUsing(s => s.EmployeeNM))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CompanyBranchMng_Branch_View, DTO.BranchDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.CompanyDTO, Company>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CompanyID, o => o.Ignore())
                    .ForMember(d => d.Branch, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.BranchDTO, Branch>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BranchID, o => o.Ignore())
                    .ForMember(d => d.CompanyID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CompanyBranchMng_Location_View, DTO.LocationDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CompanyBranchMng_FactoryQuickSearchResult_View, DTO.BranchQuickSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ID, o => o.ResolveUsing(s => s.FactoryID))
                    .ForMember(d => d.Value, o => o.ResolveUsing(s => s.FactoryUD))
                    .ForMember(d => d.Label, o => o.ResolveUsing(s => s.FactoryName))
                    .ForMember(d => d.StreetAddress, o => o.ResolveUsing(s => s.Address))
                    .ForMember(d => d.Phone, o => o.ResolveUsing(s => s.Telephone))
                    .ForMember(d => d.Fax, o => o.ResolveUsing(s => s.Fax))
                    .ForMember(d => d.Email, o => o.ResolveUsing(s => s.Email))
                    .ForMember(d => d.FactoryID, o => o.ResolveUsing(s => s.FactoryID))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CompanyBranchMng_CompanyQuickSearchResult_View, DTO.BranchQuickSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ID, o => o.ResolveUsing(s => s.CompanyID))
                    .ForMember(d => d.Value, o => o.ResolveUsing(s => s.CompanyUD))
                    .ForMember(d => d.Label, o => o.ResolveUsing(s => s.CompanyNM))
                    .ForMember(d => d.StreetAddress, o => o.ResolveUsing(s => s.StreetAddress))
                    .ForMember(d => d.Phone, o => o.ResolveUsing(s => s.Phone))
                    .ForMember(d => d.Fax, o => o.ResolveUsing(s => s.Fax))
                    .ForMember(d => d.Email, o => o.ResolveUsing(s => s.Email))
                    .ForMember(d => d.FactoryID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
            }
        }

        public List<DTO.CompanySearchResultDTO> DB2DTO_CompanySearchResult(List<CompanyBranchMng_CompanySearchResult_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<CompanyBranchMng_CompanySearchResult_View>, List<DTO.CompanySearchResultDTO>>(dbItem);
        }

        public DTO.CompanyDTO DB2DTO_Company(CompanyBranchMng_Company_View dbItem)
        {
            return AutoMapper.Mapper.Map<CompanyBranchMng_Company_View, DTO.CompanyDTO>(dbItem);
        }

        public void DTO2DB_Company(DTO.CompanyDTO dtoCompany, ref Company dbCompany)
        {
            AutoMapper.Mapper.Map<DTO.CompanyDTO, Company>(dtoCompany, dbCompany);

            // Branch
            foreach (Branch dbBranch in dbCompany.Branch.ToList())
            {
                if (!dtoCompany.Branches.Select(s => s.BranchID).Contains(dbBranch.BranchID))
                {
                    dbCompany.Branch.Remove(dbBranch);
                }
            }

            foreach (DTO.BranchDTO dtoBranch in dtoCompany.Branches.ToList())
            {
                Branch dbBranch;

                if (dtoBranch.BranchID <= 0)
                {
                    dbBranch = new Branch();
                    dbCompany.Branch.Add(dbBranch);
                }
                else
                {
                    dbBranch = dbCompany.Branch.FirstOrDefault(o => o.BranchID == dtoBranch.BranchID);
                }

                if (dbBranch != null)
                {
                    AutoMapper.Mapper.Map<DTO.BranchDTO, Branch>(dtoBranch, dbBranch);
                }
            }
        }

        public List<DTO.LocationDTO> DB2DTO_Location(List<CompanyBranchMng_Location_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<CompanyBranchMng_Location_View>, List<DTO.LocationDTO>>(dbItem);
        }

        public List<DTO.BranchQuickSearchResultDTO> DB2DTO_QuickSearchBranchFromFactory(List<CompanyBranchMng_FactoryQuickSearchResult_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<CompanyBranchMng_FactoryQuickSearchResult_View>, List<DTO.BranchQuickSearchResultDTO>>(dbItem);
        }

        public List<DTO.BranchQuickSearchResultDTO> DB2DTO_QuickSearchBranchFromCompany(List<CompanyBranchMng_CompanyQuickSearchResult_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<CompanyBranchMng_CompanyQuickSearchResult_View>, List<DTO.BranchQuickSearchResultDTO>>(dbItem);
        }
    }
}
