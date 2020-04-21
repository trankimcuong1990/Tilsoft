using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CostInvoice2Mng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;

            if (FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                return;
            }

            AutoMapper.Mapper.CreateMap<CostInvoice2Mng_CostInvoice2SearchResult_View, DTO.CostInvoice2SearchResult>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(d => d.DueDate, o => o.ResolveUsing(s => s.DueDate.HasValue ? s.DueDate.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(d => d.PaidDate, o => o.ResolveUsing(s => s.PaidDate.HasValue ? s.PaidDate.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(d => d.CostInvoice2Clients, o => o.MapFrom(s => s.CostInvoice2Mng_CostInvoice2Client_View))
                .ForMember(d => d.CostInvoice2Factories, o => o.MapFrom(s => s.CostInvoice2Mng_CostInvoice2Factory_View))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<CostInvoice2Mng_CostInvoice2_View, DTO.CostInvoice2>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(d => d.DueDate, o => o.ResolveUsing(s => s.DueDate.HasValue ? s.DueDate.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(d => d.PaidDate, o => o.ResolveUsing(s => s.PaidDate.HasValue ? s.PaidDate.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(d => d.CostInvoice2Clients, o => o.MapFrom(s => s.CostInvoice2Mng_CostInvoice2Client_View))
                .ForMember(d => d.CostInvoice2Factories, o => o.MapFrom(s => s.CostInvoice2Mng_CostInvoice2Factory_View))
                .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<CostInvoice2Mng_CostInvoice2Client_View, DTO.CostInvoice2Client>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<CostInvoice2Mng_CostInvoice2Factory_View, DTO.CostInvoice2Factory>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<DTO.CostInvoice2, CostInvoice2>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.CostInvoice2ID, o => o.Ignore())
                .ForMember(d => d.CostInvoice2UD, o => o.Ignore())
                .ForMember(d => d.InvoiceDate, o => o.Ignore())
                .ForMember(d => d.DueDate, o => o.Ignore())
                .ForMember(d => d.PaidDate, o => o.Ignore())
                .ForMember(d => d.ConfirmedBy, o => o.Ignore())
                .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                .ForMember(d => d.UpdatedBy, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore());

            AutoMapper.Mapper.CreateMap<DTO.CostInvoice2Client, CostInvoice2Client>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.CostInvoice2ClientID, o => o.Ignore());

            AutoMapper.Mapper.CreateMap<DTO.CostInvoice2Factory, CostInvoice2Factory>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.CostInvoice2FactoryID, o => o.Ignore());

            // Support data in CostInvoice2Mng
            AutoMapper.Mapper.CreateMap<CostInvoice2Mng_CostInvoice2Creditor_View, DTO.CostInvoice2Creditor>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<CostInvoice2Mng_CostInvoice2Type_View, DTO.CostInvoice2Type>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        public List<DTO.CostInvoice2SearchResult> DB2DTO_CostInvoice2SearchResults(List<CostInvoice2Mng_CostInvoice2SearchResult_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<CostInvoice2Mng_CostInvoice2SearchResult_View>, List<DTO.CostInvoice2SearchResult>>(dbItem);
        }

        public DTO.CostInvoice2 DB2DTO_CostInvoice2(CostInvoice2Mng_CostInvoice2_View dbItem)
        {
            return AutoMapper.Mapper.Map<CostInvoice2Mng_CostInvoice2_View, DTO.CostInvoice2>(dbItem);
        }

        public List<DTO.CostInvoice2Creditor> DB2DTO_CostInvoice2Creditors(List<CostInvoice2Mng_CostInvoice2Creditor_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<CostInvoice2Mng_CostInvoice2Creditor_View>, List<DTO.CostInvoice2Creditor>>(dbItem);
        }

        public List<DTO.CostInvoice2Type> DB2DTO_CostInvoice2Types(List<CostInvoice2Mng_CostInvoice2Type_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<CostInvoice2Mng_CostInvoice2Type_View>, List<DTO.CostInvoice2Type>>(dbItem);
        }

        public void DTO2DB_CostInvoice2(DTO.CostInvoice2 dtoItem, ref CostInvoice2 dbItem)
        {
            // Mapping CostInvoice2Client.
            if (dtoItem.CostInvoice2Clients != null)
            {
                foreach (CostInvoice2Client item in dbItem.CostInvoice2Client.ToList())
                {
                    if (!dtoItem.CostInvoice2Clients.Select(s => s.CostInvoice2ClientID).Contains(item.CostInvoice2ClientID))
                    {
                        dbItem.CostInvoice2Client.Remove(item);
                    }
                }

                foreach (DTO.CostInvoice2Client dto in dtoItem.CostInvoice2Clients)
                {
                    CostInvoice2Client item;

                    if (dto.CostInvoice2ClientID < 0)
                    {
                        item = new CostInvoice2Client();
                        dbItem.CostInvoice2Client.Add(item);
                    }
                    else
                    {
                        item = dbItem.CostInvoice2Client.FirstOrDefault(s => s.CostInvoice2ClientID == dto.CostInvoice2ClientID);
                    }

                    if (item != null)
                    {
                        AutoMapper.Mapper.Map<DTO.CostInvoice2Client, CostInvoice2Client>(dto, item);
                    }
                }
            }

            // Mapping CostInvoice2Factory.
            if (dtoItem.CostInvoice2Factories != null)
            {
                foreach (CostInvoice2Factory item in dbItem.CostInvoice2Factory.ToList())
                {
                    if (!dtoItem.CostInvoice2Factories.Select(s => s.CostInvoice2FactoryID).Contains(item.CostInvoice2FactoryID))
                    {
                        dbItem.CostInvoice2Factory.Remove(item);
                    }
                }

                foreach (DTO.CostInvoice2Factory dto in dtoItem.CostInvoice2Factories)
                {
                    CostInvoice2Factory item;

                    if (dto.CostInvoice2FactoryID < 0)
                    {
                        item = new CostInvoice2Factory();
                        dbItem.CostInvoice2Factory.Add(item);
                    }
                    else
                    {
                        item = dbItem.CostInvoice2Factory.FirstOrDefault(s => s.CostInvoice2FactoryID == dto.CostInvoice2FactoryID);
                    }

                    if (item != null)
                    {
                        AutoMapper.Mapper.Map<DTO.CostInvoice2Factory, CostInvoice2Factory>(dto, item);
                    }
                }
            }

            AutoMapper.Mapper.Map<DTO.CostInvoice2, CostInvoice2>(dtoItem, dbItem);
        }
    }
}
