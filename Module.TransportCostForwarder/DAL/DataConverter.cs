using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FrameworkSetting;
using Library;
using Module.Support.DAL;
using Module.Support.DTO;
using Module.TransportCostForwarder.DTO;

namespace Module.TransportCostForwarder.DAL
{
    internal class DataConverter
    {
        //private System.Globalization.CultureInfo _cInfo = new System.Globalization.CultureInfo("vi-VN");
        //private DateTime _tmpDate;
        protected static readonly string DateTimeFormatString = "dd/MM/yyyy";

        public DataConverter()
        {
            var mapName = GetType().Assembly.GetName().Name;

            if (Setting.Maps.Contains(mapName)) return;

            // from db to dto
            Mapper.CreateMap<TransportCostForwarder_TransportCostForwarderSearchResult_View, TransportCostForwarderSearchResult>()
                .ForMember(d => d.TransportInvoiceDate, o => o.ResolveUsing(s => s.TransportInvoiceDate.HasValue ? s.TransportInvoiceDate.Value.ToString(DateTimeFormatString):""))
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString(DateTimeFormatString) : ""))
                .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString(DateTimeFormatString) : ""))
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            Mapper.CreateMap<TransportCostForwarder_TransportCostForwarder_View, DTO.TransportCostForwarder>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.TransportInvoiceDate, o => o.ResolveUsing(s => s.TransportInvoiceDate.HasValue ? s.TransportInvoiceDate.Value.ToString(DateTimeFormatString) : ""))
                .ForMember(d => d.ETD, o => o.ResolveUsing(s => s.ETD.HasValue ? s.ETD.Value.ToString(DateTimeFormatString) : string.Empty))
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString(DateTimeFormatString) : ""))
                .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString(DateTimeFormatString) : ""))
                .ForMember(d => d.TransportCostForwarderItems, o => o.MapFrom(s => s.TransportCostForwarder_TransportCostForwarderItem_View))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            Mapper.CreateMap<TransportCostForwarder_TransportCostForwarderItem_View, DTO.TransportCostForwarderItem>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            Mapper.CreateMap<SupportMng_BookingForwarder_View, DTO.BookingForwarder>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ETD, o => o.ResolveUsing(s => s.ETD.HasValue ? s.ETD.Value.ToString(DateTimeFormatString) : string.Empty))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            // from dto to db
            Mapper.CreateMap<DTO.TransportCostForwarder, TransportCostForwarder>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.TransportInvoiceDate, o => o.Ignore())
                .ForMember(d => d.CreatedBy, o => o.Ignore())
                .ForMember(d => d.CreatedDate, o => o.Ignore())
                .ForMember(d => d.UpdatedBy, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore())
                .ForMember(d => d.TransportCostForwarderItem, o => o.Ignore());

            Mapper.CreateMap<DTO.TransportCostForwarderItem, TransportCostForwarderItem>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.PricePerUnit, o => o.ResolveUsing(s => s.PricePerUnit ?? 0))
                .ForMember(d => d.NumberOfUnits, o => o.ResolveUsing(s => s.NumberOfUnits ?? 0))
                .ForMember(d => d.TypeOfCurrency, o => o.ResolveUsing(s => s.CurrencyValue ?? 0))
                .ForMember(d => d.TransportCostForwarderItemID, o => o.Ignore());

            Mapper.CreateMap<SupportMng_Container_View, DTO.BookingContainer>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            Setting.Maps.Add(mapName);
        }

        public List<TransportCostForwarderSearchResult> DB2DTO_TransportCostForwarderSearchResultList(List<TransportCostForwarder_TransportCostForwarderSearchResult_View> dbItems)
        {
            return Mapper.Map<List<TransportCostForwarder_TransportCostForwarderSearchResult_View>, List<TransportCostForwarderSearchResult>>(dbItems);
        }

        public DTO.TransportCostForwarder DB2DTO_TransportCostForwarder(TransportCostForwarder_TransportCostForwarder_View dbItem)
        {
            return Mapper.Map<TransportCostForwarder_TransportCostForwarder_View, DTO.TransportCostForwarder>(dbItem);
        }

        public void DTO2DB_TransportCostForwarder(DTO.TransportCostForwarder dtoItem, ref TransportCostForwarder dbItem)
        {
            if (dtoItem.TransportCostForwarderItems != null)
            {
                foreach (var item in dbItem.TransportCostForwarderItem.ToArray())
                {
                    if (!dtoItem.TransportCostForwarderItems.Select(s => s.TransportCostForwarderItemID).Contains(item.TransportCostForwarderItemID))
                    {
                        dbItem.TransportCostForwarderItem.Remove(item);
                    }
                }

                foreach (var dto in dtoItem.TransportCostForwarderItems)
                {
                    TransportCostForwarderItem item;

                    if (dto.TransportCostForwarderItemID < 0)
                    {
                        item = new TransportCostForwarderItem();

                        dbItem.TransportCostForwarderItem.Add(item);
                    }
                    else
                    {
                        item = dbItem.TransportCostForwarderItem.FirstOrDefault(o => o.TransportCostForwarderItemID == dto.TransportCostForwarderItemID);
                    }

                    if (item != null)
                    {
                        Mapper.Map<DTO.TransportCostForwarderItem, TransportCostForwarderItem>(dto, item);
                    }
                }
            }

            // Convert string to DateTime Transport Invoice Date
            if (!string.IsNullOrEmpty(dtoItem.TransportInvoiceDate))
            {
                dbItem.TransportInvoiceDate = dtoItem.TransportInvoiceDate.ConvertStringToDateTime();
            }
            else
            {
                dbItem.TransportInvoiceDate = null;
            }

            // Mapping from dto to db
            Mapper.Map<DTO.TransportCostForwarder, TransportCostForwarder>(dtoItem, dbItem);
        }

        public Forwarder DB2DTO_Forwarder(SupportMng_Forwarder_View dbItem)
        {
            return Mapper.Map<SupportMng_Forwarder_View, Forwarder>(dbItem);
        }

        public List<BookingForwarder> DB2DTO_BookingForwader(List<SupportMng_BookingForwarder_View> dbItems)
        {
            return Mapper.Map<List<SupportMng_BookingForwarder_View>, List<BookingForwarder>>(dbItems);
        }

        public List<BookingContainer> DB2DTO_BookingContainer(List<SupportMng_Container_View> dbItems)
        {
            return Mapper.Map<List<SupportMng_Container_View>, List<BookingContainer>>(dbItems);
        }
    }
}
