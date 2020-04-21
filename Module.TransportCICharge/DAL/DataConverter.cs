namespace Module.TransportCICharge.DAL
{
    using AutoMapper;
    using FrameworkSetting;
    using Library;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class DataConverter
    {
        #region ** Variables & Constants **

        protected readonly string FormatDateTimeToString = "dd/MM/yyyy";

        #endregion

        #region ** Constructors **

        public DataConverter()
        {
            var mapName = GetType().Assembly.GetName().Name;

            if (Setting.Maps.Contains(mapName)) return;

            // from db to dto
            Mapper.CreateMap<TransportCICharge_TransportCIChargeSearch_View, DTO.TransportCIChargeSearchResultDto>()
                .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString(FormatDateTimeToString) : ""))
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString(FormatDateTimeToString) : ""))
                .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString(FormatDateTimeToString) : ""))
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            Mapper.CreateMap<TransportCICharge_TransportCICharge_View, DTO.TransportCIChargeDto>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString(FormatDateTimeToString) : ""))
                .ForMember(d => d.ETD, o => o.ResolveUsing(s => s.ETD.HasValue ? s.ETD.Value.ToString(FormatDateTimeToString) : string.Empty))
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString(FormatDateTimeToString) : ""))
                .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString(FormatDateTimeToString) : ""))
                .ForMember(d => d.TransportCIChargeDetail, o => o.MapFrom(s => s.TransportCICharge_TransportCIChargeDetail_View))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            Mapper.CreateMap<TransportCICharge_TransportCIChargeDetail_View, DTO.TransportCIChargeDetailDto>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            Mapper.CreateMap<SupportMng_BookingForClient_View, DTO.ClientBooking>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ETD, o => o.ResolveUsing(s => s.ETD.HasValue ? s.ETD.Value.ToString(FormatDateTimeToString) : string.Empty))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            Mapper.CreateMap<TransportCICharge_PriceListClientCharge_View, DTO.PriceListCICharge>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString(FormatDateTimeToString) : string.Empty))
                .ForMember(d => d.EndDate, o => o.ResolveUsing(s => s.EndDate.HasValue ? s.EndDate.Value.ToString(FormatDateTimeToString) : string.Empty))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            // from dto to db
            Mapper.CreateMap<DTO.TransportCIChargeDto, TransportCICharge>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.InvoiceDate, o => o.Ignore())
                .ForMember(d => d.CreatedBy, o => o.Ignore())
                .ForMember(d => d.CreatedDate, o => o.Ignore())
                .ForMember(d => d.UpdatedBy, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore())
                .ForMember(d => d.TransportCIChargeDetail, o => o.Ignore());

            Mapper.CreateMap<DTO.TransportCIChargeDetailDto, TransportCIChargeDetail>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.PricePerUnit, o => o.ResolveUsing(s => s.PricePerUnit ?? 0))
                .ForMember(d => d.NumberOfUnits, o => o.ResolveUsing(s => s.NumberOfUnits ?? 0))
                .ForMember(d => d.CurrencyTypeID, o => o.ResolveUsing(s => s.CurrencyTypeID ?? 0))
                .ForMember(d => d.TransportCIChargeDetailID, o => o.Ignore());

            Mapper.CreateMap<SupportMng_Container_View, DTO.ContainerNr>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            Setting.Maps.Add(mapName);
        }

        #endregion

        #region ** Functions & Methods **

        public List<DTO.TransportCIChargeSearchResultDto> DB2DTO_TransportCICharge_SearchView(List<TransportCICharge_TransportCIChargeSearch_View> dbItems)
        {
            return Mapper.Map<List<TransportCICharge_TransportCIChargeSearch_View>, List<DTO.TransportCIChargeSearchResultDto>>(dbItems);
        }

        public DTO.TransportCIChargeDto DB2DTO_TransportCICharge(TransportCICharge_TransportCICharge_View dbItem)
        {
            return Mapper.Map<TransportCICharge_TransportCICharge_View, DTO.TransportCIChargeDto>(dbItem);
        }

        public List<DTO.ClientBooking> DB2DTO_BookingForClient(List<SupportMng_BookingForClient_View> dbItems)
        {
            return Mapper.Map<List<SupportMng_BookingForClient_View>, List<DTO.ClientBooking>>(dbItems);
        }

        public List<DTO.ContainerNr> DB2DTO_BookingContainer(List<SupportMng_Container_View> dbItems)
        {
            return Mapper.Map<List<SupportMng_Container_View>, List<DTO.ContainerNr>>(dbItems);
        }

        public void DTO2DB_TransportCICharge(DTO.TransportCIChargeDto dtoItem, ref TransportCICharge dbItem)
        {
            if (dtoItem.TransportCIChargeDetail != null)
            {
                foreach (var item in dbItem.TransportCIChargeDetail.ToArray())
                {
                    if (!dtoItem.TransportCIChargeDetail.Select(s => s.TransportCIChargeDetailID).Contains(item.TransportCIChargeDetailID))
                    {
                        dbItem.TransportCIChargeDetail.Remove(item);
                    }
                }

                foreach (var dto in dtoItem.TransportCIChargeDetail)
                {
                    TransportCIChargeDetail item;

                    if (dto.TransportCIChargeDetailID < 0)
                    {
                        item = new TransportCIChargeDetail();

                        dbItem.TransportCIChargeDetail.Add(item);
                    }
                    else
                    {
                        item = dbItem.TransportCIChargeDetail.FirstOrDefault(o => o.TransportCIChargeDetailID == dto.TransportCIChargeDetailID);
                    }

                    if (item != null)
                    {
                        Mapper.Map<DTO.TransportCIChargeDetailDto, TransportCIChargeDetail>(dto, item);
                    }
                }
            }

            // Convert string to DateTime Transport Invoice Date
            if (!string.IsNullOrEmpty(dtoItem.InvoiceDate))
            {
                dbItem.InvoiceDate = dtoItem.InvoiceDate.ConvertStringToDateTime();
            }
            else
            {
                dbItem.InvoiceDate = null;
            }

            // Mapping from dto to db
            Mapper.Map<DTO.TransportCIChargeDto, TransportCICharge>(dtoItem, dbItem);
        }

        public DTO.PriceListCICharge DB2DTO_PriceListCICharge(TransportCICharge_PriceListClientCharge_View dbItem)
        {
            return Mapper.Map<TransportCICharge_PriceListClientCharge_View, DTO.PriceListCICharge>(dbItem);
        }

        #endregion
    }
}
