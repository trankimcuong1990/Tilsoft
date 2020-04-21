using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;

namespace DAL.FactoryPaymentMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryPaymentMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FactoryPaymentMng_FactoryPaymentSearch_View, DTO.FactoryPaymentMng.FactoryPaymentSearch>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PaymentDate, o => o.ResolveUsing(s => s.PaymentDate.HasValue ? s.PaymentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryPaymentMng_FactoryPayment_View, DTO.FactoryPaymentMng.FactoryPayment>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PaymentDate, o => o.ResolveUsing(s => s.PaymentDate.HasValue ? s.PaymentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.ResolveUsing(s => s.ConcurrencyFlag != null ? Convert.ToBase64String(s.ConcurrencyFlag) : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryPaymentMng.FactoryPayment, FactoryPayment>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PaymentDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.FactoryPaymentID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryPaymentMng.FactoryPaymentSearch> DB2DTO_FactoryPaymentSearch(List<FactoryPaymentMng_FactoryPaymentSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryPaymentMng_FactoryPaymentSearch_View>, List<DTO.FactoryPaymentMng.FactoryPaymentSearch>>(dbItems);
        }

        public DTO.FactoryPaymentMng.FactoryPayment DB2DTO_FactoryPayment(FactoryPaymentMng_FactoryPayment_View dbItem)
        {
            DTO.FactoryPaymentMng.FactoryPayment dtoItem =  AutoMapper.Mapper.Map<FactoryPaymentMng_FactoryPayment_View, DTO.FactoryPaymentMng.FactoryPayment>(dbItem);
            dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dtoItem.ConcurrencyFlag);

            return dtoItem;
        }

        public void DTO2BD_FactoryPayment(DTO.FactoryPaymentMng.FactoryPayment dtoItem, ref FactoryPayment dbItem)
        {
            AutoMapper.Mapper.Map<DTO.FactoryPaymentMng.FactoryPayment, FactoryPayment>(dtoItem, dbItem);
            dbItem.PaymentDate = dtoItem.PaymentDate.ConvertStringToDateTime();
            if (dbItem.FactoryPaymentID > 0)
            {
                dbItem.UpdatedDate = DateTime.Now;
                dbItem.UpdatedBy = dtoItem.UpdatedBy;
            }
            else
            {
                dbItem.CreatedDate = DateTime.Now;
                dbItem.CreatedBy = dtoItem.UpdatedBy;
            }
        }
    }
}
