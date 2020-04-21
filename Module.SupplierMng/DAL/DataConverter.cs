using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SupplierMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;
            if (FrameworkSetting.Setting.Maps.Contains(mapName))
                return;

            AutoMapper.Mapper.CreateMap<SupplierMng_SupplierSearchResult_View, DTO.SupplierSearchResult>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<SupplierMng_Factory_View, DTO.Factory>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<SupplierMng_Supplier_View, DTO.Supplier>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.Factories, o => o.MapFrom(s => s.SupplierMng_Factory_View))
                .ForMember(d => d.supplierBanks, o => o.MapFrom(s => s.SupplierMng_SupplierBank_View))
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<DTO.Supplier, Supplier>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.UpdatedDate, o => o.Ignore())
                .ForMember(d => d.SupplierID, o => o.Ignore());

            AutoMapper.Mapper.CreateMap<DTO.SupplierBankDTO, SupplierBank>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            // delivery term
            AutoMapper.Mapper.CreateMap<List_DeliveryTerm, DTO.DeliveryTerm>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            // payment term
            AutoMapper.Mapper.CreateMap<List_PaymentTerm, DTO.PaymentTerm>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            // manufacturer country
            AutoMapper.Mapper.CreateMap<List_ManufacturerCountry_View, DTO.ManufacturerCountry>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
            // compay
            AutoMapper.Mapper.CreateMap<List_Company_View, DTO.Company>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<SupplierMng_SupplierBank_View, DTO.SupplierBankDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));



            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        public List<DTO.SupplierSearchResult> DB2DTO_SupplierSearchResultList(List<SupplierMng_SupplierSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupplierMng_SupplierSearchResult_View>, List<DTO.SupplierSearchResult>>(dbItems);
        }

        public DTO.Supplier DB2DTO_Supplier(SupplierMng_Supplier_View dbItem)
        {
            DTO.Supplier dtoItem = AutoMapper.Mapper.Map<SupplierMng_Supplier_View, DTO.Supplier>(dbItem);
            dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dtoItem.ConcurrencyFlag);
            return dtoItem;
        }

        public void DTO2BD(DTO.Supplier dtoItem, ref Supplier dbItem)
        {
            // 
            if (dtoItem.supplierBanks != null)
            {
                foreach (var item in dbItem.SupplierBank.ToArray())
                {
                    if (!dtoItem.supplierBanks.Select(o => o.SupplierBankID).Contains(item.SupplierBankID))
                    {
                        dbItem.SupplierBank.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.supplierBanks)
                {
                    SupplierBank dbSupplierBank;
                    if (item.SupplierBankID <= 0)
                    {
                        dbSupplierBank = new SupplierBank();
                        dbItem.SupplierBank.Add(dbSupplierBank);
                    }
                    else
                    {
                        dbSupplierBank = dbItem.SupplierBank.FirstOrDefault(o => o.SupplierBankID == item.SupplierBankID);
                    }
                    if (dbSupplierBank != null)
                    {
                        AutoMapper.Mapper.Map<DTO.SupplierBankDTO, SupplierBank>(item, dbSupplierBank);
                    }
                }
            }

            AutoMapper.Mapper.Map<DTO.Supplier, Supplier>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
        }

        public List<DTO.ManufacturerCountry> DB2DTO_ManufacturerCountry(List<List_ManufacturerCountry_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_ManufacturerCountry_View>, List<DTO.ManufacturerCountry>>(dbItems);
        }
        public List<DTO.Company> DB2DTO_Company(List<List_Company_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_Company_View>, List<DTO.Company>>(dbItems);
        }
        public List<DTO.PaymentTerm> DB2DTO_PaymentTerm(List<List_PaymentTerm> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_PaymentTerm>, List<DTO.PaymentTerm>>(dbItems);
        }

        public List<DTO.DeliveryTerm> DB2DTO_DeliveryTerm(List<List_DeliveryTerm> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_DeliveryTerm>, List<DTO.DeliveryTerm>>(dbItems);
        }
    }
}
