using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.ClientPaymentMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ClientPaymentMng";

            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ClientPaymentMng_ClientPaymentSearch_View, DTO.ClientPaymentMng.ClientPaymentSearch>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PaymentDate, o => o.ResolveUsing(s => s.PaymentDate.HasValue ? s.PaymentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientPaymentMng_ClientPaymentDetail_View, DTO.ClientPaymentMng.ClientPaymentDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PaymentDate, o => o.ResolveUsing(s => s.PaymentDate.HasValue ? s.PaymentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientPaymentMng_ClientPayment_View, DTO.ClientPaymentMng.ClientPayment>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PaymentDate, o => o.ResolveUsing(s => s.PaymentDate.HasValue ? s.PaymentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.ResolveUsing(s => s.ConcurrencyFlag != null ? Convert.ToBase64String(s.ConcurrencyFlag) : ""))
                    .ForMember(d => d.ClientPaymentDetails, o => o.MapFrom(s => s.ClientPaymentMng_ClientPaymentDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ClientPaymentMng.ClientPayment, ClientPayment>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ClientPaymentID, o => o.Ignore())
                    .ForMember(d => d.ClientPaymentDetail, o => o.Ignore())
                    .ForMember(d => d.PaymentDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.ClientPaymentMng.ClientPaymentDetail, ClientPaymentDetail>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.PaymentDate, o => o.Ignore())
                     .ForMember(d => d.ReceivedDate, o => o.Ignore())
                     .ForMember(d => d.ClientPaymentDetailID, o => o.Ignore())
                     ;

                FrameworkSetting.Setting.Maps.Add(mapName);

            }
        }

        public List<DTO.ClientPaymentMng.ClientPaymentSearch> DB2DTO_ClientPaymentSearch(List<ClientPaymentMng_ClientPaymentSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ClientPaymentMng_ClientPaymentSearch_View>, List<DTO.ClientPaymentMng.ClientPaymentSearch>>(dbItems);
        }

        public DTO.ClientPaymentMng.ClientPayment DB2DTO_ClientPayment(ClientPaymentMng_ClientPayment_View dbItem)
        {
            DTO.ClientPaymentMng.ClientPayment dtoItem = AutoMapper.Mapper.Map<ClientPaymentMng_ClientPayment_View, DTO.ClientPaymentMng.ClientPayment>(dbItem);
            return dtoItem;
        }

        public void DTO2DB_ClientPayment(DTO.ClientPaymentMng.ClientPayment dtoItem, ref ClientPayment dbItem)
        {
            List<ClientPaymentDetail> paymentdetail_tobedeleted = new List<ClientPaymentDetail>();
            if (dtoItem.ClientPaymentDetails != null)
            {
                //CHECK
                foreach (var dbDetail in dbItem.ClientPaymentDetail.Where(o => !dtoItem.ClientPaymentDetails.Select(s => s.ClientPaymentDetailID).Contains(o.ClientPaymentDetailID)))
                {
                    paymentdetail_tobedeleted.Add(dbDetail);
                }
                foreach (var dbDetail in paymentdetail_tobedeleted)
                {
                    dbItem.ClientPaymentDetail.Remove(dbDetail);
                }
                //MAP
                foreach (var dtoDetail in dtoItem.ClientPaymentDetails)
                {
                    ClientPaymentDetail dbDetail;
                    if (dtoDetail.ClientPaymentDetailID < 0)
                    {
                        dbDetail = new ClientPaymentDetail();
                        dbItem.ClientPaymentDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.ClientPaymentDetail.FirstOrDefault(o => o.ClientPaymentDetailID == dtoDetail.ClientPaymentDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ClientPaymentMng.ClientPaymentDetail, ClientPaymentDetail>(dtoDetail, dbDetail);
                        dbDetail.PaymentDate = dtoDetail.PaymentDate.ConvertStringToDateTime();
                        dbDetail.ReceivedDate = dtoDetail.ReceivedDate.ConvertStringToDateTime();
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.ClientPaymentMng.ClientPayment, ClientPayment>(dtoItem, dbItem);
            if (dtoItem.ClientPaymentID < 0 )
            {
                dbItem.CreatedDate = DateTime.Now;
                dbItem.CreatedBy = dtoItem.UpdatedBy;
            }
            dbItem.UpdatedDate = DateTime.Now;
            dbItem.UpdatedBy = dtoItem.UpdatedBy;

            dbItem.PaymentDate = dtoItem.PaymentDate.ConvertStringToDateTime();
        }

            
    }
}
